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
    /// page 271
    /// 
    /// The first row of the TypeDef table represents the pseudo class that acts as parent for functions 
    /// and variables defined at module scope
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.TypeDef)]
    [MetadataTableLevel(MetadataTableLevel.Sophisticated)]
    public struct TypeDefTable
    {
        public TypeAttributes Flags { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public dynamic TypeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public dynamic TypeNamespace { get; set; }

        public string StringTypeName { get; set; }
        public string StringTypeNamespace { get; set; }

        /// <summary>
        /// an index into the TypeDef, TypeRef, or TypeSpec table
        /// </summary>
        public dynamic Extends { get; set; }
        /// <summary>
        /// an index into the Field table
        /// </summary>
        public dynamic FieldList { get; set; }
        /// <summary>
        /// an index into the MethodDef table
        /// 
        /// TypeDef 定义了多个方法
        /// </summary>
        public dynamic MethodList { get; set; }
    }

    public class TypeDefTableCalc : TableBase<TypeDefTable>
    {
        public override MetadataTableType Type => MetadataTableType.TypeDef;

        public override TypeDefTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            TypeDefTable typeDefTable = new TypeDefTable();
            typeDefTable.Flags = (TypeAttributes)Marshal.ReadInt32(baseAddr + offset);
            offset += 4;

            typeDefTable.TypeName = CheckIndexFromStringStream(parser, baseAddr, ref offset, typeDefTable.TypeName);
            typeDefTable.TypeNamespace = CheckIndexFromStringStream(parser, baseAddr, ref offset, typeDefTable.TypeNamespace);
            typeDefTable.Extends = CheckIndexFromWhatever(parser, baseAddr, ref offset, typeDefTable.Extends, 65536);
            typeDefTable.FieldList = CheckIndexFromWhatever(parser, baseAddr, ref offset, typeDefTable.FieldList, 65536);
            typeDefTable.MethodList = CheckIndexFromWhatever(parser, baseAddr, ref offset, typeDefTable.MethodList, 65536);

            typeDefTable.StringTypeName = Marshal.PtrToStringAnsi(parser.StringStreamAddr + typeDefTable.TypeName);
            typeDefTable.StringTypeNamespace = Marshal.PtrToStringAnsi(parser.StringStreamAddr + typeDefTable.TypeNamespace);

            Position = offset;
            return typeDefTable;
        }
    }
}
