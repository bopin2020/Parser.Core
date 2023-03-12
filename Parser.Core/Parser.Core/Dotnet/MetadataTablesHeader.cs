namespace Parser.Core.Dotnet
{
    /// <summary>
    /// #~  structrue
    /// II.24.2.6 #~ stream
    /// ecma335 page 299
    /// </summary>
    public struct MetadataTablesHeader
    {
        /// <summary>
        /// Reserved, always 0
        /// </summary>
        public int Reserved { get; set; }

        /// <summary>
        /// Major version of table schemata; shall be 0x02
        /// </summary>
        public byte MajorVersion { get; set; }
        /// <summary>
        /// Minor version of table schemata; shall be 0x00
        /// </summary>
        public byte MinorVersion { get; set; }
        /// <summary>
        /// Bit vector for heap sizes
        /// If bit 0 is set, indexes into the “#String” heap are 4 bytes wide
        /// if bit 1 is set, indexes into the “#GUID” heap are 4 bytes wide
        /// if bit 2 is set, indexes into the “#Blob” heap are 4 bytes wide
        /// 
        /// Conversely, if the HeapSize bit for a particular heap is not set, indexes into that heap are 2 bytes wide
        /// </summary>
        public byte HeapOffsetSizes { get; set; }
        /// <summary>
        /// Reserved, always 1
        /// </summary>
        public byte Reserved2 { get; set; }

        /// <summary>
        /// Bit vector of present tables, let n be the number of bits that are 1
        /// It's a bitmask-qword that tells us which MetaData Tables are present in the assembly
        /// 
        /// qword 8字节 共计64bit  共计64张表
        /// The Valid field is a 64-bit bitvector that has a specific bit set for each table that is stored in the stream
        /// 
        /// For example when the DeclSecuritytable is present in the logical metadata, bit 0x0e should be set in the Valid vector
        /// 
        /// </summary>
        public long Valid { get; set; }
        /// <summary>
        /// Bit vector of sorted tables
        /// Also a bitmask-qword. It tells us which tables are sorted
        /// </summary>
        public long Sorted { get; set; }
    }
}
