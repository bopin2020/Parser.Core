using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// II.22.3 AssemblyOS : 0x22
    /// page 238
    /// This record should not be emitted into any PE file. However, if present in a PE file, it shall be treated
    /// as if all its fields were zero.It shall be ignored by the CLI
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.AssemblyOS)]
    [MetadataTableLevel(MetadataTableLevel.CLIIgnore)]
    public struct AssemblyOS
    {
        public int OSPlatformID { get; set; }

        public int OSMajorVersion { get; set; }

        public int OSMinorVersion { get; set; }
    }
}
