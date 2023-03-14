using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// 
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.TypeRef)]
    [MetadataTableLevel(MetadataTableLevel.Sophisticated)]
    public struct TypeRefTable
    {
        /// <summary>
        /// an index into a Module, ModuleRef, AssemblyRef or TypeRef table,
        /// or null; more precisely, a ResolutionScope
        /// </summary>
        public int ResolutionScope { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public int TypeName { get; set; }
        /// <summary>
        /// an index into the String heap
        /// 
        /// TypeNamespace can be null, or non-null
        /// </summary>
        public int TypeNamespace { get; set; }
    }
}
