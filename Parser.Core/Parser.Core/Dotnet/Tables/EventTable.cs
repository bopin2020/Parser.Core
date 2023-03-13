using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// Events are treated within metadata much like Properties
    /// 
    /// There are two required methods (add_ and remove_) raise_
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.Event)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct EventTable
    {
        /// <summary>
        /// an index into the TypeDef table
        /// </summary>
        public EventAttributes EventFlags { get; set; }

        /// <summary>
        /// an index into the String heap
        /// </summary>
        public int Name { get; set; }
        /// <summary>
        /// an index into a TypeDef, a TypeRef, or TypeSpec table
        /// </summary>
        public int EventType { get; set; }
    }
}
