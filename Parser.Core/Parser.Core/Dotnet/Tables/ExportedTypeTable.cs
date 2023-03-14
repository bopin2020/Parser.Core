using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// .class extern
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.ExportedType)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct ExportedTypeTable
    {
        /// <summary>
        /// a 4-byte bitmask of type TypeAttributes
        /// </summary>
        public TypeAttributes Flags { get; set; }

        /// <summary>
        /// a 4-byte index into a TypeDef table of another module in this Assembly
        /// This column is used as a hint only
        /// If the entry in the target TypeDef table matches
        /// the TypeName and TypeNamespace entries in this table, resolution has succeeded
        /// 
        /// But if there is a mismatch, the CLI shall fall back to a search of the target TypeDef
        /// table.Ignored and should be zero if Flags has IsTypeForwarder set.
        /// </summary>
        public int TypeDefId { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public int TypeName { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public int TypeNamespace { get; set; }
        /// <summary>
        /// an index into the File,ExportedType, AssemblyRef Table
        /// If implementation indexes the File Table then Flags.VisibilityMask shall be public
        /// </summary>
        public int Implementation { get; set; }
    }
}
