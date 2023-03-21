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
    /// Conceptually, each row in the Field table is owned by one, and only one, row in the TypeDef table.
    /// However, the owner of any row in the Field table is not stored anywhere in the Field table itself
    /// 
    /// .field
    /// 
    /// Each row shall have one, and only one, owner row in the TypeDef table
    /// 
    /// The owner row in the TypeDef table shall not be an Interface    => Interface be forbidden for the ability of being created Fileds
    /// 
    /// 字段如果被定义在 <Module> 指示字段 Flags.MemberAccessMask Public,CompilerControlled,or Private
    /// 
    /// 字段如果是一个Enum  TypeDef row应该继承自 System.Enum
    /// 
    /// 只能从 TypeDef 查找 Field Table 因此没有在FieldTable 设置Parent域值
    /// However, the owner of any row in the Field table is not stored anywhere in the Field table itself
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.Field)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct FieldTable
    {
        /// <summary>
        /// a 4-byte bitmask of type TypeAttributes
        /// more precisely  FieldAccessMask subfield 
        /// </summary>
        public FieldAttributes Flags { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public dynamic Name { get; set; }

        public string StringName { get; set; }

        /// <summary>
        /// an index into the Blob heap
        /// </summary>
        public dynamic Signature { get; set; }
    }

    public class FieldTableCalc : TableBase<FieldTable>
    {
        public override MetadataTableType Type => MetadataTableType.Field;

        public override FieldTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            FieldTable fieldTable = new FieldTable();
            fieldTable.Flags = (FieldAttributes)ReadUInt16(baseAddr, offset);
            offset += 2;

            fieldTable.Name = CheckIndexFromStringStream(parser, baseAddr, ref offset, fieldTable.Name);
            fieldTable.Signature = CheckIndexFromBlobStream(parser, baseAddr, ref offset, fieldTable.Signature);

            fieldTable.StringName = Marshal.PtrToStringAnsi(parser.GetOffset(parser.StringStreamAddr,fieldTable.Name));
            Position = offset;

            return fieldTable;
        }
    }
}
