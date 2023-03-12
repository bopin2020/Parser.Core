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

        private Lazy<List<StreamHeader>> _streamHeadersLazy = new();

        public IntPtr MetadataAddr { get; private set; }

        public string MetadataString { get; private set; }


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
            for (int i = 0; i < _metadataHeader.NumberOfStreams; i++)
            {
                // StreamHeader Stream Name 不大于32字符,以4字节为边界对齐
                IntPtr streamAddr = new IntPtr(ImageBase.ToInt64() + _imageCore20Header.Metadata.MetaDataRVA + 32 + offset);
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
