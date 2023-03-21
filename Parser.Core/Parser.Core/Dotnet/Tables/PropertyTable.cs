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
    /// page 268
    /// 
    /// Properties within metadata are best viewed as a means to gather together collections of methods
    /// defined on a class
    /// 
    /// The methods are typically get_ and set_
    /// methods, already defined on the class, and inserted like any other methods into the MethodDef table
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.Property)]
    [MetadataTableLevel(MetadataTableLevel.Sophisticated)]
    public struct PropertyTable
    {
        /// <summary>
        /// 
        /// </summary>
        public PropertyAttributes Flags { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public dynamic Name { get; set; }

        public string StringName { get; set; }

        /// <summary>
        /// an index into the Blob heap
        /// 
        /// The name of this column is misleading. It does
        ///not index a TypeDef or TypeRef table—instead it indexes the signature in the Blob
        ///heap of the Property
        /// </summary>
        public dynamic Type { get; set; }

    }

    public class PropertyTableCalc : TableBase<PropertyTable>
    {
        public override MetadataTableType Type => MetadataTableType.Property;

        public override PropertyTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            PropertyTable property = new PropertyTable();
            property.Flags = (PropertyAttributes)ReadUInt16(baseAddr + offset);
            offset += 2;
            property.Name = CheckIndexFromStringStream(parser, baseAddr, ref offset, property.Name);
            property.StringName = Marshal.PtrToStringAnsi(parser.GetOffset(parser.StringStreamAddr, property.Name));
            property.Type = CheckIndexFromBlobStream(parser, baseAddr, ref offset, property.Type);
            Position = offset;
            return property;
        }
    }
}
