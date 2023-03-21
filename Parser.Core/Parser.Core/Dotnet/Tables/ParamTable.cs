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
    /// .param
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.Param)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct ParamTable
    {
        /// <summary>
        /// a 2-byte bitmask of type ParamAttributes
        /// </summary>
        public ParamAttributes Flags { get; set; }
        /// <summary>
        /// an index into the TypeDef table
        /// </summary>
        public ushort Sequence { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public dynamic Name { get; set; }

        public string StringName { get; set; }
    }

    public class ParamTableCalc : TableBase<ParamTable>
    {
        public override MetadataTableType Type => MetadataTableType.Param;

        public override ParamTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            ParamTable paramTable = new ParamTable();
            paramTable.Flags = (ParamAttributes)ReadUInt16(baseAddr + offset);
            offset += 2;
            paramTable.Sequence = ReadUInt16(baseAddr + offset);
            offset += 2;

            paramTable.Name = CheckIndexFromStringStream(parser, baseAddr, ref offset, paramTable.Name);
            paramTable.StringName = Marshal.PtrToStringAnsi(parser.GetOffset(parser.StringStreamAddr,paramTable.Name));

            Position = offset;

            return paramTable;
        }
    }
}
