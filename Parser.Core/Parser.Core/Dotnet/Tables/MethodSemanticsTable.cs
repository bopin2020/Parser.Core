using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Core.Dotnet.Bitmasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// .property
    /// .event
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.MethodSemantics)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct MethodSemanticsTable
    {
        /// <summary>
        /// 
        /// </summary>
        public MethodSemanticsAttributes Semantics { get; set; }
        /// <summary>
        /// an index into the MethodDef table
        /// </summary>
        public int Method { get; set; }
        /// <summary>
        /// an index into the Event or Property table
        /// </summary>
        public int Association { get; set; }
    }

    public class MethodSemanticsTableCalc : TableBase<MethodSemanticsTable>
    {
        public override MetadataTableType Type => MetadataTableType.MethodSemantics;

        public override MethodSemanticsTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            throw new Exception();
        }
    }
}
