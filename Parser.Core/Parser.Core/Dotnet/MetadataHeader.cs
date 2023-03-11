namespace Parser.Core.Dotnet
{
    /// <summary>
    /// It's a dynamic header,it makes no sense declaring a structure
    /// </summary>
    public struct MetadataHeader
    {
        /// <summary>
        /// 0x424A5342
        /// BSJB
        /// </summary>
        public int Signature { get; set; }
        /// <summary>
        /// 0x0001
        /// </summary>
        public short MajorVersion { get; set; }
        /// <summary>
        /// 0x0001
        /// </summary>
        public short MinorVersion { get; set; }
        /// <summary>
        /// 0x00000000
        /// </summary>
        public int Reserved { get; set; }
        /// <summary>
        /// 0x0c for  v4.0.30319
        /// </summary>
        public int VersionLength { get; set; }

        /// <summary>
        /// 4字节边界对齐
        /// </summary>
        public string VersionString { get; set; }

        public short Flags { get; set; }

        /// <summary>
        /// < 65535
        /// 过多的Stream CFF显示会截断
        /// </summary>
        public short NumberOfStreams { get; set; }

        public List<StreamHeader> StreamHeaders { get; set; }

        /// Stream Headers之后就是Stream的数据
    }
}
