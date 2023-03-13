using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    [MetadataTableTypeDef(MetadataTableType.AssemblyRefOS)]
    [MetadataTableLevel(MetadataTableLevel.CLIIgnore)]
    public struct AssemblyRefOS
    {
        public int OSPlatformID { get; set; }

        public int OSMajorVersion { get; set; }

        public int OSMinorVersion { get; set; }
        /// <summary>
        /// an index into the AssemblyRef table
        /// </summary>
        public int AssemblyRef { get; set; }
    }
}
