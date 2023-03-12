using System.Runtime.InteropServices;

namespace Parser.Core.PE
{
    [StructLayout(LayoutKind.Sequential)]
    public struct IMAGE_FILE_HEADER
    {
        /// <summary>
        /// 
        /// </summary>
        public MachineType Machine;
        /// <summary>
        /// Number of sections; indicates size of the Section Table,
        /// which immediately follows the headers
        /// </summary>
        public UInt16 NumberOfSections;
        /// <summary>
        /// Time and date the file was created in seconds since
        /// January 1st 1970 00:00:00 or 0
        /// </summary>
        public UInt32 TimeDateStamp;
        /// <summary>
        /// Always 0 
        /// </summary>
        public UInt32 PointerToSymbolTable;
        /// <summary>
        /// Always 0
        /// </summary>
        public UInt32 NumberOfSymbols;
        /// <summary>
        /// Size of the optional header, the format is described below
        /// </summary>
        public UInt16 SizeOfOptionalHeader;
        /// <summary>
        /// Flags indicating attributes of the file
        /// </summary>
        public UInt16 Characteristics;
    }

    public enum MachineType : ushort
    {
        /// <summary>
        /// The content of this field is assumed to be applicable to any machine type
        /// </summary>
        Unknown = 0x0000,
        /// <summary>
        /// Intel 386 or later processors and compatible processors
        /// </summary>
        I386 = 0x014c,
        R3000 = 0x0162,
        /// <summary>
        ///  MIPS little endian
        /// </summary>
        R4000 = 0x0166,
        R10000 = 0x0168,
        /// <summary>
        /// MIPS little-endian WCE v2
        /// </summary>
        WCEMIPSV2 = 0x0169,
        /// <summary>
        /// Alpha AXP
        /// </summary>
        Alpha = 0x0184,
        /// <summary>
        /// Hitachi SH3
        /// </summary>
        SH3 = 0x01a2,
        /// <summary>
        /// Hitachi SH3 DSP
        /// </summary>
        SH3DSP = 0x01a3,
        /// <summary>
        /// Hitachi SH4
        /// </summary>
        SH4 = 0x01a6,
        /// <summary>
        /// Hitachi SH5
        /// </summary>
        SH5 = 0x01a8,
        /// <summary>
        /// ARM little endian
        /// </summary>
        ARM = 0x01c0,
        /// <summary>
        /// Thumb
        /// </summary>
        Thumb = 0x01c2,
        /// <summary>
        /// ARM Thumb-2 little endian
        /// </summary>
        ARMNT = 0x01c4,
        /// <summary>
        /// Matsushita AM33
        /// </summary>
        AM33 = 0x01d3,
        /// <summary>
        /// Power PC little endian
        /// </summary>
        PowerPC = 0x01f0,
        /// <summary>
        /// Power PC with floating point support
        /// </summary>
        PowerPCFP = 0x01f1,
        /// <summary>
        /// Intel Itanium processor family
        /// </summary>
        IA64 = 0x0200,
        /// <summary>
        /// MIPS16
        /// </summary>
        MIPS16 = 0x0266,
        /// <summary>
        /// Motorola 68000 series
        /// </summary>
        M68K = 0x0268,
        /// <summary>
        /// Alpha AXP 64-bit
        /// </summary>
        Alpha64 = 0x0284,
        /// <summary>
        /// MIPS with FPU
        /// </summary>
        MIPSFPU = 0x0366,
        /// <summary>
        /// MIPS16 with FPU
        /// </summary>
        MIPSFPU16 = 0x0466,
        /// <summary>
        /// EFI byte code
        /// </summary>
        EBC = 0x0ebc,
        /// <summary>
        /// RISC-V 32-bit address space
        /// </summary>
        RISCV32 = 0x5032,
        /// <summary>
        /// RISC-V 64-bit address space
        /// </summary>
        RISCV64 = 0x5064,
        /// <summary>
        /// RISC-V 128-bit address space
        /// </summary>
        RISCV128 = 0x5128,
        /// <summary>
        /// x64
        /// </summary>
        AMD64 = 0x8664,
        /// <summary>
        /// ARM64 little endian
        /// </summary>
        ARM64 = 0xaa64,
        /// <summary>
        /// LoongArch 32-bit processor family
        /// </summary>
        LoongArch32 = 0x6232,
        /// <summary>
        /// LoongArch 64-bit processor family
        /// </summary>
        LoongArch64 = 0x6264,
        /// <summary>
        /// Mitsubishi M32R little endian
        /// </summary>
        M32R = 0x9041
    }
    public enum MagicType : ushort
    {
        IMAGE_NT_OPTIONAL_HDR32_MAGIC = 0x10b,
        IMAGE_NT_OPTIONAL_HDR64_MAGIC = 0x20b
    }

    public enum SubSystemType : ushort
    {
        IMAGE_SUBSYSTEM_UNKNOWN = 0,
        IMAGE_SUBSYSTEM_NATIVE = 1,
        IMAGE_SUBSYSTEM_WINDOWS_GUI = 2,
        IMAGE_SUBSYSTEM_WINDOWS_CUI = 3,
        IMAGE_SUBSYSTEM_POSIX_CUI = 7,
        IMAGE_SUBSYSTEM_WINDOWS_CE_GUI = 9,
        IMAGE_SUBSYSTEM_EFI_APPLICATION = 10,
        IMAGE_SUBSYSTEM_EFI_BOOT_SERVICE_DRIVER = 11,
        IMAGE_SUBSYSTEM_EFI_RUNTIME_DRIVER = 12,
        IMAGE_SUBSYSTEM_EFI_ROM = 13,
        IMAGE_SUBSYSTEM_XBOX = 14

    }

    /// <summary>
    /// A CIL-only DLL sets flag 0x2000 to 1, while a CIL-only .exe has flag 0x2000 set to zero
    /// https://learn.microsoft.com/en-us/windows/win32/debug/pe-format#characteristics
    /// </summary>
    public enum CharacteristicsType : ushort
    {
        /// <summary>
        /// IMAGE_FILE_RELOCS_STRIPPED
        /// </summary>
        IMAGE_FILE_RELOCS_STRIPPED = 0x0001,
        /// <summary>
        /// Image only. This indicates that the image file is valid and can be run. If this flag is not set, it indicates a linker error
        /// </summary>
        IMAGE_FILE_EXECUTABLE_IMAGE = 0x0002,
        /// <summary>
        /// COFF line numbers have been removed. This flag is deprecated and should be zero
        /// </summary>
        IMAGE_FILE_LINE_NUMS_STRIPPED = 0x0004,
        /// <summary>
        /// COFF symbol table entries for local symbols have been removed. This flag is deprecated and should be zero
        /// </summary>
        IMAGE_FILE_LOCAL_SYMS_STRIPPED = 0x0008,
        /// <summary>
        /// 
        /// </summary>
        IMAGE_FILE_AGGRESSIVE_WS_TRIM = 0x0010,
        IMAGE_FILE_LARGE_ADDRESS_AWARE = 0x0020,
        /// <summary>
        /// This flag is reserved for future use
        /// </summary>
        IMAGE_DLL_CHARACTERISTICS_DYNAMIC_BASE = 0x0040,
        /// <summary>
        /// IMAGE_FILE_BYTES_REVERSED_LO
        /// </summary>
        IMAGE_DLL_CHARACTERISTICS_FORCE_INTEGRITY = 0x0080,
        /// <summary>
        /// Shall be one if and only if
        ///COMIMAGE_FLAGS_32BITREQUIRED is
        ///one
        ///
        /// </summary>
        IMAGE_FILE_32BIT_MACHINE = 0x0100,
        IMAGE_DLL_CHARACTERISTICS_NX_COMPAT = 0x0100,
        /// <summary>
        /// Debugging information is removed from the image file
        /// </summary>
        IMAGE_DLLCHARACTERISTICS_NO_ISOLATION = 0x0200,
        /// <summary>
        /// If the image is on removable media, fully load it and copy it to the swap file
        /// </summary>
        IMAGE_DLLCHARACTERISTICS_NO_SEH = 0x0400,
        /// <summary>
        /// If the image is on network media, fully load it and copy it to the swap file.
        /// </summary>
        IMAGE_DLLCHARACTERISTICS_NO_BIND = 0x0800,
        /// <summary>
        /// The image file is a system file, not a user program.
        /// </summary>
        IMAGE_FILE_SYSTEM = 0x1000,
        /// <summary>
        /// The image file is a dynamic-link library (DLL)
        /// </summary>
        IMAGE_FILE_DLL = 0x2000,
        IMAGE_DLLCHARACTERISTICS_WDM_DRIVER = 0x2000,
        /// <summary>
        /// The file should be run only on a uniprocessor machine.
        /// </summary>
        IMAGE_FILE_UP_SYSTEM_ONLY = 0x4000,
        /// <summary>
        /// Big endian: the MSB precedes the LSB in memory. This flag is deprecated and should be zero
        /// </summary>
        IMAGE_FILE_BYTES_REVERSED_HI = 0x8000,
        IMAGE_DLLCHARACTERISTICS_TERMINAL_SERVER_AWARE = 0x8000
    }
}
