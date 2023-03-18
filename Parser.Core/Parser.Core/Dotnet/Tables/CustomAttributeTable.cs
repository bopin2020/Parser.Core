using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// II.22.10 CustomAttribute : 0x0C 
    /// page 242
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.CustomAttribute)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct CustomAttributeTable
    {
        /// <summary>
        /// an index into a metadata table that has an associated HasCustomAttribute
        /// (§II.24.2.6) coded index
        /// 
        /// .custom
        /// </summary>
        public int Parent { get; set; }

        /// <summary>
        /// an index into the MethodDef or MemberRef table
        /// 
        /// The column called Type is slightly misleading
        /// it actually indexes a constructor method
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// an index into the Blob heap
        /// </summary>
        public int Value { get; set; }
    }

    public class CustomAttributeTableCalc : TableBase<CustomAttributeTable>
    {
        public override MetadataTableType Type => MetadataTableType.CustomAttribute;

        public override CustomAttributeTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            throw new Exception();
        }
    }
}
