using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// II.22.10 CustomAttribute : 0x0C 
    /// page 242
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.CustomAttribute)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct CustomAttributeTable
    {
        /// <summary>
        /// an index into a metadata table that has an associated HasCustomAttribute
        /// (§II.24.2.6) coded index
        /// 
        /// .custom
        /// 判断元数据大小 超过65535字节 则Parent 按照4字节读取
        /// </summary>
        public dynamic Parent { get; set; }

        /// <summary>
        /// an index into the MethodDef or MemberRef table
        /// 
        /// The column called Type is slightly misleading
        /// it actually indexes a constructor method
        /// 
        /// 
        /// </summary>
        public dynamic Type { get; set; }
        public MetadataTableType Type_ { get; set; }
        public uint Type_Index { get; set; }
        /// <summary>
        /// an index into the Blob heap
        /// </summary>
        public dynamic Value { get; set; }
    }

    public class CustomAttributeTableCalc : TableBase<CustomAttributeTable>
    {
        #region Private Region

        private int _MethodDefOrRef;

        private int _index;

        private int CheckMethodDefOrRef(DotnetParser parser)
        {
            int result = 0;
            result = parser.GetTableRows(MetadataTableType.MethodDef);
            int result2 = parser.GetTableRows(MetadataTableType.MemberRef);
            if (result <= result2)
            {
                return result2;
            }
            return result;
        }
        #endregion

        public override MetadataTableType Type => MetadataTableType.CustomAttribute;

        public override CustomAttributeTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            CustomAttributeTable customAttr = new CustomAttributeTable();
            customAttr.Parent = CheckIndexFromWhatever(parser, baseAddr, ref offset, customAttr.Parent,parser.MetadataSize);
            customAttr.Type = CheckCustomAttributeType(parser, baseAddr, ref offset, customAttr.Type, CheckMethodDefOrRef(parser));

            customAttr.Type_ = parser.Bitparser["MethodDefOrRef"].SpecifiedTable(customAttr.Type, out int index);
            customAttr.Type_Index = (uint)index;

            customAttr.Value = CheckIndexFromBlobStream(parser, baseAddr, ref offset, customAttr.Value);
            Position = offset;

            return customAttr;
        }


        /*
         https://www.ntcore.com/files/dotnetformat.htm
        CustomAttributeTable.Type
        CustomAttributeType: 3 bits to encode tag
        000 保留1
        001 保留2
        010 MethodDef
        011 MemberRef
        100 保留2
         */
        public virtual dynamic CheckCustomAttributeType(DotnetParser parser, IntPtr baseAddr, ref int offset, dynamic item, int index = Constants.IndexLimited)
        {
            ushort flag = PeekData(baseAddr, offset);
            if ((flag >> 3) < index && flag >= 0 && index <= Constants.IndexLimited)
            {
                item = ReadUInt16(baseAddr, offset);
                offset += 2;
            }
            else
            {
                item = ReadUInt32(baseAddr, offset);
                offset += 4;
            }

            return item;
        }
    }
}
