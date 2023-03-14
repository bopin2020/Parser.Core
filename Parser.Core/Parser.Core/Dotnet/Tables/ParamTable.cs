using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// .param
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.Param)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct ParamTable
    {
        /// <summary>
        /// a 2-byte bitmask of type ParamAttributes
        /// </summary>
        public ParamAttributes Flags { get; set; }
        /// <summary>
        /// an index into the TypeDef table
        /// </summary>
        public short Sequence { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public int Name { get; set; }
    }
}
