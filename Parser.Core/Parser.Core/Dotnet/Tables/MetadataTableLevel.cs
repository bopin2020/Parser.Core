using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    [AttributeUsage(AttributeTargets.Struct, AllowMultiple = true)]
    public class MetadataTableLevelAttribute : Attribute
    {
        public MetadataTableLevel Level { get; set; }

        public MetadataTableLevelAttribute(MetadataTableLevel level)
        {
            Level = level;
        }
    }

    /// <summary>
    /// recognize whether the specified table was important by self
    /// 元数据表知识的级别
    /// </summary>
    public enum MetadataTableLevel
    {
        /// <summary>
        /// CLI Ignore
        /// </summary>
        CLIIgnore,
        /// <summary>
        /// 
        /// </summary>
        Important,
        /// <summary>
        /// 非常重要
        /// </summary>
        Sophisticated,
    }
}
