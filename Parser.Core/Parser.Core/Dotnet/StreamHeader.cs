namespace Parser.Core.Dotnet
{
    public struct StreamHeader
    {
        /// <summary>
        /// 距 BSJB  Metadata Root的 RVA
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// Stream Size
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 4的倍数为边界
        /// </summary>
        public string Name { get; set; }

        public IntPtr BaseAddress { get; set; }
    }
}
