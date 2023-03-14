using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// page 268
    /// 
    /// Properties within metadata are best viewed as a means to gather together collections of methods
    /// defined on a class
    /// 
    /// The methods are typically get_ and set_
    /// methods, already defined on the class, and inserted like any other methods into the MethodDef table
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.Property)]
    [MetadataTableLevel(MetadataTableLevel.Sophisticated)]
    public struct PropertyTable
    {
        /// <summary>
        /// 
        /// </summary>
        public PropertyAttributes Flags { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public int Name { get; set; }
        /// <summary>
        /// an index into the Blob heap
        /// 
        /// The name of this column is misleading. It does
        ///not index a TypeDef or TypeRef table—instead it indexes the signature in the Blob
        ///heap of the Property
        /// </summary>
        public int Type { get; set; }

    }
}
