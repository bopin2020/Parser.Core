using Parser.Core.Dotnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core
{
    public abstract class DotnetParser : PEParser
    {
        private IMAGE_COR20_HEADER _imageCore20Header;

        private MetadataHeader _metadataHeader;

        private MetadataTablesHeader _metadataTablesHeader;

        private List<StreamHeader> _streamHeaders = new();


        protected DotnetParser(byte[] data) : base(data)
        {
        }

        protected DotnetParser(Stream stream) : base(stream)
        {
        }

        protected DotnetParser(string filename) : base(filename)
        {
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
