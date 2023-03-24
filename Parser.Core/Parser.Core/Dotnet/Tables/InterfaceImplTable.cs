using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// The InterfaceImpl table records the interfaces a type implements explicitly
    /// 注意: InterfaceImpl explicitly 含义是类实现接口就会被定义在这里  并不是强调显示接口方法
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.InterfaceImpl)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct InterfaceImplTable
    {
        /// <summary>
        /// an index into the TypeDef table
        /// </summary>
        public dynamic Class { get; set; }
        /// <summary>
        /// an index into the TypeDef, TypeRef, or TypeSpec table
        /// </summary>
        public dynamic Interface { get; set; }
        public MetadataTableType InterfaceType { get; set; }
        public uint InterfaceIndex { get; set; }
    }

    public class InterfaceImplTableCalc : TableBase<InterfaceImplTable>
    {
        public override MetadataTableType Type => MetadataTableType.InterfaceImpl;

        public override InterfaceImplTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            InterfaceImplTable interfaceImpl = new InterfaceImplTable();
            interfaceImpl.Class = CheckIndexFromWhatever(parser, baseAddr, ref offset, interfaceImpl.Class);
            interfaceImpl.Interface = CheckIndexFromWhatever(parser, baseAddr, ref offset, interfaceImpl.Interface);

            interfaceImpl.InterfaceType = parser.Bitparser["TypeDefOrRef"].SpecifiedTable(interfaceImpl.Interface, out int index);
            interfaceImpl.InterfaceIndex = (uint)index;

            Position = offset;
            return interfaceImpl;
        }
    }
}
