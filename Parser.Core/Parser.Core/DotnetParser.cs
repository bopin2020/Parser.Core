using Parser.Core.Dotnet;
using Parser.Core.Dotnet.Tables;
using Parser.Core.PE;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Parser.Core
{
    public abstract class DotnetParser : PEParser
    {
        private readonly static byte[] _imageCore20Sig = { 0x48,0x00,0x00,0x00,0x02,0x00,0x05,0x00 };

        private readonly Assembly _currentAssembly = Assembly.GetExecutingAssembly();

        private const string _Tablestream = "#~";
        private const string _Stringsstream = "#Strings";
        private const string _USstream = "#US";
        private const string _GUIDstream = "#GUID";
        private const string _Blobstream = "#Blob";

        private IMAGE_COR20_HEADER _imageCore20Header;

        private MetadataHeader _metadataHeader;

        private MetadataTablesHeader _metadataTablesHeader;
        /// <summary>
        /// Array of n 4-byte unsigned integers indicating the number of rows for each present table
        /// 记录每一张表的 Rows
        /// </summary>
        private Lazy<List<MetadataTableContent>> _rowsLazy = new();

        private Lazy<List<StreamHeader>> _streamHeadersLazy = new();

        private List<object> _tables = new List<object>();

        /// <summary>
        /// We get MetadataTableHeader #~ addr after the last Stream offset
        /// </summary>
        private IntPtr _lastStreamAddr;
        /// <summary>
        /// The address of first Metadata Table how many rows that have
        /// </summary>
        private IntPtr _firstRowsNumAddr;
        /// <summary>
        /// 64 bit 表的信息 即 包含了那些表
        /// </summary>
        private IntPtr _8BytesTablesInfo;
        /// <summary>
        /// Module 表信息的基地址
        /// 在4字节所有表的长度之后 Module表是第一张表
        /// </summary>
        private IntPtr _ModuleTableInfo;

        public IntPtr MetadataAddr { get; private set; }

        public string MetadataString { get; private set; }

        public int EntryPointToken
        {
            get
            {
                return _imageCore20Header.EntryPointToken;
            }

        }

        public int MetadataSize => _imageCore20Header.Metadata.MetadataSize;

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
                    _rowsLazy.Value.Add(new MetadataTableContent()
                    {
                        Type = (MetadataTableType)i,
                    });
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
                    Name = Marshal.PtrToStringAnsi(GetOffset(streamAddr,8)),
                };
                int padlen = Padding4Bytes(streamHeader.Name);
                offset += 8 + padlen;
                if (streamHeader.Name.Length % 4 == 0)
                    offset += (Marshal.ReadByte(GetOffset(streamAddr,8 + padlen)) == 0x00 ? 4 : 0);
                streamHeader.BaseAddress = GetOffset(metadataAddr, streamHeader.Offset);
                _streamHeadersLazy.Value.Add(streamHeader);
            }

            // Parse MetadataTablesHeader
            _metadataTablesHeader = Marshal.PtrToStructure<MetadataTablesHeader>(_lastStreamAddr);

            IntPtr firstRowsNumAddr = GetOffset(_lastStreamAddr,Marshal.SizeOf(typeof(MetadataTablesHeader)));
            _8BytesTablesInfo = GetOffset(_lastStreamAddr, 8);
            _firstRowsNumAddr = firstRowsNumAddr;

            ParseTables();
            // 解析每张表中 Rows的行数
            int index = 0;
            foreach (var item in _rowsLazy.Value)
            {
                item.RowLength = Marshal.ReadInt32(firstRowsNumAddr, index);
                index += 4;
            }

            _ModuleTableInfo = GetOffset(firstRowsNumAddr,index);

            // 根据Table的 Type获取对应Row的结构  初始化设置
            foreach (var item in _rowsLazy.Value)
            {
                Type type = _currentAssembly.GetTypes().Where(x => x.GetCustomAttribute<MetadataTableTypeDefAttribute>()?.Type == item.Type).FirstOrDefault();
                for (int i = 0; i < item.RowLength; i++)
                {
                    item.Rows.Value.Add(type.Assembly.CreateInstance(type.FullName));
                }
            }
            // 设置Row数据
            // 将判断逻辑放在单独的类
            IntPtr tmpAddr = _ModuleTableInfo;
            for (int i = 0; i < _rowsLazy.Value.Count; i++)
            {
                try
                {
                    dynamic instance = Activator.CreateInstance(Tables.Vault[_rowsLazy.Value[i].Type]);
                    if (instance is null) { continue; }
                    for (int j = 0; j < _rowsLazy.Value[i].RowLength; j++)
                    {
                        _rowsLazy.Value[i].Rows.Value[j] = instance.Create(this, tmpAddr);
                        // push the position
                        tmpAddr = GetOffset(tmpAddr, instance.Position);
                    }
                }
                catch (Exception)
                {
                    continue;
                }

            }
        }

        private byte[] GetStream(string streamName)
        {
            var streamheader = _streamHeadersLazy.Value.Where(x => x.Name == streamName).FirstOrDefault();

            byte[] data = new byte[streamheader.Size];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Marshal.ReadByte(MetadataAddr, streamheader.Offset + i);
            }
            return data;
        }

        private IntPtr GetStreamAddress(string streamName)
            => GetOffset(MetadataAddr, _streamHeadersLazy.Value.Where(x => x.Name == streamName).FirstOrDefault().Offset);

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

        public byte[] GetTableStream() => GetStream(_Tablestream);
        public byte[] GetStringsStream() => GetStream(_Stringsstream);
        public byte[] GetUSStream() => GetStream(_USstream);
        public byte[] GetGUIDStream() => GetStream(_GUIDstream);
        public byte[] GetBlobStream() => GetStream(_Blobstream);

        public int GetTableRows(MetadataTableType type)
        {
            return _rowsLazy.Value.Where(x => x.Type == type).FirstOrDefault().RowLength;
        }

        public IntPtr StringStreamAddr
        {
            get
            {
                return GetStreamAddress("#Strings");
            }
        }

        public IntPtr USStreamAddr
        {
            get
            {
                return GetStreamAddress("#US");
            }
        }

        public IntPtr GUIDStreamAddr
        {
            get
            {
                return GetStreamAddress("#GUID");
            }
        }

        public IntPtr BlobStreamAddr
        {
            get
            {
                return GetStreamAddress("#Blob");
            }
        }

        public UTF8String GetStringsStreamUTF8() => new UTF8String(GetStringsStream());
        public string GetUSStreamUTF8() => Encoding.UTF8.GetString(Encoding.Convert(Encoding.Unicode,Encoding.UTF8, GetUSStream()));

        public static DotnetParser Load(byte[] data) => new DotnetParserUS(data);
        public static DotnetParser LoadFromStream(Stream stream) => new DotnetParserUS(stream);
        public static DotnetParser LoadFile(string filename) => new DotnetParserUS(filename);

        public IEnumerable<T> GetMetadataTable<T>(MetadataTableType type)
        {
            List<T> tmp = new List<T>();
            foreach (var item in _rowsLazy.Value.Where(x => x.Type == type).FirstOrDefault().Rows.Value)
            {
                tmp.Add((T)item);
            }
            return tmp;
        }
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
