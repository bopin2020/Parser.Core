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
    /// .module extern
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.ModuleRef)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct ModuleRef
    {
        /// <summary>
        /// an index into the String heap
        /// 
        /// Name should match an entry in the Name column of the File table
        /// </summary>
        public dynamic Name { get; set; }

        public string StringName { get; set; }
    }

    public class ModuleRefCalc : TableBase<ModuleRef>
    {
        public override MetadataTableType Type => MetadataTableType.ModuleRef;

        public override ModuleRef Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            ModuleRef moduleRef = new ModuleRef();
            moduleRef.Name = CheckIndexFromStringStream(parser, baseAddr, ref offset, moduleRef.Name);
            moduleRef.StringName = Marshal.PtrToStringAnsi(parser.GetOffset(parser.StringStreamAddr,moduleRef.Name));
            Position = offset;
            return moduleRef;
        }
    }
}
