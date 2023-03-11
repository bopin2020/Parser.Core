using Parser.Core.PE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core
{
    public abstract class PEParser : IParser
    {
        private bool _isDotnet;

        private IMAGE_DOS_HEADER _dosHeader;

        private IMAGE_FILE_HEADER _fileHeader;

        private IMAGE_NT_HEADERS32 _ntHeader32;

        private IMAGE_NT_HEADERS64 _ntHeader64;

        private IMAGE_OPTIONAL_HEADER32 _optionalHeader32;

        private IMAGE_OPTIONAL_HEADER64 _optionalHeader64;

        public bool IsDotnet
        {
            get
            {
                if(Is32BitHeader)
                {
                    return _optionalHeader32.CLRRuntimeHeader.MetaDataRVA != 0 && _optionalHeader32.CLRRuntimeHeader.MetadataSize == 0x48;
                }
                return _optionalHeader64.CLRRuntimeHeader.MetaDataRVA != 0 && _optionalHeader64.CLRRuntimeHeader.MetadataSize == 0x48;
            }
        }

        public bool Is32BitHeader
        {
            get
            {
                // System.UInt16     Struct
                UInt16 IMAGE_FILE_32BIT_MACHINE = 0x0100;
                return (IMAGE_FILE_32BIT_MACHINE & _fileHeader.Characteristics) == IMAGE_FILE_32BIT_MACHINE;
            }
        }

        private T FromBinaryReader<T>(BinaryReader br) where T : struct
        {
            byte[] data = br.ReadBytes(Marshal.SizeOf(typeof(T)));
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            var result = Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
            handle.Free();
            return result;
        }

        private void Init()
        {
            if(!IsPEFile) { throw new ArgumentException("The binary was not standard pe file"); }
            using (MemoryStream ms = new MemoryStream(OriginalData))
            {
                using (BinaryReader br = new BinaryReader(ms))
                {
                    _dosHeader = FromBinaryReader<IMAGE_DOS_HEADER>(br);
                    ms.Seek(_dosHeader.e_lfanew, SeekOrigin.Begin);
                    UInt32 ntHeadersSignature = br.ReadUInt32();
                    _fileHeader = FromBinaryReader<IMAGE_FILE_HEADER>(br);
                    if(Is32BitHeader)
                    {
                        _optionalHeader32 = FromBinaryReader<IMAGE_OPTIONAL_HEADER32>(br);
                        _ntHeader32 = new IMAGE_NT_HEADERS32()
                        {
                            Signature = ntHeadersSignature,
                            FileHeader = _fileHeader,
                            OptionalHeader = _optionalHeader32
                        };
                    }
                    else
                    {
                        _optionalHeader64 = FromBinaryReader<IMAGE_OPTIONAL_HEADER64>(br);
                        _ntHeader64 = new IMAGE_NT_HEADERS64()
                        {
                            Signature = ntHeadersSignature,
                            FileHeader = _fileHeader,
                            OptionalHeader = _optionalHeader64
                        };
                    }
                }
            }
        }

        protected PEParser(byte[] data) : base(data)
        {
            Init();
        }

        protected PEParser(Stream stream) : base(stream)
        {
            Init();
        }

        protected PEParser(string filename) : base(filename)
        {
            Init();
        }

        public bool IsPE() => IsPEFile;
    }

    public class PEParserUS : PEParser
    {
        public PEParserUS(byte[] data) : base(data)
        {
        }

        public PEParserUS(Stream stream) : base(stream)
        {
        }

        public PEParserUS(string filename) : base(filename)
        {
        }
    }
}
