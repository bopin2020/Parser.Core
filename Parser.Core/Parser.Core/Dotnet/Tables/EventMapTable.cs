using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// II.22.12 EventMap : 0x12 
    /// page 246
    /// 
    /// Note that EventMap info does not directly influence runtime behavior; what counts is the information
    /// stored for each method that the event comprises.
    /// .event directive
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.EventMap)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct EventMapTable
    {
        /// <summary>
        /// an index into the TypeDef table
        /// </summary>
        public int Parent { get; set; }

        /// <summary>
        /// an index into the Event table
        /// </summary>
        public int EventList { get; set; }
    }

    public class EventMapTableCalc : TableBase<EventMapTable>
    {
        public override MetadataTableType Type => MetadataTableType.EventMap;

        public override EventMapTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            throw new Exception();
        }
    }
}
