using Parser.Core.Dotnet.Bitmasks;
using System.Runtime.InteropServices;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// II.22.2 Assembly : 0x20 
    /// page 237
    /// 
    /// .assembly directive defined AssemblyTable
    ///     .hash algorithm
    ///     .ver
    ///     .publickey
    ///     .culture
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.Assembly)]
    public struct AssemblyTable
    {
        /// <summary>
        /// a 4-byte constant of type AssemblyHashAlgorithm
        /// 
        /// </summary>
        public AssemblyHashAlgorithm HashAlgId { get; set; }
        /// <summary>
        /// MajorVersion, MinorVersion, BuildNumber, RevisionNumber (each being 2-byte
        /// constants)
        /// </summary>
        public ushort MajorVersion { get; set; }

        public ushort MinorVersion { get; set; }

        public ushort BuildNumber { get; set; }

        public ushort RevisionNumber { get; set; }
        /// <summary>
        /// a 4-byte bitmask of type AssemblyFlags
        /// </summary>
        public AssemblyFlags Flags { get; set; }
        /// <summary>
        /// an index into the Blob heap
        /// can be null or non-null
        /// </summary>
        public dynamic PublicKey { get; set; }
        /// <summary>
        /// an index into the String heap
        /// indexed by Name can be of unlimited length
        /// Assembly Name 不能包含路径,磁盘字母,文件扩展，分号，反斜杠等
        /// </summary>
        public dynamic Name { get; set; }

        public string StringName { get; set; }

        /// <summary>
        /// an index into the String heap
        /// Culture can be null or non-null
        /// </summary>
        public dynamic Culture { get; set; }
        public string StringCulture { get; set; }
    }

    public class AssemblyTableCalc : TableBase<AssemblyTable>
    {
        public override MetadataTableType Type => MetadataTableType.Assembly;

        public override AssemblyTable Create(DotnetParser parser, IntPtr baseAddr)
        {

            int offset = 0;
            AssemblyTable assembly = new AssemblyTable();
            assembly.HashAlgId = (AssemblyHashAlgorithm)Marshal.ReadInt32(baseAddr + offset);
            offset += 4;

            assembly.MajorVersion = ReadUInt16(baseAddr + offset); offset += 2;
            assembly.MinorVersion = ReadUInt16(baseAddr + offset); offset += 2;
            assembly.BuildNumber = ReadUInt16(baseAddr + offset); offset += 2;
            assembly.RevisionNumber = ReadUInt16(baseAddr + offset); offset += 2;
            assembly.Flags = (AssemblyFlags)ReadUInt32(baseAddr + offset); offset += 4;

            assembly.PublicKey = CheckIndexFromBlobStream(parser, baseAddr, ref offset, assembly.PublicKey);
            assembly.Name = CheckIndexFromStringStream(parser, baseAddr, ref offset, assembly.Name);
            assembly.Culture = CheckIndexFromStringStream(parser, baseAddr, ref offset, assembly.Culture);

            assembly.StringName = Marshal.PtrToStringAnsi(parser.GetOffset(parser.StringStreamAddr,assembly.Name));
            assembly.StringCulture = Marshal.PtrToStringAnsi(parser.GetOffset(parser.StringStreamAddr,assembly.Culture));

            Position = offset;
            return assembly;
        }
    }
}
