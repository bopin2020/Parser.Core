using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    public class MetadataTableContent
    {
        public MetadataTableType Type { get; set; }

        public int RowLength { get; set; }

        public Lazy<List<object>> Rows { get; set; } = new();
    }
}
