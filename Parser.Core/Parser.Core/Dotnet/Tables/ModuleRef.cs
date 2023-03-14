using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// .module extern
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.ModuleRef)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct ModuleRef
    {
        /// <summary>
        /// an index into the String heap
        /// 
        /// Name should match an entry in the Name column of the File table
        /// </summary>
        public int Name { get; set; }
    }
}
