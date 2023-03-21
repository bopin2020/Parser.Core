using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// 
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.Module)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct ModuleTable
    {
        /// <summary>
        /// a 2-byte value, reserved, shall be zero
        /// </summary>
        public ushort Generation { get; set; }
        /// <summary>
        /// an index into the String heap
        /// 
        /// 2 bytes or 4 bytes 
        /// </summary>
        public dynamic Name { get; set; }

        public string StringName { get; set; }


        /// <summary>
        /// an index into the Guid heap; simply a Guid used to distinguish between two
        /// versions of the same module)
        /// </summary>
        public dynamic Mvid { get; set; }
        /// <summary>
        /// an index into the Guid heap; reserved, shall be zero
        /// 0x0000
        /// </summary>
        public ushort EncId { get; set; }
        /// <summary>
        /// an index into the Guid heap; reserved, shall be zero
        /// 0x0000
        /// </summary>
        public ushort EncBaseId { get; set; }
    }

    public class ModuleCalc : TableBase<ModuleTable>
    {
        public override MetadataTableType Type => MetadataTableType.Module;

        public override ModuleTable Create(DotnetParser parser,IntPtr baseAddr)
        {
            int offset = 0;
            ModuleTable moduleTable = new ModuleTable();
            moduleTable.Generation = ReadUInt16(baseAddr,offset);
            offset += 2;

            moduleTable.Name = CheckIndexFromStringStream(parser, baseAddr, ref offset, moduleTable.Name);
            moduleTable.Mvid = CheckIndexFromGUIDStream(parser, baseAddr, ref offset, moduleTable.Mvid);
            moduleTable.StringName = Marshal.PtrToStringAnsi(parser.GetOffset(parser.StringStreamAddr,moduleTable.Name));
            moduleTable.EncId = ReadUInt16(baseAddr,offset);
            offset += 2;
            moduleTable.EncBaseId = ReadUInt16(baseAddr,offset);
            Position = offset + 2;
            return moduleTable;
        }
    }
}
