using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        public uint Offset { get; set; }
        /// <summary>
        /// </summary>
        public ManifestResourceAttributes Flags { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public dynamic Name { get; set; }

        public string StringName { get; set; }

        /// <summary>
        /// an index into a File table, a AssemblyRef table, or null; more
        /// precisely, an Implementation
        /// 
        /// The Implementation specifies which file holds this resource
        /// </summary>
        public dynamic Implementation { get; set; }
        public MetadataTableType ImplementationType { get; set; }
        public uint ImplementationIndex { get; set; }
    }

    public class ManifestResourceTableCalc : TableBase<ManifestResourceTable>
    {
        public override MetadataTableType Type => MetadataTableType.ManifestResource;

        public override ManifestResourceTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            ManifestResourceTable manifestResource = new ManifestResourceTable();
            manifestResource.Offset = ReadUInt32(baseAddr + offset);offset += 4;
            manifestResource.Flags = (ManifestResourceAttributes)ReadUInt32(baseAddr + offset);offset += 4;

            manifestResource.Name = CheckIndexFromStringStream(parser, baseAddr, ref offset, manifestResource.Name);
            manifestResource.Implementation = CheckIndexFromWhatever(parser, baseAddr, ref offset, manifestResource.Implementation);

            manifestResource.ImplementationType = parser.Bitparser["Implementation"].SpecifiedTable(manifestResource.Implementation, out int index);
            manifestResource.ImplementationIndex = (uint)index;

            manifestResource.StringName = Marshal.PtrToStringAnsi(parser.GetOffset(parser.StringStreamAddr,manifestResource.Name));

            Position = offset;
            return manifestResource;
        }
    }
}
