using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    [MetadataTableTypeDef(MetadataTableType.AssemblyRefProcessor)]
    [MetadataTableLevel(MetadataTableLevel.CLIIgnore)]
    public struct AssemblyRefProcessor
    {
        public int Processor { get; set; }
        public int AssemblyRef { get; set; }

    }

    public class AssemblyRefProcessorCalc : TableBase<AssemblyRefProcessor>
    {
        public override MetadataTableType Type => MetadataTableType.AssemblyRefProcessor;

        public override AssemblyRefProcessor Create(DotnetParser parser, IntPtr baseAddr)
        {
            throw new Exception();
        }
    }
}
