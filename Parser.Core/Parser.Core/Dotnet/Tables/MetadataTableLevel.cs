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
    /// </summary>
    public enum MetadataTableLevel
    {
        CLIIgnore,
        Important
    }
}
