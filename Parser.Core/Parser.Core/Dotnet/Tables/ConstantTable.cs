using Parser.Core.Dotnet.Bitmasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// The Constant table is used to store compile-time, constant values for fields, parameters, and properties
    /// 
    /// Note that Constant information does not directly influence runtime behavior, although it is visible via
    /// Reflection(and hence can be used to implement functionality
    /// 
    /// Compilers inspect this information, at compile time, when importing
    /// metadata, but the value of the constant itself, if used, becomes embedded into the CIL stream the
    /// compiler emits.There are no CIL instructions to access the Constant table at runtime
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.Constant)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct ConstantTable
    {
        /// <summary>
        /// a 1-byte constant, followed by a 1-byte padding zero
        /// </summary>
        public ElementType Type { get; set; }
        /// <summary>
        /// 00
        /// </summary>
        public byte PaddingZero { get; set; }
        /// <summary>
        /// an index into the Param, Field, or Property table; more precisely, a
        ///HasConstant(§II.24.2.6) coded index
        /// </summary>
        public dynamic Parent { get; set; }

        public MetadataTableType ParentType { get; set; }
        public uint ParentIndex { get; set; }

        /// <summary>
        /// an index into the Blob heap
        /// </summary>
        public dynamic Value { get; set; }
    }

    public class ConstantTableCalc : TableBase<ConstantTable>
    {
        public override MetadataTableType Type => MetadataTableType.Constant;

        public override ConstantTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            ConstantTable constant = new ConstantTable();
            constant.Type = (ElementType)Marshal.ReadByte(baseAddr + offset);
            offset += 1;
            constant.PaddingZero = 0x00;
            offset += 1;

            constant.Parent = CheckIndexFromWhatever(parser, baseAddr, ref offset, constant.Parent);
            constant.ParentType = parser.Bitparser["HasConstant"].SpecifiedTable(constant.Parent,out int index);
            constant.ParentIndex = (uint)index;

            constant.Value = CheckIndexFromBlobStream(parser, baseAddr, ref offset, constant.Value);
            Position = offset;
            return constant;
        }
    }
}
