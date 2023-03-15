using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetadataBitmasks = Parser.Core.Dotnet.Bitmasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// II.22.19 File : 0x26 
    /// page 253
    /// 
    /// .file
    /// 
    /// If the File table is empty, then this, by definition, is a single-file assembly. In this
    /// case, the ExportedType table should be empty
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.File)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct FileTable
    {
        /// <summary>
        /// </summary>
        public MetadataBitmasks.FileAttributes Flags { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public int Name { get; set; }
        /// <summary>
        /// an index into the Blob heap
        /// </summary>
        public int HashValue { get; set; }
    }
}
