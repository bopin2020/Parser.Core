using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// 
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.Module)]
    [MetadataTableLevel(MetadataTableLevel.Sophisticated)]
    public struct ModuleTable
    {
        /// <summary>
        /// a 2-byte value, reserved, shall be zero
        /// </summary>
        public short Generation { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public int Name { get; set; }
        /// <summary>
        /// an index into the Guid heap; simply a Guid used to distinguish between two
        /// versions of the same module)
        /// </summary>
        public int Mvid { get; set; }
        /// <summary>
        /// an index into the Guid heap; reserved, shall be zero
        /// </summary>
        public int EncId { get; set; }
        /// <summary>
        /// an index into the Guid heap; reserved, shall be zero
        /// </summary>
        public int EncBaseId { get; set; }
    }
}
