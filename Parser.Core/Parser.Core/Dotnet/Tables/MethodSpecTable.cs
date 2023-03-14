using System;
using System.Collections.Generic;
using System.Linq;
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
        public int Method { get; set; }
        /// <summary>
        /// an index into the Blob heap holding the signature of this instantiation
        /// </summary>
        public int Instantiation { get; set; }
        /// <summary>
        /// an index into the Event or Property table
        /// </summary>
        public int Association { get; set; }
    }
}
