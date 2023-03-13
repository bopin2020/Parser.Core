using Parser.Core.Dotnet;
using Parser.Core.PE;
using System.Runtime.InteropServices;
using System.Text;

namespace Parser.Core
{
    public abstract class DotnetParser : PEParser
    {
        private readonly static byte[] _imageCore20Sig = { 0x48,0x00,0x00,0x00,0x02,0x00,0x05,0x00 };

        private IMAGE_COR20_HEADER _imageCore20Header;

        private MetadataHeader _metadataHeader;

        private MetadataTablesHeader _metadataTablesHeader;
        /// <summary>
        /// Array of n 4-byte unsigned integers indicating the number of rows for each present table
        /// 记录每一张表的 Rows
        /// </summary>
        private Lazy<Dictionary<ushort,int>> _rowsLazy = new();

        private Lazy<List<StreamHeader>> _streamHeadersLazy = new();
        /// <summary>
        /// We get MetadataTableHeader #~ addr after the last Stream offset
        /// </summary>
        private IntPtr _lastStreamAddr;

        public IntPtr MetadataAddr { get; private set; }

        public string MetadataString { get; private set; }

        public int EntryPointToken
        {
            get
            {
                return _imageCore20Header.EntryPointToken;
            }

        }

        private int Padding4Bytes(string name)
        {
            return Padding4Bytes(name.Length);
        }

        private int Padding4Bytes(int size)
        {
            int tmp1 = size / 4;
            int tmp2 = size % 4;
            return tmp2 == 0 ? tmp1 * 4 : (tmp1 + 1) * 4;
        }

        /// <summary>
        /// MetadataTablesHeader.Valid long 8字节数据 按位解析表
        /// </summary>
        private void ParseTables()
        {
            // 获取二进制数据 从右往左解析
            char[] tables = Convert.ToString(_metadataTablesHeader.Valid, 2).ToCharArray();
            Array.Reverse(tables);
            for (int i = 0; i < tables.Length; i++)
            {
                if (tables[i] == '1')
                {
                    _rowsLazy.Value.Add((ushort)i, 0);
                }
            }
        }

        private void Init()
        {
            IntPtr cor20Addr = new IntPtr(ImageBase.ToInt64() + CLRRuntimeRVA.VirtualAddress);
            _imageCore20Header = Marshal.PtrToStructure<IMAGE_COR20_HEADER>(cor20Addr);

            IntPtr metadataAddr = new IntPtr(ImageBase.ToInt64() + _imageCore20Header.Metadata.MetaDataRVA);
            MetadataAddr = metadataAddr;
            MetadataString = Encoding.UTF8.GetString(BitConverter.GetBytes(Marshal.ReadInt32(MetadataAddr)));

            // 考虑到VersionString 以4字节边界对齐 采取手动解析
            _metadataHeader = new MetadataHeader()
            {
                Signature = Marshal.ReadInt32(metadataAddr,0),
                MajorVersion = Marshal.ReadInt16(metadataAddr,4),
                MinorVersion = Marshal.ReadInt16(metadataAddr,6),
                Reserved = Marshal.ReadInt32(metadataAddr,8),
                VersionLength = Marshal.ReadInt32(metadataAddr,12),
                VersionString = Marshal.PtrToStringAnsi(GetOffset(metadataAddr,16)),
                Flags = Marshal.ReadInt16(metadataAddr,28),
                NumberOfStreams = Marshal.ReadInt16(metadataAddr,30)
            };
            int offset = 0;
            for (int i = 0; i < _metadataHeader.NumberOfStreams + 1; i++)
            {
                // StreamHeader Stream Name 不大于32字符,以4字节为边界对齐
                IntPtr streamAddr = new IntPtr(ImageBase.ToInt64() + _imageCore20Header.Metadata.MetaDataRVA + 32 + offset);
                // 记录最后一个Stream的地址 获取MetadataTableHeaders
                if (_metadataHeader.NumberOfStreams == i)
                {
                    _lastStreamAddr = streamAddr;
                    break;
                }

                StreamHeader streamHeader = new StreamHeader()
                {
                    Offset = Marshal.ReadInt32(streamAddr, 0),
                    Size = Marshal.ReadInt32(streamAddr,4),
                    Name = Marshal.PtrToStringAnsi(GetOffset(streamAddr,8))
                };
                int padlen = Padding4Bytes(streamHeader.Name);
                offset += 8 + padlen;
                if (streamHeader.Name.Length % 4 == 0)
                    offset += (Marshal.ReadByte(GetOffset(streamAddr,8 + padlen)) == 0x00 ? 4 : 0);
                _streamHeadersLazy.Value.Add(streamHeader);
            }

            // Parse MetadataTablesHeader
            _metadataTablesHeader = Marshal.PtrToStructure<MetadataTablesHeader>(_lastStreamAddr);

            ParseTables();
        }

        protected DotnetParser(byte[] data) : base(data)
        {
            Init();
        }

        protected DotnetParser(Stream stream) : base(stream)
        {
            Init();
        }

        protected DotnetParser(string filename) : base(filename)
        {
            Init();
        }

        public virtual bool IsDotnetPE() => IsDotnet;

        public bool IsPureIL() => (_imageCore20Header.Flags | RuntimeFlags.COMIMAGE_FLAGS_ILONLY) == RuntimeFlags.COMIMAGE_FLAGS_ILONLY;

        public bool IsMixedIL() => !IsPureIL();

        public bool IsNativeIL() => IsMixedIL();

        public static DotnetParser Load(byte[] data) => new DotnetParserUS(data);
        public static DotnetParser LoadFromStream(Stream stream) => new DotnetParserUS(stream);
        public static DotnetParser LoadFile(string filename) => new DotnetParserUS(filename);

    }

    public class DotnetParserUS : DotnetParser
    {
        public DotnetParserUS(byte[] data) : base(data)
        {
        }

        public DotnetParserUS(Stream stream) : base(stream)
        {
        }

        public DotnetParserUS(string filename) : base(filename)
        {
        }
    }
}
