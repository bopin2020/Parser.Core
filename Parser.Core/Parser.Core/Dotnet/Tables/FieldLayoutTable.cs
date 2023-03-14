using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// A row in the FieldLayout table is created if the .field directive for the parent field has specified a field
    ///offset
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.FieldLayout)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct FieldLayoutTable
    {
        /// <summary>
        /// 
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// an index into the Field table
        /// </summary>
        public int Field { get; set; }
        /// <summary>
        /// an index into the Blob heap
        /// </summary>
        public int Signature { get; set; }
    }
}
