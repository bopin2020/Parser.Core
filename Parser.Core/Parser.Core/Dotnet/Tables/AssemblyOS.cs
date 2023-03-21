using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        public uint OSPlatformID { get; set; }

        public uint OSMajorVersion { get; set; }

        public uint OSMinorVersion { get; set; }
    }

    public class AssemblyOSCalc : TableBase<AssemblyOS>
    {
        public override MetadataTableType Type => MetadataTableType.AssemblyOS;

        public override AssemblyOS Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            AssemblyOS assemblyOS = new AssemblyOS();
            assemblyOS.OSPlatformID = ReadUInt32(baseAddr + offset); offset += 4;
            assemblyOS.OSMajorVersion = ReadUInt32(baseAddr + offset); offset += 4;
            assemblyOS.OSMinorVersion = ReadUInt32(baseAddr + offset); offset += 4;

            Position = offset;
            return assemblyOS;
        }
    }
}
