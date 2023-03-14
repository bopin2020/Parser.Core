using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// page 269
    /// Signatures are stored in the metadata Blob heap
    /// 
    /// In most cases, they are indexed by a column in some
    /// table—Field.Signature, Method.Signature, MemberRef.Signature
    /// However, there are two cases
    /// that require a metadata token for a signature that is not indexed by any metadata table.The
    /// StandAloneSig table fulfils this need.It has just one column, which points to a Signature in the Blob
    /// heap
    /// 
    /// 
    /// The signature 'blob' indexed by Signature shall be a valid METHOD / LOCALS
    /// Duplicate rows are allowed
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.StandAloneSig)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct StandAloneSigTable
    {
        /// <summary>
        /// an index into the Blob heap
        /// 
        /// a method  方法体内 每一条calli  CIL 指令都会在 StandAloneSig Table 生成一行
        /// local variables   .locals directive   IILAsm generates a row in the StandAloneSig
        /// </summary>
        public int Signature { get; set; }
    }
}
