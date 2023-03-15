using Parser.Core.Dotnet.Bitmasks;

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
        public short MajorVersion { get; set; }

        public short MinorVersion { get; set; }

        public short BuildNumber { get; set; }

        public short RevisionNumber { get; set; }
        /// <summary>
        /// a 4-byte bitmask of type AssemblyFlags
        /// </summary>
        public AssemblyFlags Flags { get; set; }
        /// <summary>
        /// an index into the Blob heap
        /// can be null or non-null
        /// </summary>
        public int PublicKey { get; set; }
        /// <summary>
        /// an index into the String heap
        /// indexed by Name can be of unlimited length
        /// Assembly Name 不能包含路径,磁盘字母,文件扩展，分号，反斜杠等
        /// </summary>
        public int Name { get; set; }
        /// <summary>
        /// an index into the String heap
        /// Culture can be null or non-null
        /// </summary>
        public int Culture { get; set; }
    }
}
