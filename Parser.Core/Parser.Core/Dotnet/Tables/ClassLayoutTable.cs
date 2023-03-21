using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// II.22.8 ClassLayout : 0x0F
    /// The ClassLayout table is used to define how the fields of a class or value type shall be laid out by the
    /// CLI
    /// 
    /// Normally, the CLI is free to reorder and/or insert gaps between the fields defined for a class or
    /// value type.
    /// 
    ///  This feature is used to lay out a managed value type in exactly the same way as an
    /// unmanaged C struct, allowing a managed value type to be handed to unmanaged code, which then
    /// accesses the fields exactly as if that block of memory had been laid out by unmanaged code.end
    /// rationale
    /// 
    /// .pack
    /// .size
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.ClassLayout)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct ClassLayoutTable
    {
        /// <summary>
        /// packing size
        /// </summary>
        public ushort PackingSize { get; set; }
        /// <summary>
        /// .size = 0 not mean the class has zero size.
        /// It means that no .size direcive was specified at definition time
        /// in which case, the actual size is calculated from the field types, taking account of
        ///packing size(default or specified) and natural alignment on the target, runtime platform
        /// </summary>
        public uint ClassSize { get; set; }
        /// <summary>
        /// an index into the TypeDef table
        /// </summary>
        public dynamic Parent { get; set; }
    }

    public class ClassLayoutTableCalc : TableBase<ClassLayoutTable>
    {
        public override MetadataTableType Type => MetadataTableType.ClassLayout;

        public override ClassLayoutTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            ClassLayoutTable classLayoutTable = new ClassLayoutTable();

            classLayoutTable.PackingSize = ReadUInt16(baseAddr + offset);
            offset += 2;

            classLayoutTable.ClassSize = ReadUInt32(baseAddr + offset);
            offset += 4;

            classLayoutTable.Parent = CheckIndexFromWhatever(parser, baseAddr, ref offset, classLayoutTable.Parent,parser.GetTableRows(MetadataTableType.TypeDef));
            Position = offset;

            return classLayoutTable;
        }
    }
}
