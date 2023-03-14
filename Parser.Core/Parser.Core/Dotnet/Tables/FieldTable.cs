using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// Conceptually, each row in the Field table is owned by one, and only one, row in the TypeDef table.
    /// However, the owner of any row in the Field table is not stored anywhere in the Field table itself
    /// 
    /// .field
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.Field)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct FieldTable
    {
        /// <summary>
        /// a 4-byte bitmask of type TypeAttributes
        /// </summary>
        public FieldAttributes Flags { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public int Name { get; set; }
        /// <summary>
        /// an index into the Blob heap
        /// </summary>
        public int Signature { get; set; }
    }
}
