using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    [MetadataTableTypeDef(MetadataTableType.NestedClass)]
    [MetadataTableLevel(MetadataTableLevel.Sophisticated)]
    public struct NestedClassTable
    {
        /// <summary>
        /// an index into the TypeDef table
        /// </summary>
        public int NestedClass { get; set; }
        /// <summary>
        /// an index into the TypeDef table
        /// </summary>
        public int EnclosingClass { get; set; }
    }
}
