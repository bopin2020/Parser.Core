using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// page 271
    /// 
    /// The first row of the TypeDef table represents the pseudo class that acts as parent for functions 
    /// and variables defined at module scope
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.TypeDef)]
    [MetadataTableLevel(MetadataTableLevel.Sophisticated)]
    public struct TypeDefTable
    {
        public TypeAttributes Flags { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public int TypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TypeNamespace { get; set; }
        /// <summary>
        /// an index into the TypeDef, TypeRef, or TypeSpec table
        /// </summary>
        public int Extends { get; set; }
        /// <summary>
        /// an index into the Field table
        /// </summary>
        public int FieldList { get; set; }
        /// <summary>
        /// an index into the MethodDef table
        /// </summary>
        public int MethodList { get; set; }
    }
}
