namespace Parser.Core.Dotnet.Tables
{
    [MetadataTableTypeDef(MetadataTableType.Assembly)]
    public struct AssemblyTable
    {
        /// <summary>
        /// a 4-byte constant of type AssemblyHashAlgorithm
        /// 
        /// </summary>
        public int HashAlgId { get; set; }

        public short MajorVersion { get; set; }

        public short MinorVersion { get; set; }

        public short BuildNumber { get; set; }

        public short RevisionNumber { get; set; }

        public AssemblyFlags Flags { get; set; }

        public int PublicKey { get; set; }

        public int Name { get; set; }
    }
}
