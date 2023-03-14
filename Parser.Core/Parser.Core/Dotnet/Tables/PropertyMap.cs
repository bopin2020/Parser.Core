using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// page 268
    /// 
    /// .property
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.PropertyMap)]
    [MetadataTableLevel(MetadataTableLevel.Sophisticated)]
    public struct PropertyMap
    {
        /// <summary>
        /// an index into the TypeDef table
        /// </summary>
        public int Parent { get; set; }
        /// <summary>
        /// an index into the Property table
        /// </summary>
        public int PropertyList { get; set; }
    }
}
