using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.COFF
{
    [StructLayout(LayoutKind.Explicit, Pack = 2)]           // Pack 结构的封装大小
    public struct CoffRelocation
    {
        [FieldOffset(0)]
        public uint VirtualAddress;     // 引用的虚拟地址
        [FieldOffset(4)]
        public uint SymbolTableIndex;   // 符号表搜索   .rdata   __imp_KERNEL32$HeapFree
        [FieldOffset(8)]
        public ushort RelocationType;
    }
}
