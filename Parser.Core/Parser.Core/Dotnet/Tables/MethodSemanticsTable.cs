using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Parser.Core.Dotnet.Bitmasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// .property
    /// .event
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.MethodSemantics)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct MethodSemanticsTable
    {
        /// <summary>
        /// 
        /// </summary>
        public MethodSemanticsAttributes Semantics { get; set; }
        /// <summary>
        /// an index into the MethodDef table
        /// </summary>
        public dynamic Method { get; set; }
        /// <summary>
        /// an index into the Event or Property table
        /// </summary>
        public dynamic Association { get; set; }
        public MetadataTableType AssociationType { get; set; }
        public uint AssociationIndex { get; set; }
    }

    public class MethodSemanticsTableCalc : TableBase<MethodSemanticsTable>
    {
        public override MetadataTableType Type => MetadataTableType.MethodSemantics;

        public override MethodSemanticsTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            MethodSemanticsTable methodSemanticsTable = new MethodSemanticsTable();
            methodSemanticsTable.Semantics = (MethodSemanticsAttributes)ReadUInt16(baseAddr + offset); offset += 2;
            methodSemanticsTable.Method = CheckIndexFromWhatever(parser,baseAddr,ref offset, methodSemanticsTable.Method);
            methodSemanticsTable.Association = CheckIndexFromWhatever(parser,baseAddr,ref offset, methodSemanticsTable.Association);

            methodSemanticsTable.AssociationType = parser.Bitparser["HasSemantics"].SpecifiedTable(methodSemanticsTable.Association, out int index);
            methodSemanticsTable.AssociationIndex = (uint)index;

            Position = offset;
            return methodSemanticsTable;
        }
    }
}
