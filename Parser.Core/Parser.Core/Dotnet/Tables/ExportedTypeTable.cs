using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Parser.Core.Dotnet.Bitmasks;

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
        public uint TypeDefId { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public dynamic TypeName { get; set; }
        public string StringTypeName { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public dynamic TypeNamespace { get; set; }
        public string StringTypeNamespace { get; set; }
        /// <summary>
        /// an index into the File,ExportedType, AssemblyRef Table
        /// If implementation indexes the File Table then Flags.VisibilityMask shall be public
        /// </summary>
        public dynamic Implementation { get; set; }
        public MetadataTableType ImplementationType { get; set; }
        public uint ImplementationIndex { get; set; }
    }

    public class ExportedTypeTableCalc : TableBase<ExportedTypeTable>
    {
        public override MetadataTableType Type => MetadataTableType.ExportedType;

        public override ExportedTypeTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            ExportedTypeTable exportedType = new ExportedTypeTable();
            exportedType.Flags = (TypeAttributes)ReadUInt32(baseAddr + offset);offset += 4;
            exportedType.TypeDefId = ReadUInt32(baseAddr + offset);offset += 4;

            exportedType.TypeName = CheckIndexFromStringStream(parser, baseAddr, ref offset, exportedType.TypeName);
            exportedType.TypeNamespace = CheckIndexFromStringStream(parser, baseAddr, ref offset, exportedType.TypeNamespace);
            exportedType.Implementation = CheckIndexFromWhatever(parser, baseAddr, ref offset, exportedType.Implementation);

            exportedType.ImplementationType = parser.Bitparser["Implementation"].SpecifiedTable(exportedType.Implementation, out int index);
            exportedType.ImplementationIndex = (uint)index;

            exportedType.StringTypeName = Marshal.PtrToStringAnsi(parser.GetOffset(parser.StringStreamAddr,exportedType.TypeName));
            exportedType.StringTypeNamespace = Marshal.PtrToStringAnsi(parser.GetOffset(parser.StringStreamAddr,exportedType.TypeNamespace));

            Position = offset;
            return exportedType;
        }
    }
}
