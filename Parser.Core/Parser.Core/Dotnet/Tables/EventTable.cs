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
        public dynamic Name { get; set; }

        public string StringName { get; set; }

        /// <summary>
        /// an index into a TypeDef, a TypeRef, or TypeSpec table
        /// more precisely, a TypeDefOrRef coded index
        /// This corresponds to the Type of the
        /// Event; it is not the Type that owns this event
        /// </summary>
        public dynamic EventType { get; set; }
    }

    public class EventTableCalc : TableBase<EventTable>
    {
        public override MetadataTableType Type => MetadataTableType.Event;

        public override EventTable Create(DotnetParser parser, IntPtr baseAddr)
        {

            int offset = 0;
            EventTable _event = new EventTable();
            _event.EventFlags = (EventAttributes)ReadUInt16(baseAddr + offset);
            offset += 2;

            _event.Name = CheckIndexFromStringStream(parser, baseAddr, ref offset, _event.Name);
            _event.StringName = Marshal.PtrToStringAnsi(parser.GetOffset(parser.StringStreamAddr,_event.Name));

            _event.EventType = CheckIndexFromWhatever(parser, baseAddr, ref offset, _event.EventType);

            Position = offset;
            return _event;
        }
    }
}
