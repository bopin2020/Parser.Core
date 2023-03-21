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
    /// page 268
    /// 
    /// .property
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.PropertyMap)]
    [MetadataTableLevel(MetadataTableLevel.Sophisticated)]
    public struct PropertyMap
    {
        /// <summary>
        /// an index into the TypeDef table
        /// </summary>
        public dynamic Parent { get; set; }
        /// <summary>
        /// an index into the Property table
        /// </summary>
        public dynamic PropertyList { get; set; }
    }

    public class PropertyMapCalc : TableBase<PropertyMap>
    {
        public override MetadataTableType Type => MetadataTableType.PropertyMap;

        public override PropertyMap Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            PropertyMap propertyMap = new PropertyMap();
            propertyMap.Parent = CheckIndexFromWhatever(parser, baseAddr, ref offset, propertyMap.Parent);
            propertyMap.PropertyList = CheckIndexFromWhatever(parser, baseAddr, ref offset, propertyMap.PropertyList);
            Position = offset;
            return propertyMap;
        }
    }
}
