using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// Conceptually, each row in the FieldRVA table is an extension to exactly one row in the Field table
    /// , and  records the RVA(Relative Virtual Address) within the image file at which this field’s initial value isstored
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.FieldRVA)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct FieldRVA
    {
        /// <summary>
        /// RVA shall be non-zero
        /// RVA shall point into the current module’s data area (not its metadata area)
        /// </summary>
        public int RVA { get; set; }
        /// <summary>
        /// an index into the Field table
        /// </summary>
        public int Field { get; set; }
    }
}
