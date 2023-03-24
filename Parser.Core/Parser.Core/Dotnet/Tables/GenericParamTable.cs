using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Parser.Core.Dotnet.Bitmasks;

namespace Parser.Core.Dotnet.Tables
{
    [MetadataTableTypeDef(MetadataTableType.GenericParam)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct GenericParamTable
    {
        /// <summary>
        /// the 2-byte index of the generic parameter, numbered left-to-right, from
        /// zero
        /// </summary>
        public ushort Number { get; set; }
        /// <summary>
        /// a 2-byte bitmask of type GenericParamAttributes
        /// </summary>
        public GenericParamAttributes Flags { get; set; }
        /// <summary>
        /// an index into the TypeDef or MethodDef table, specifying the Type or
        /// Method to which this generic parameter applies
        /// 
        /// 1 bit 
        /// 0 表示 Type
        /// 1 Method
        /// TypeOrMethodDef
        /// </summary>
        public dynamic Owner { get; set; }
        public MetadataTableType OwnerType { get; set; }
        public uint OwnerIndex { get; set; }

        /// <summary>
        /// a non-null index into the String heap, giving the name for the generic
        /// parameter
        /// </summary>
        public dynamic Name { get; set; }

        public string StringName { get; set; }
    }

    public class GenericParamTableCalc : TableBase<GenericParamTable>
    {
        public override MetadataTableType Type => MetadataTableType.GenericParam;

        public override GenericParamTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            GenericParamTable genericParam = new GenericParamTable();
            genericParam.Number = ReadUInt16(baseAddr + offset); offset += 2;
            genericParam.Flags = (GenericParamAttributes)ReadUInt16(baseAddr + offset); offset += 2;
            genericParam.Owner = CheckIndexFromWhatever(parser,baseAddr,ref offset, genericParam.Owner);

            genericParam.OwnerType = parser.Bitparser["TypeOrMethodDef"].SpecifiedTable(genericParam.Owner, out int index);
            genericParam.OwnerIndex = (uint)index;

            genericParam.Name = CheckIndexFromWhatever(parser,baseAddr,ref offset, genericParam.Name);
            genericParam.StringName = Marshal.PtrToStringAnsi(parser.GetOffset(parser.StringStreamAddr, genericParam.Name));

            Position = offset;
            return genericParam;
        }
    }
}
