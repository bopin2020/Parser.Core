using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// .property
    /// .event
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.MethodSemantics)]
    [MetadataTableLevel(MetadataTableLevel.Sophisticated)]
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
}
