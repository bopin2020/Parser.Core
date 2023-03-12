using Parser.Core.PE;
using System;
using System.Runtime.InteropServices;
using static System.Collections.Specialized.BitVector32;

namespace Parser.Core
{
    public abstract class PEParser : IParser
    {
        private const bool _allocMemZeroFill = false;

        private bool _isDotnet;

        private IMAGE_DOS_HEADER _dosHeader;

        private IMAGE_FILE_HEADER _fileHeader;

        private IMAGE_NT_HEADERS32 _ntHeader32;

        private IMAGE_NT_HEADERS64 _ntHeader64;

        private IMAGE_OPTIONAL_HEADER32 _optionalHeader32;

        private IMAGE_OPTIONAL_HEADER64 _optionalHeader64;

        private Lazy<List<IMAGE_SECTION_HEADER>> _sectionsHeaderLazy = new ();

        public bool IsDotnet
        {
            get
            {
                if (Is32BitHeader)
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

        public IntPtr ImageBase { get; private set; }

        public RVA CLRRuntimeRVA
        {
            get
            {
                return new RVA()
                {
                    VirtualAddress = Is32BitHeader ? _optionalHeader32.CLRRuntimeHeader.MetaDataRVA : _optionalHeader64.CLRRuntimeHeader.MetaDataRVA
                };
            }
        }

        private void ImageMemory(int size)
        {
            if (_allocMemZeroFill)
            {
                unsafe
                {
                    byte* basePtr = stackalloc byte[size];
                    ImageBase = new IntPtr(basePtr);
                }
            }
            else
            {
                ImageBase = Marshal.AllocHGlobal(size);
            }
        }

        private void CopyPEHeaderToMem()
        {
            if(Is32BitHeader)
            {
                for (int i = 0; i < _optionalHeader32.SizeOfHeaders; i++)
                {
                    Marshal.WriteByte(ImageBase, i, OriginalData[i]);
                }
            }
            else
            {
                for (int i = 0; i < _optionalHeader64.SizeOfHeaders; i++)
                {
                    Marshal.WriteByte(ImageBase, i, OriginalData[i]);
                }
            }

        }

        private void CopyPESectionToMem()
        {
            foreach (var item in _sectionsHeaderLazy.Value)
            {
                IntPtr secAddr = new IntPtr(ImageBase.ToInt64() + item.VirtualAddress);
                for (int i = 0; i < item.SizeOfRawData; i++)
                {
                    Marshal.WriteByte(secAddr, i, OriginalData[i + item.PointerToRawData]);
                }
            }
            // we won't execute pe in memory so that's no need change Section Characteristics
        }

        private void InitSections(BinaryReader br)
        {
            for (int i = 0; i < _fileHeader.NumberOfSections; i++)
            {
                var section = FromBinaryReader<IMAGE_SECTION_HEADER>(br);
                _sectionsHeaderLazy.Value.Add(section);
            }
        }

        private void Init()
        {
            if (!IsPEFile) { throw new ArgumentException("The binary was not standard pe file"); }
            using (MemoryStream ms = new MemoryStream(OriginalData))
            {
                using (BinaryReader br = new BinaryReader(ms))
                {
                    _dosHeader = FromBinaryReader<IMAGE_DOS_HEADER>(br);
                    ms.Seek(_dosHeader.e_lfanew, SeekOrigin.Begin);
                    // PE00
                    UInt32 ntHeadersSignature = br.ReadUInt32();
                    _fileHeader = FromBinaryReader<IMAGE_FILE_HEADER>(br);
                    if (Is32BitHeader)
                    {
                        _optionalHeader32 = FromBinaryReader<IMAGE_OPTIONAL_HEADER32>(br);
                        _ntHeader32 = new IMAGE_NT_HEADERS32()
                        {
                            Signature = ntHeadersSignature,
                            FileHeader = _fileHeader,
                            OptionalHeader = _optionalHeader32
                        };

                        ImageMemory((int)_optionalHeader32.SizeOfImage);
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
                        ImageMemory((int)_optionalHeader64.SizeOfImage);
                    }

                    CopyPEHeaderToMem();
                    InitSections(br);
                    CopyPESectionToMem();
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

        public DateTime GetDateStamp()
        {
            // TimeDateStamp to DateTime
            // UNDO
            return DateTime.FromFileTime(_fileHeader.TimeDateStamp + new DateTime(1970, 1, 1, 0, 0, 0).ToFileTimeUtc());
        }

        public T FromBinaryReader<T>(BinaryReader br) where T : struct
        {
            byte[] data = br.ReadBytes(Marshal.SizeOf(typeof(T)));
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            var result = Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
            handle.Free();
            return result;
        }

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

    public struct RVA
    {
        public int VirtualAddress { get; set; }
    }
}
