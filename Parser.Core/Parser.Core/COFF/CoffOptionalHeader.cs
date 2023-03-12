using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.COFF
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct CoffOptionalHeader
    {
        public MagicNumber Magic;
        public short VersionStamp;
        public uint ExeCodeSize;
        public uint InitDataSize;
        public uint UnInitDataSize;
        public int EntryPoint;
        public int ExeCodeAddress;
        public int InitDataAddress;
    }

    public enum MagicNumber : ushort
    {
        TMS470 = 0x0097,
        TMS320 = 0x0098,
        C5400 = 0x0098,
        C6000 = 0x0099,
        C5500 = 0x009C,
        C2800 = 0x009D,
        MSP430 = 0x00A0,
        C5500_High = 0x00A1
    }
}
