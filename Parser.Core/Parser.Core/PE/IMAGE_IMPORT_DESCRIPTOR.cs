using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.PE
{
    /// <summary>
    /// II.25.3.1 Import Table and Import Address Table (IAT)
    /// </summary>
    public struct IMAGE_IMPORT_DESCRIPTOR
    {
        public uint OriginalFirstThunk { get; set; }

        public uint TimeDateStamp { get; set; }

        public uint ForwarderChain { get; set; }

        public uint Name { get; set; }

        public uint FirstThunk { get; set; }
    }

    public struct IMAGE_THUNK_DATA
    {

    }
}
