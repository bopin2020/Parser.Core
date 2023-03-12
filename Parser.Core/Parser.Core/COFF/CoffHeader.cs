using Parser.Core.PE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.COFF
{
    /// <summary>
    /// https://learn.microsoft.com/en-us/windows/win32/debug/pe-format#coff-file-header-object-and-image
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct CoffHeader
    {
        /// <summary>
        /// The number that identifies the type of target machine
        /// </summary>
        public MachineType Machine;
        /// <summary>
        /// The number of sections. This indicates the size of the section table, which immediately follows the headers
        /// </summary>
        public ushort NumberOfSections;
        /// <summary>
        /// The low 32 bits of the number of seconds since 00:00 January 1, 1970 (a C run-time time_t value), which indicates when the file was created
        /// </summary>
        public uint TimeDateStamp;
        /// <summary>
        /// The file offset of the COFF symbol table, or zero if no COFF symbol table is present. This value should be zero for an image because COFF debugging information is deprecated
        /// </summary>
        public uint PointerToSymbolTable;
        /// <summary>
        /// The number of entries in the symbol table. This data can be used to locate the string table, which immediately follows the symbol table. This value should be zero for an image because COFF debugging information is deprecated
        /// </summary>
        public uint NumberOfSymbols;
        /// <summary>
        /// The size of the optional header, which is required for executable files but not for object files. This value should be zero for an object file. For a description of the header format
        /// </summary>
        public ushort SizeOfOptionalHeader;
        /// <summary>
        /// 
        /// </summary>
        public UInt16 Characteristics;
    }
}
