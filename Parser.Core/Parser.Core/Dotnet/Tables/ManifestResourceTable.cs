using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Core.Dotnet.Bitmasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// .mresource directives on the Assembly
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.ManifestResource)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct ManifestResourceTable
    {
        /// <summary>
        /// The Offset specifies the byte offset within the referenced file at which this resource record begins
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// </summary>
        public ManifestResourceAttributes Flags { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public int Name { get; set; }
        /// <summary>
        /// an index into a File table, a AssemblyRef table, or null; more
        /// precisely, an Implementation
        /// 
        /// The Implementation specifies which file holds this resource
        /// </summary>
        public int Implementation { get; set; }
    }
}
