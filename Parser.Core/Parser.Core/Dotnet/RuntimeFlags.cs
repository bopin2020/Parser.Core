namespace Parser.Core.Dotnet
{
    /// <summary>
    /// II.25.3.3.1 Runtime flags 
    /// The following flags describe this runtime image and are used by the loader. All unspecified bits should be zero
    /// </summary>
    public enum RuntimeFlags : uint
    {
        /// <summary>
        /// Shall be 1
        /// </summary>
        COMIMAGE_FLAGS_ILONLY = 0x00000001,
        /// <summary>
        /// Image can only be loaded into a 32-bit process,
        /// for instance if there are 32-bit vtablefixups, or
        /// casts from native integers to int32.
        /// CLI implementations that have 64-bit native
        /// integers shall refuse loading binaries with this flag set
        /// </summary>
        COMIMAGE_FLAGS_32BITREQUIRED = 0x00000002,
        /// <summary>
        /// Image has a strong name signature
        /// </summary>
        COMIMAGE_FLAGS_STRONGNAMESIGNED = 0x00000008,
        /// <summary>
        /// Shall be 0
        /// </summary>
        COMIMAGE_FLAGS_NATIVE_ENTRYPOINT = 0x00000010,
        /// <summary>
        /// Complete CLI components (metadata and CIL instructions) are stored in a subset of the current
        /// Portable Executable (PE) File Format (§II.25). Because of this heritage, some of the fields in the
        /// physical representation of metadata have fixed values. When writing these fields it is best that they be
        /// set to the value indicated, on reading they should be ignored.
        /// </summary>
        COMIMAGE_FLAGS_TRACKDEBUGDATA = 0x00010000,
    }
}
