using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet
{
    public struct IMAGE_DATA_DIRECTORY
    {
        public int MetaDataRVA { get; set; }
        public int MetadataSize { get; set; }
    }
}
