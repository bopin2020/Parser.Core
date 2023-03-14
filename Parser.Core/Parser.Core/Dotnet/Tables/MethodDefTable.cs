using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// .method
    /// II.22.26 MethodDef : 0x06 
    /// page 259
    /// 
    /// This contains informative text only 详细记录了元数据的规定
    /// 
    /// 
    /// 
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.MethodDef)]
    [MetadataTableLevel(MetadataTableLevel.Sophisticated)]
    public struct MethodDefTable
    {
        /// <summary>
        /// 
        /// </summary>
        public int RVA { get; set; }
        /// <summary>
        /// </summary>
        public MethodImplAttributes ImplFlags { get; set; }
        /// <summary>
        /// </summary>
        public MethodAttributes Flags { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public int Name { get; set; }
        /// <summary>
        /// an index into the Blob heap
        /// </summary>
        public int Signature { get; set; }
        /// <summary>
        /// an index into the Param table
        /// </summary>
        public int ParamList { get; set; }
    }
}
