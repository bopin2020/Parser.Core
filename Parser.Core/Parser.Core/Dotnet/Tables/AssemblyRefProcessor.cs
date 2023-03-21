using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    [MetadataTableTypeDef(MetadataTableType.AssemblyRefProcessor)]
    [MetadataTableLevel(MetadataTableLevel.CLIIgnore)]
    public struct AssemblyRefProcessor
    {
        /// <summary>
        /// Processor (4-byte constant)
        /// </summary>
        public uint Processor { get; set; }
        /// <summary>
        /// index into the AssemblyRef table
        /// </summary>
        public dynamic AssemblyRef { get; set; }

    }

    public class AssemblyRefProcessorCalc : TableBase<AssemblyRefProcessor>
    {
        public override MetadataTableType Type => MetadataTableType.AssemblyRefProcessor;

        public override AssemblyRefProcessor Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            AssemblyRefProcessor assemblyRefProcessor = new AssemblyRefProcessor();
            assemblyRefProcessor.Processor = ReadUInt32(baseAddr + offset); offset += 4;
            assemblyRefProcessor.AssemblyRef = CheckIndexFromStringStream(parser, baseAddr, ref offset, assemblyRefProcessor.AssemblyRef);

            Position = offset;
            return assemblyRefProcessor;
        }
    }
}
