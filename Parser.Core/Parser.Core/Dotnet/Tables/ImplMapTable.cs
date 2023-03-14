using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public int MemberForwarded { get; set; }
        /// <summary>
        /// an index into the String heap
        public int ImportName { get; set; }

        /// <summary>
        /// an index into the ModuleRef table
        /// </summary>
        public int ImportScope { get; set; }
    }
}
