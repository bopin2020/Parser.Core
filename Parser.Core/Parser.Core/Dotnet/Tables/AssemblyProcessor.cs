using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// II.22.4 AssemblyProcessor : 0x21 
    /// This record should not be emitted into any PE file. However, if present in a PE file, it should be treated
    ///as if its field were zero.It should be ignored by the CLI.
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.AssemblyProcessor)]
    [MetadataTableLevel(MetadataTableLevel.CLIIgnore)]
    public struct AssemblyProcessor
    {
        /// <summary>
        /// a 4-byte constant
        /// </summary>
        public uint Processor { get; set; }
    }

    public class AssemblyProcessorCalc : TableBase<AssemblyProcessor>
    {
        public override MetadataTableType Type => MetadataTableType.AssemblyProcessor;

        public override AssemblyProcessor Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            AssemblyProcessor assemblyProcessor = new AssemblyProcessor();
            assemblyProcessor.Processor = ReadUInt32(baseAddr + offset);
            offset += 4;

            Position = offset;
            return assemblyProcessor;
        }
    }
}
