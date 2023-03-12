using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.COFF
{
    [StructLayout(LayoutKind.Explicit)]
    public struct CoffSectionHeader
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] Name;
        [FieldOffset(8)]
        public uint PhysicalAddr;
        [FieldOffset(12)]
        public uint VirtualAddr;
        [FieldOffset(16)]
        public uint SectionSize;
        [FieldOffset(20)]
        public uint RawdataPtr;
        [FieldOffset(24)]
        public uint RelocationPtr;
        [FieldOffset(28)]
        public uint LineNumberPtr;
        [FieldOffset(32)]
        public ushort RelocationsNumber;
        [FieldOffset(34)]
        public ushort LineNumbersNumber;
        [FieldOffset(36)]
        public uint Characteristics;
    }
}
