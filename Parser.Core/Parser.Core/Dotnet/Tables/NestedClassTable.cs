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
        public int NestedClass { get; set; }
        /// <summary>
        /// an index into the TypeDef table
        /// </summary>
        public int EnclosingClass { get; set; }
    }

    public class NestedClassTableCalc : TableBase<NestedClassTable>
    {
        public override MetadataTableType Type => MetadataTableType.NestedClass;

        public override NestedClassTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            throw new Exception();
        }
    }
}
