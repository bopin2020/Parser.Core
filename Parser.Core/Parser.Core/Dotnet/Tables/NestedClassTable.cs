using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    [MetadataTableTypeDef(MetadataTableType.NestedClass)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct NestedClassTable
    {
        /// <summary>
        /// an index into the TypeDef table
        /// 
        /// </summary>
        public dynamic NestedClass { get; set; }
        /// <summary>
        /// an index into the TypeDef table
        /// </summary>
        public dynamic EnclosingClass { get; set; }
    }

    public class NestedClassTableCalc : TableBase<NestedClassTable>
    {
        public override MetadataTableType Type => MetadataTableType.NestedClass;

        public override NestedClassTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            NestedClassTable nestedClassTable = new NestedClassTable();
            nestedClassTable.NestedClass = CheckIndexFromWhatever(parser, baseAddr, ref offset, nestedClassTable.NestedClass);
            nestedClassTable.EnclosingClass = CheckIndexFromWhatever(parser, baseAddr, ref offset, nestedClassTable.EnclosingClass);

            Position = offset;
            return nestedClassTable;
        }
    }
}
