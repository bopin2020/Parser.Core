using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.COFF
{
    [StructLayout(LayoutKind.Explicit, Pack = 1)]           
    public struct CoffSymbolTable
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] Name;
        [FieldOffset(8)]
        public int Value;
        [FieldOffset(12)]
        public ushort SectionNumber;
        [FieldOffset(14)]
        public ushort Type;    
        [FieldOffset(16)]
        public char StorageClass;
        [FieldOffset(17)]
        public char NumberOfAuxSymbols;
    }
}
