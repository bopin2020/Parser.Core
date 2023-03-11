namespace Parser.Core.Dotnet
{
    /// <summary>
    /// https://www.ntcore.com/files/dotnetformat.htm#NETPEFiles
    /// 
    /// CLI header
    /// </summary>
    public struct IMAGE_COR20_HEADER
    {
        /// <summary>
        /// Size of the structure
        /// </summary>
        public int cb { get; set; }
        /// <summary>
        /// MajorRuntimeVersion and MinorRuntimeVersion Version of the CLR Runtime
        /// </summary>
        public short MajorRuntimeVersion { get; set; }
        public short MinorRuntimeVersion { get; set; }
        /// <summary>
        /// Metadata
        /// </summary>
        public IMAGE_DATA_DIRECTORY Metadata { get; set; }

        public RuntimeFlags Flags { get; set; }
        /// <summary>
        /// if RuntimeFlags.COMIMAGE_FLAGS_NATIVE_ENTRYPOINT 没有设置  表示托管入口点的Token 0x06xxx
        /// Native EntryPoint设置了 表示到 native entrypoint的 RVA
        /// </summary>
        public int EntryPointToken { get; set; }

        /// <summary>
        /// A Data Directory for the Resources. These resources are referenced in the MetaData
        /// </summary>
        public IMAGE_DATA_DIRECTORY Resources { get; set; }
        /// <summary>
        /// A Data Directory for the Strong Name Signature
        /// It's a signature to uniquely identify .NET Assemblies
        /// This section is only present when the COMIMAGE_FLAGS_STRONGNAMESIGNED is set
        /// </summary>
        public IMAGE_DATA_DIRECTORY StrongNameSignature { get; set; }
        /// <summary>
        /// CodeManagerTable Always 0
        /// </summary>
        public IMAGE_DATA_DIRECTORY CodeManagerTable { get; set; }
        /// <summary>
        /// virtual functions which need to be representend in a v-table
        /// These v-tables are laid out by the compiler, not by the runtime
        /// Finding the correct v-table slot and calling indirectly
        /// through the value held in that slot is also done by the compiler
        /// The VtableFixups field in the runtime header contains the location and size of an array of Vtable Fixups (§14.5.1)
        /// 
        /// V-tables shall be emitted into a read-write section of the PE file
        /// Each entry in this array describes a contiguous array of v-table slots of the specified size
        /// 
        /// Each slot starts out initialized to the metadata token value for the method they need to call
        /// 
        /// At image load time, the runtime Loader will turn each entry into a pointer to machine code
        /// for the CPU and can be called directly.". And this is everything you'll find here about VTableFixups
        /// </summary>
        public IMAGE_DATA_DIRECTORY VTableFixups { get; set; }
        /// <summary>
        /// ExportAddressTableJumps Always 0.
        /// </summary>
        public IMAGE_DATA_DIRECTORY ExportAddressTableJumps { get; set; }
        /// <summary>
        /// ManagedNativeHeader Always 0 in normal .NET assemblies, only present in native images
        /// </summary>
        public IMAGE_DATA_DIRECTORY ManagedNativeHeader { get; set; }
    }
}
