using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    [AttributeUsage(AttributeTargets.Struct)]
    public class MetadataTableTypeDefAttribute : Attribute
    {
        public ushort BitNumber { get; set; }

        public MetadataTableType Type { get; set; }

        public MetadataTableTypeDefAttribute(ushort bitnumber)
        {
            BitNumber = bitnumber;
            Type = (MetadataTableType)bitnumber;
        }

        public MetadataTableTypeDefAttribute(MetadataTableType type)
        {
            BitNumber = (ushort)type;
            Type = type;
        }
    }
}
