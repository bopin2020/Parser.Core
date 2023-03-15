using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Parser.Core.Dotnet.Bitmasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// AssemblyRef 相当于 native PE中的导入表
    /// 基本上分析AssemblyRef 就知道Assembly调用了哪些功能
    /// 
    /// TODO: 是否可能 AssemblyRef 动态加载  延迟初始化
    /// 
    /// .assembly extern directive
    ///     .publickeytoken
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.AssemblyRef)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct AssemblyRef
    {
        public short MajorVersion { get; set; }
        public short MinorVersion { get; set; }
        public short BuildNumber { get; set; }
        public short RevisionNumber { get; set; }

        public AssemblyFlags Flags { get; set; }
        /// <summary>
        /// (an index into the Blob heap, indicating the public key or token
        /// that identifies the author of this Assembly
        /// </summary>
        public int PublicKeyOrToken { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public int Name { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public int Culture { get; set; }
        /// <summary>
        /// an index into the Blob heap
        /// </summary>
        public int HashValue { get; set; }

    }
}
