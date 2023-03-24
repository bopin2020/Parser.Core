using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// The FieldMarshal table has two columns. It ‘links’ an existing row in the Field or Param table
    /// 
    /// to information in the Blob heap that defines how that field or parameter(which, as usual, covers the
    /// method return, as parameter number 0) shall be marshalled when calling to or from unmanaged code
    /// via PInvoke dispatch
    /// 
    /// 
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.FieldMarshal)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct FieldMarshalTable
    {
        /// <summary>
        /// an index into Field or Param table; more precisely   HasFieldMarshal coded index
        /// 
        /// HasFieldMarshall: 1 bit to encode tag
        /// </summary>
        public dynamic Parent { get; set; }
        public MetadataTableType ParentType { get; set; }
        public uint ParentIndex { get; set; }
        /// <summary>
        /// an index into the Blob heap
        /// </summary>
        public dynamic NativeType { get; set; }
    }

    public class FieldMarshalTableCalc : TableBase<FieldMarshalTable>
    {
        public override MetadataTableType Type => MetadataTableType.FieldMarshal;

        public override FieldMarshalTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            FieldMarshalTable fieldMarshal = new FieldMarshalTable();

            fieldMarshal.Parent = CheckIndexFromWhatever(parser, baseAddr, ref offset, fieldMarshal.Parent);
            fieldMarshal.ParentType = parser.Bitparser["HasFieldMarshall"].SpecifiedTable(fieldMarshal.Parent, out int index);
            fieldMarshal.ParentIndex = (uint)index;

            fieldMarshal.NativeType = CheckIndexFromWhatever(parser, baseAddr, ref offset, fieldMarshal.NativeType);
            Position = offset;

            return fieldMarshal;
        }
    }
}
