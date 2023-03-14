using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// MethodImpl tables let a compiler override the default inheritance rules provided by the CLI
    /// 
    /// .override
    /// 
    /// 类的析构函数      实现了Finialize方法 System.Object
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.MethodImpl)]
    [MetadataTableLevel(MetadataTableLevel.Sophisticated)]
    public struct MethodImplTable
    {
        /// <summary>
        /// an index into the TypeDef table
        /// </summary>
        public int Class { get; set; }
        /// <summary>
        /// an index into the MethodDef or MemberRef table; more precisely, a MethodDefOrRef
        /// </summary>
        public int MethodBody { get; set; }
        /// <summary>
        /// an index into the MethodDef or MemberRef table
        /// </summary>
        public int MethodDeclaration { get; set; }
    }
}
