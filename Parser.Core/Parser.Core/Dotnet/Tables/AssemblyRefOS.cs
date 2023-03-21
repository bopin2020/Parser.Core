using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    [MetadataTableTypeDef(MetadataTableType.AssemblyRefOS)]
    [MetadataTableLevel(MetadataTableLevel.CLIIgnore)]
    public struct AssemblyRefOS
    {
        public uint OSPlatformID { get; set; }

        public uint OSMajorVersion { get; set; }

        public uint OSMinorVersion { get; set; }
        /// <summary>
        /// an index into the AssemblyRef table
        /// </summary>
        public dynamic AssemblyRef { get; set; }
    }

    public class AssemblyRefOSCalc : TableBase<AssemblyRefOS>
    {
        public override MetadataTableType Type => MetadataTableType.AssemblyRefOS;

        public override AssemblyRefOS Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            AssemblyRefOS assemblyRefOS = new AssemblyRefOS();
            assemblyRefOS.OSPlatformID = ReadUInt32(baseAddr + offset); offset += 4;
            assemblyRefOS.OSMajorVersion = ReadUInt32(baseAddr + offset); offset += 4;
            assemblyRefOS.OSMinorVersion = ReadUInt32(baseAddr + offset); offset += 4;

            assemblyRefOS.AssemblyRef = CheckIndexFromWhatever(parser, baseAddr, ref offset, assemblyRefOS.AssemblyRef);

            Position = offset;
            return assemblyRefOS;
        }
    }
}
