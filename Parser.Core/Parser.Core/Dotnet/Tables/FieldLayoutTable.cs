using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// A row in the FieldLayout table is created if the .field directive for the parent field has specified a field
    ///offset
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.FieldLayout)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct FieldLayoutTable
    {
        /// <summary>
        /// 
        /// </summary>
        public uint Offset { get; set; }
        /// <summary>
        /// an index into the Field table
        /// </summary>
        public dynamic Field { get; set; }
        /// <summary>
        /// an index into the Blob heap
        /// </summary>
        public dynamic Signature { get; set; }
    }

    public class FieldLayoutTableCalc : TableBase<FieldLayoutTable>
    {
        public override MetadataTableType Type => MetadataTableType.FieldLayout;

        public override FieldLayoutTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            FieldLayoutTable fieldLayout = new FieldLayoutTable();

            fieldLayout.Offset = ReadUInt32(baseAddr + offset);
            offset += 4;

            fieldLayout.Field = CheckIndexFromWhatever(parser, baseAddr, ref offset, fieldLayout.Field);
            fieldLayout.Signature = CheckIndexFromBlobStream(parser, baseAddr, ref offset, fieldLayout.Signature);
            Position = offset;

            return fieldLayout;
        }
    }
}
