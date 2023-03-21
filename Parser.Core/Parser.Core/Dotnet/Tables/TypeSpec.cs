using Parser.Core.Dotnet.Bitmasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// page 274
    /// 
    /// The TypeSpec table has just one column, which indexes the specification of a Type, stored in the Blob
    ///heap
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.TypeSpec)]
    [MetadataTableLevel(MetadataTableLevel.Sophisticated)]
    public struct TypeSpec
    {
        /// <summary>
        /// index into the Blob heap, where the blob is formatted as specified
        /// </summary>
        public dynamic Signature { get; set; }
    }

    public class TypeSpecCalc : TableBase<TypeSpec>
    {
        public override MetadataTableType Type => MetadataTableType.TypeSpec;

        public override TypeSpec Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            TypeSpec typeSpec = new TypeSpec();
            typeSpec.Signature = CheckIndexFromBlobStream(parser, baseAddr, ref offset, typeSpec.Signature);
            Position = offset;
            return typeSpec;
        }
    }
}
