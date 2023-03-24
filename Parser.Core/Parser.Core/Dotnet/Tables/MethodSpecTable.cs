using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// II.22.29 MethodSpec : 0x2B
    /// The MethodSpec table records the signature of an instantiated generic method
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.MethodSpec)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct MethodSpecTable
    {
        /// <summary>
        /// an index into the MethodDef or MemberRef table, specifying to which
        /// generic method this row refers; that is, which generic method this row is an
        /// instantiation of
        /// </summary>
        public dynamic Method { get; set; }
        public MetadataTableType MethodType { get; set; }
        public uint MethodIndex { get; set; }

        /// <summary>
        /// an index into the Blob heap holding the signature of this instantiation
        /// </summary>
        public dynamic Instantiation { get; set; }
    }

    public class MethodSpecTableCalc : TableBase<MethodSpecTable>
    {
        public override MetadataTableType Type => MetadataTableType.MethodSpec;

        public override MethodSpecTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            MethodSpecTable methodSpecTable = new MethodSpecTable();
            methodSpecTable.Method = CheckIndexFromWhatever(parser, baseAddr, ref offset, methodSpecTable.Method);
            methodSpecTable.MethodType = parser.Bitparser["MethodDefOrRef"].SpecifiedTable(methodSpecTable.Method, out int index);
            methodSpecTable.MethodIndex = (uint)index;

            methodSpecTable.Instantiation = CheckIndexFromWhatever(parser, baseAddr, ref offset, methodSpecTable.Instantiation);
            Position = offset;
            return methodSpecTable;
        }
    }
}
