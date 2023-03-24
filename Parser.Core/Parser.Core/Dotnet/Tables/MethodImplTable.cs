using Parser.Core.Dotnet.Bitmasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// MethodImpl tables let a compiler override the default inheritance rules provided by the CLI
    /// 
    /// .override
    /// 
    /// 类的析构函数      实现了Finialize方法 System.Object
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.MethodImpl)]
    [MetadataTableLevel(MetadataTableLevel.Sophisticated)]
    public struct MethodImplTable
    {
        /// <summary>
        /// an index into the TypeDef table
        /// </summary>
        public dynamic Class { get; set; }
        /// <summary>
        /// an index into the MethodDef or MemberRef table; more precisely, a MethodDefOrRef
        /// </summary>
        public dynamic MethodBody { get; set; }
        public MetadataTableType MethodBodyType { get; set; }
        public uint MethodBodyIndex { get; set; }
        /// <summary>
        /// an index into the MethodDef or MemberRef table
        /// </summary>
        public dynamic MethodDeclaration { get; set; }
        public MetadataTableType MethodDeclarationType { get; set; }
        public uint MethodDeclarationIndex { get; set; }
    }

    public class MethodImplTableCalc : TableBase<MethodImplTable>
    {
        public override MetadataTableType Type => MetadataTableType.MethodImpl;

        public override MethodImplTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            MethodImplTable methodImplTable = new MethodImplTable();
            methodImplTable.Class = CheckIndexFromWhatever(parser, baseAddr, ref offset, methodImplTable.Class);
            methodImplTable.MethodBody = CheckIndexFromWhatever(parser, baseAddr, ref offset, methodImplTable.MethodBody);
            methodImplTable.MethodBodyType = parser.Bitparser["MethodDefOrRef"].SpecifiedTable(methodImplTable.MethodBody, out int index);
            methodImplTable.MethodBodyIndex = (uint)index;

            methodImplTable.MethodDeclaration = CheckIndexFromWhatever(parser, baseAddr, ref offset, methodImplTable.MethodDeclaration);
            methodImplTable.MethodDeclarationType = parser.Bitparser["MethodDefOrRef"].SpecifiedTable(methodImplTable.MethodDeclaration, out int index2);
            methodImplTable.MethodDeclarationIndex = (uint)index2;
            Position = offset;
            return methodImplTable;
        }
    }
}
