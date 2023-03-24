using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Parser.Core.Dotnet.Bitmasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// The ImplMap table holds information about unmanaged methods that can be reached from managed
    /// code, using PInvoke dispatch
    /// 
    /// Each row of the ImplMap table associates a row in the MethodDef table (MemberForwarded) with the
    /// name of a routine(ImportName) in some unmanaged DLL(ImportScope)
    /// 
    /// .pinvokeimpl
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.ImplMap)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct ImplMapTable
    {
        /// <summary>
        /// a 2-byte bitmask of type PInvokeAttributes
        /// </summary>
        public PInvokeAttributes MappingFlags { get; set; }
        /// <summary>
        /// (an index into the Field or MethodDef table; more precisely, a
        /// MemberForwarded
        /// 
        /// However, it only ever indexes the
        /// MethodDef table, since Field export is not supported
        /// </summary>
        public dynamic MemberForwarded { get; set; }
        public MetadataTableType MemberForwardedType { get; set; }
        public uint MemberForwardedIndex { get; set; }
        /// <summary>
        /// an index into the String heap
        public dynamic ImportName { get; set; }

        public string StringImportName { get; set; }

        /// <summary>
        /// an index into the ModuleRef table
        /// </summary>
        public dynamic ImportScope { get; set; }
    }

    public class ImplMapTableCalc : TableBase<ImplMapTable>
    {
        public override MetadataTableType Type => MetadataTableType.ImplMap;

        public override ImplMapTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            ImplMapTable implMap = new ImplMapTable();
            implMap.MappingFlags = (PInvokeAttributes)ReadUInt16(baseAddr + offset);
            offset += 2;

            implMap.MemberForwarded = CheckIndexFromWhatever(parser, baseAddr, ref offset, implMap.MemberForwarded);
            implMap.MemberForwardedType = parser.Bitparser["MemberForwarded"].SpecifiedTable(implMap.MemberForwarded, out int index);
            implMap.MemberForwardedIndex = (uint)index;

            implMap.ImportName = CheckIndexFromStringStream(parser, baseAddr, ref offset, implMap.ImportName);
            implMap.ImportScope = CheckIndexFromWhatever(parser, baseAddr, ref offset, implMap.ImportScope);
            implMap.StringImportName = Marshal.PtrToStringAnsi(parser.GetOffset(parser.StringStreamAddr,implMap.ImportName));
            Position = offset;
            return implMap;
        }
    }
}
