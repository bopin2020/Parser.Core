using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// The FieldMarshal table has two columns. It ‘links’ an existing row in the Field or Param table
    /// 
    /// to information in the Blob heap that defines how that field or parameter(which, as usual, covers the
    /// method return, as parameter number 0) shall be marshalled when calling to or from unmanaged code
    /// via PInvoke dispatch
    /// 
    /// 
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.FieldMarshal)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct FieldMarshalTable
    {
        /// <summary>
        /// an index into Field or Param table; more precisely   HasFieldMarshal coded index
        /// </summary>
        public int Parent { get; set; }
        /// <summary>
        /// an index into the Blob heap
        /// </summary>
        public int NativeType { get; set; }
    }
}
