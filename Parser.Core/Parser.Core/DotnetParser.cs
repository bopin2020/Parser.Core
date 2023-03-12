using Parser.Core.Dotnet;
using Parser.Core.PE;
using System.Runtime.InteropServices;

namespace Parser.Core
{
    public abstract class DotnetParser : PEParser
    {
        private readonly static byte[] _imageCore20Sig = { 0x48,0x00,0x00,0x00,0x02,0x00,0x05,0x00 };

        private IMAGE_COR20_HEADER _imageCore20Header;

        private MetadataHeader _metadataHeader;

        private MetadataTablesHeader _metadataTablesHeader;

        private List<StreamHeader> _streamHeaders = new();

        private void Init()
        {
            IntPtr cor20Addr = new IntPtr(ImageBase.ToInt64() + CLRRuntimeRVA.VirtualAddress);
            _imageCore20Header = Marshal.PtrToStructure<IMAGE_COR20_HEADER>(cor20Addr);
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
