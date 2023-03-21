using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MetadataBitmasks = Parser.Core.Dotnet.Bitmasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// II.22.19 File : 0x26 
    /// page 253
    /// 
    /// .file
    /// 
    /// If the File table is empty, then this, by definition, is a single-file assembly. In this
    /// case, the ExportedType table should be empty
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.File)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct FileTable
    {
        /// <summary>
        /// </summary>
        public MetadataBitmasks.FileAttributes Flags { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public dynamic Name { get; set; }

        public string StringName { get; set; }

        /// <summary>
        /// an index into the Blob heap
        /// </summary>
        public dynamic HashValue { get; set; }
    }

    public class FileTableCalc : TableBase<FileTable>
    {
        public override MetadataTableType Type => MetadataTableType.File;

        public override FileTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            FileTable file = new FileTable();
            file.Flags = (MetadataBitmasks.FileAttributes)ReadUInt32(baseAddr + offset); offset += 4;

            file.Name = CheckIndexFromStringStream(parser, baseAddr, ref offset, file.Name);
            file.HashValue = CheckIndexFromBlobStream(parser, baseAddr, ref offset, file.HashValue);

            file.StringName = Marshal.PtrToStringAnsi(parser.GetOffset(parser.StringStreamAddr,file.Name));
            Position = offset;

            return file;
        }
    }
}
