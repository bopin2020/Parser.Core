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
        public UInt32 PointerToSymbolTable;
        public UInt32 NumberOfSymbols;
        public UInt16 SizeOfOptionalHeader;
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
    /// </summary>
    public enum DllCharacteristicsType : ushort
    {
        /// <summary>
        /// IMAGE_FILE_RELOCS_STRIPPED
        /// </summary>
        IMAGE_FILE_RELOCS_STRIPPED = 0x0001,
        /// <summary>
        /// IMAGE_FILE_EXECUTABLE_IMAGE
        /// </summary>
        IMAGE_FILE_EXECUTABLE_IMAGE = 0x0002,
        RES_2 = 0x0004,
        RES_3 = 0x0008,
        IMAGE_DLL_CHARACTERISTICS_DYNAMIC_BASE = 0x0040,
        IMAGE_DLL_CHARACTERISTICS_FORCE_INTEGRITY = 0x0080,
        IMAGE_DLL_CHARACTERISTICS_NX_COMPAT = 0x0100,
        IMAGE_DLLCHARACTERISTICS_NO_ISOLATION = 0x0200,
        IMAGE_DLLCHARACTERISTICS_NO_SEH = 0x0400,
        IMAGE_DLLCHARACTERISTICS_NO_BIND = 0x0800,
        RES_4 = 0x1000,
        IMAGE_DLLCHARACTERISTICS_WDM_DRIVER = 0x2000,
        IMAGE_DLLCHARACTERISTICS_TERMINAL_SERVER_AWARE = 0x8000
    }
}
