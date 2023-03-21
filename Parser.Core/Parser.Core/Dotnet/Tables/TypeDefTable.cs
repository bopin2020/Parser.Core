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
            typeDefTable.Flags = (TypeAttributes)ReadUInt32(baseAddr + offset);
            offset += 4;

            typeDefTable.TypeName = CheckIndexFromStringStream(parser, baseAddr, ref offset, typeDefTable.TypeName);
            typeDefTable.TypeNamespace = CheckIndexFromStringStream(parser, baseAddr, ref offset, typeDefTable.TypeNamespace);
            typeDefTable.Extends = CheckIndexFromWhatever(parser, baseAddr, ref offset, typeDefTable.Extends);
            //  Extends (index into TypeDef, TypeRef or TypeSpec table; more precisely, a TypeDefOrRef coded index)
            // 低2bit 是那张表的索引
            // TypeDef  00
            // TypeRef  01
            // TypeSpec 10
            // 剩余14bit 是索引长度
            var result = SpecifiedTable(typeDefTable.Extends, out int len);
            //if (typeDefTable.Extends > parser.GetTableRows(result))
            //{
            //    offset -= 4;
            //    typeDefTable.Extends = ReadUInt16(baseAddr + offset);
            //    offset += 2;
            //}

            typeDefTable.FieldList = CheckIndexFromWhatever(parser, baseAddr, ref offset, typeDefTable.FieldList);
            typeDefTable.MethodList = CheckIndexFromWhatever(parser, baseAddr, ref offset, typeDefTable.MethodList);

            typeDefTable.StringTypeName = Marshal.PtrToStringAnsi(parser.GetOffset(parser.StringStreamAddr,typeDefTable.TypeName));
            typeDefTable.StringTypeNamespace = Marshal.PtrToStringAnsi(parser.GetOffset(parser.StringStreamAddr,typeDefTable.TypeNamespace));

            Position = offset;
            return typeDefTable;
        }

        private MetadataTableType SpecifiedTable(dynamic extends,out int index)
        {
            index = 100;
            if(extends == 0) { return MetadataTableType.TypeDef; }

            char[] tables = Convert.ToString(extends, 2).ToCharArray();
            Array.Reverse(tables);
            byte[] len = new byte[tables.Length - 2];
            // 二进制字符串 转换为字节数组  转为整数

            if (tables[0] == '0' && tables[1] == '0')
            {
                return MetadataTableType.TypeDef;
            }
            else if (tables[0] == '1' && tables[1] == '0')
            {
                return MetadataTableType.TypeRef;
            }
            else if (tables[0] == '1' && tables[1] == '0')
            {
                return MetadataTableType.TypeSpec;
            }
            else
            {
                throw new ArgumentOutOfRangeException("解析错误");
            }
        }
    }
}
