namespace Parser.Core.Dotnet.Bitmasks
{
    [Flags]
    /// <summary>
    /// Metadata logical format:
    /// other structures: Bitmasks and flags
    /// 
    /// ecma page 275
    /// </summary>
    public enum AssemblyHashAlgorithm : uint
    {
        None = 0x0000,
        /// <summary>
        /// MD5
        /// </summary>
        Reserved = 0x8003,
        SHA1 = 0x8004
    }
    [Flags]
    /// <summary>
    /// II.23.1.2 Values for AssemblyFlags 
    /// page 275
    /// 
    /// https://www.ntcore.com/files/dotnetformat.htm undocumented AssemblyFlags
    /// </summary>
    public enum AssemblyFlags : uint
    {
        /// <summary>
        /// Processor Architecture unspecified
        /// </summary>
        None = 0x0000,
        /// <summary>
        /// The assembly reference holds the full (unhashed) public key
        /// </summary>
        PublicKey = 0x0001,
        Shift = 0x0004,
        /// <summary>
        /// Processor Architecture: neutral (PE32)
        /// </summary>
        MSIL = 0x0010,
        /// <summary>
        /// Processor Architecture: x86 (PE32)
        /// </summary>
        X86 = 0x0020,
        IA64 = 0x0030,
        AMD64 = 0x0040,
        Mask = 0x0070,
        Specified = 0x0080,
        FullMask = 0x00f0,
        /// <summary>
        /// The implementation of this assembly used at runtime is
        /// not expected to match the version seen at compile time
        /// </summary>
        Retargetable = 0x0100,
        /// <summary>
        /// Reserved (a conforming implementation of the CLI
        /// can ignore this setting on read; some implementations
        /// might use this bit to indicate that a CIL-to-native-code
        /// compiler should not generate optimized code)
        /// </summary>
        DisableJITcompileOptimizer = 0x4000,
        /// <summary>
        /// Reserved (a conforming implementation of the CLI
        /// can ignore this setting on read; some implementations
        /// might use this bit to indicate that a CIL-to-native-code
        /// compiler should generate CIL-to-native code map)
        /// </summary>
        EnableJITcompileTracking = 0x8000
    }
    [Flags]
    /// <summary>
    /// Flags for events
    /// </summary>
    public enum EventAttributes : ushort
    {
        /// <summary>
        /// Event is special
        /// </summary>
        SpecialName = 0x0200,
        /// <summary>
        /// CLI provides 'special' behavior, depending upon the name of the event
        /// </summary>
        RTSpecialName = 0x0400
    }
    [Flags]
    /// <summary>
    /// Flags for fields
    /// </summary>
    public enum FieldAttributes : ushort
    {
        #region FieldAccessMask

        /// <summary>
        /// These 3 bits contain one of the following values
        /// </summary>
        FieldAccessMask = 0x0007,
        /// <summary>
        /// Member not referenceable
        /// then this row is ignored completely in
        /// duplicate checking
        /// </summary>
        CompilerControlled = 0x0000,
        /// <summary>
        /// Accessible only by the parent type
        /// </summary>
        Private = 0x0001,
        /// <summary>
        /// Accessible by sub-types only in this Assembly
        /// </summary>
        FamANDAssem = 0x0002,
        /// <summary>
        /// Accessibly by anyone in the Assembly
        /// </summary>
        Assembly = 0x0003,
        /// <summary>
        /// Accessible only by type and sub-types
        /// </summary>
        Family = 0x0004,
        /// <summary>
        /// Accessibly by sub-types anywhere, plus anyone in assembly
        /// </summary>
        FamORAssem = 0x0005,
        /// <summary>
        /// Accessibly by anyone who has visibility to this scope field
        /// contract attributes
        /// </summary>
        Public = 0x0006,

        #endregion

        /// <summary>
        /// Defined on type, else per instance
        /// </summary>
        Static = 0x0010,
        /// <summary>
        /// Field can only be initialized, not written to after init
        /// </summary>
        InitOnly = 0x0020,
        /// <summary>
        /// Value is compile time constant
        /// const  常量  static也应该被设置
        /// </summary>
        Literal = 0x0040,
        /// <summary>
        /// Reserved (to indicate this field should not be serialized when
        /// type is remoted)
        /// </summary>
        NotSerialized = 0x0080,
        /// <summary>
        /// Field is special
        /// </summary>
        SpecialName = 0x0200,
        /// <summary>
        /// Implementation is forwarded through PInvoke.
        /// </summary>
        PInvokeImpl = 0x2000,
        /// <summary>
        /// CLI provides 'special' behavior, depending upon the name of the
        /// field
        /// </summary>
        RTSpecialName = 0x0400,
        /// <summary>
        /// Field has marshalling information
        /// 
        /// field 有此标识  在FieldMarshal table有对应的索引 
        /// </summary>
        HasFieldMarshal = 0x1000,
        /// <summary>
        /// Field has default
        /// 
        /// Constant Table 有对应索引
        /// </summary>
        HasDefault = 0x8000,
        /// <summary>
        /// Field has RVA
        /// 
        /// Field's RVA Table
        /// </summary>
        HasFieldRVA = 0x0100
    }
    [Flags]
    /// <summary>
    /// Flags for files
    /// </summary>
    public enum FileAttributes : uint
    {
        /// <summary>
        /// This is not a resource file
        /// </summary>
        ContainsMetaData = 0x0000,
        /// <summary>
        /// This is a resource file or other non-metadata-containing file
        /// </summary>
        ContainsNoMetaData = 0x0001,
    }
    [Flags]
    /// <summary>
    /// Flags for Generic Parameters [GenericPara mAttributes]
    /// </summary>
    public enum GenericParamAttributes : ushort
    {
        /// <summary>
        /// These 2 bits contain one of the following values:
        /// </summary>
        VarianceMask = 0x0003,
        /// <summary>
        /// The generic parameter is non-variant and has no special
        /// constraints
        /// </summary>
        None = 0x0000,
        /// <summary>
        /// The generic parameter is covariant
        /// </summary>
        Covariant = 0x0001,
        /// <summary>
        /// The generic parameter is contravariant
        /// </summary>
        Contravariant = 0x0002,
        /// <summary>
        /// These 3 bits contain one of the following values:
        /// </summary>
        SpecialConstraintMask = 0x001C,
        /// <summary>
        /// The generic parameter has the class special constraint
        /// </summary>
        ReferenceTypeConstraint = 0x0004,
        /// <summary>
        /// The generic parameter has the valuetype special
        /// constraint
        /// </summary>
        NotNullableValueTypeConstraint = 0x0008,
        /// <summary>
        /// The generic parameter has the .ctor special constraint
        /// </summary>
        DefaultConstructorConstraint = 0x0010
    }
    [Flags]
    public enum PInvokeAttributes : ushort
    {
        /// <summary>
        /// PInvoke is to use the member name as specified
        /// </summary>
        NoMangle = 0x0001,
        /// <summary>
        /// This is a resource file or other non-metadata-containing file.
        /// These 2 bits contain one of the following values
        /// </summary>
        CharSetMask = 0x0006,
        CharSetNotSpec = 0x0000,
        CharSetAnsi = 0x0002,
        CharSetUnicode = 0x0004,
        CharSetAuto = 0x0006,
        /// <summary>
        /// Information about target function. Not relevant for fields
        /// </summary>
        SupportsLastError = 0x0040,
        /// <summary>
        /// These 3 bits contain one of the following values:
        /// </summary>
        CallConvMask = 0x0700,
        CallConvPlatformapi = 0x0100,
        CallConvCdecl = 0x0200,
        CallConvStdcall = 0x0300,
        CallConvThiscall = 0x0400,
        CallConvFastcall = 0x0500

    }
    [Flags]
    /// <summary>
    /// Flags for ManifestResource
    /// </summary>
    public enum ManifestResourceAttributes : uint
    {
        /// <summary>
        /// These 3 bits contain one of the following values:
        /// </summary>
        VisibilityMask = 0x0007,
        /// <summary>
        /// The Resource is exported from the Assembly
        /// </summary>
        Public = 0x0001,
        /// <summary>
        /// The Resource is private to the Assembly
        /// </summary>
        Private = 0x0002
    }
    [Flags]
    /// <summary>
    /// Flags for methods
    /// </summary>
    public enum MethodAttributes
    {
        /// <summary>
        /// These 3 bits contain one of the following values
        /// </summary>
        MemberAccessMask = 0x0007,
        /// <summary>
        /// Member not referenceable
        /// </summary>
        CompilerControlled = 0x0000,
        /// <summary>
        /// Accessible only by the parent type
        /// </summary>
        Private = 0x0001,
        /// <summary>
        /// Accessible by sub-types only in this Assembly
        /// </summary>
        FamANDAssem = 0x0002,
        /// <summary>
        /// Accessibly by anyone in the Assembly
        /// </summary>
        Assem = 0x0003,
        /// <summary>
        /// Accessible only by type and sub-types
        /// </summary>
        Family = 0x0004,
        /// <summary>
        /// Accessibly by sub-types anywhere, plus anyone in assembly
        /// </summary>
        FamORAssem = 0x0005,
        /// <summary>
        /// Accessibly by anyone who has visibility to this scope
        /// </summary>
        Public = 0x0006,
        /// <summary>
        /// Defined on type, else per instance
        /// </summary>
        Static = 0x0010,
        /// <summary>
        /// Method cannot be overridden
        /// </summary>
        Final = 0x0020,
        /// <summary>
        /// Method is virtual
        /// </summary>
        Virtual = 0x0040,
        /// <summary>
        /// Method hides by name+sig, else just by name
        /// </summary>
        HideBySig = 0x0080,
        /// <summary>
        /// Use this mask to retrieve vtable attributes. This bit contains
        /// one of the following values:
        /// </summary>
        VtableLayoutMask = 0x0100,
        /// <summary>
        /// Method reuses existing slot in vtable
        /// </summary>
        ReuseSlot = 0x0000,
        /// <summary>
        /// Method always gets a new slot in the vtable
        /// </summary>
        NewSlot = 0x0100,
        /// <summary>
        /// Method can only be overriden if also accessible
        /// </summary>
        Strict = 0x0200,
        /// <summary>
        /// Method does not provide an implementation
        /// </summary>
        Abstract = 0x0400,
        /// <summary>
        /// Method is special
        /// </summary>
        SpecialName = 0x0800,
        /// <summary>
        /// Interop attributes
        /// Implementation is forwarded through PInvoke
        /// </summary>
        PInvokeImpl = 0x2000,
        /// <summary>
        /// Reserved: shall be zero for conforming implementations
        /// </summary>
        UnmanagedExport = 0x0008,
        /// <summary>
        /// CLI provides 'special' behavior, depending upon the name of
        /// the method
        /// </summary>
        RTSpecialName = 0x1000,
        /// <summary>
        /// Method has security associate with it
        /// </summary>
        HasSecurity = 0x4000,
        /// <summary>
        /// Method calls another method containing security code
        /// </summary>
        RequireSecObject = 0x8000
    }

    [Flags]
    /// <summary>
    /// Flags for methods
    /// </summary>
    public enum MethodImplAttributes
    {
        /// <summary>
        /// These 2 bits contain one of the following values
        /// </summary>
        CodeTypeMask = 0x0003,
        /// <summary>
        /// Method impl is CIL
        /// </summary>
        IL = 0x0000,
        /// <summary>
        /// Method impl is native
        /// </summary>
        Native = 0x0001,
        /// <summary>
        /// Reserved: shall be zero in conforming implementations
        /// </summary>
        OPTIL = 0x0002,
        /// <summary>
        /// Method impl is provided by the runtime
        /// </summary>
        Runtime = 0x0003,
        /// <summary>
        /// Flags specifying whether the code is managed or unmanaged.
        /// This bit contains one of the following values
        /// </summary>
        ManagedMask = 0x0004,
        /// <summary>
        /// Method impl is unmanaged, otherwise managed
        /// </summary>
        Unmanaged = 0x0004,
        /// <summary>
        /// Method impl is managed
        /// </summary>
        Managed = 0x0000,
        /// <summary>
        /// Indicates method is defined; used primarily in merge
        /// scenarios
        /// </summary>
        ForwardRef = 0x0010,
        /// <summary>
        /// Reserved: conforming implementations can ignore
        /// </summary>
        PreserveSig = 0x0080,
        /// <summary>
        /// Reserved: shall be zero in conforming implementations
        /// </summary>
        InternalCall = 0x1000,
        /// <summary>
        /// Method is single threaded through the body
        /// </summary>
        Synchronized = 0x0020,
        /// <summary>
        /// Method cannot be inlined
        /// </summary>
        NoInlining = 0x0008,
        /// <summary>
        /// Range check value 
        /// </summary>
        MaxMethodImplVal = 0xffff,
        /// <summary>
        /// Method will not be optimized when generating native code
        /// </summary>
        NoOptimization = 0x0040
    }

    [Flags]
    /// <summary>
    /// Flags for MethodSemantics
    /// </summary>
    public enum MethodSemanticsAttributes : ushort
    {
        /// <summary>
        /// Setter for property
        /// </summary>
        Setter = 0x0001,
        /// <summary>
        /// Getter for property
        /// </summary>
        Getter = 0x0002,
        /// <summary>
        /// Other method for property or event
        /// </summary>
        Other = 0x0004,
        /// <summary>
        /// AddOn method for event. This refers to the required add_
        /// method for events. 
        /// </summary>
        AddOn = 0x0008,
        /// <summary>
        /// RemoveOn method for event. . This refers to the required
        /// remove_ method for events
        /// </summary>
        RemoveOn = 0x0010,
        /// <summary>
        /// Fire method for event. This refers to the optional raise_
        /// method for events
        /// </summary>
        Fire = 0x0020
    }
    [Flags]
    /// <summary>
    /// Flags for params
    /// </summary>
    public enum ParamAttributes : ushort
    {
        /// <summary>
        /// Param is [In]
        /// </summary>
        In = 0x0001,
        /// <summary>
        /// Param is [out]
        /// </summary>
        Out = 0x0002,
        /// <summary>
        /// Param is optional
        /// </summary>
        Optional = 0x0010,
        /// <summary>
        /// Param has default value
        /// </summary>
        HasDefault = 0x1000,
        /// <summary>
        /// Param has FieldMarshal
        /// </summary>
        HasFieldMarshal = 0x2000,
        /// <summary>
        /// Reserved: shall be zero in a conforming implementation
        /// </summary>
        Unused = 0xcfe0
    }

    [Flags]
    public enum PropertyAttributes : ushort
    {
        /// <summary>
        /// Property is specia
        /// </summary>
        SpecialName = 0x0200,
        /// <summary>
        /// Runtime(metadata internal APIs) should check name
        /// encoding
        /// </summary>
        RTSpecialName = 0x0400,
        /// <summary>
        /// Property has default
        /// </summary>
        HasDefault = 0x1000,
        /// <summary>
        /// Reserved: shall be zero in a conforming implementation
        /// </summary>
        Unused = 0xe9ff
    }

    /// <summary>
    /// Flags for types
    /// </summary>
    [Flags]
    public enum TypeAttributes : uint
    {
        #region Visibility

        /// <summary>
        /// Use this mask to retrieve visibility information.
        /// These 3 bits contain one of the following
        /// values
        /// </summary>
        VisibilityMask = 0x00000007,
        /// <summary>
        /// Class has no public scope
        /// </summary>
        NotPublic = 0x00000000,
        /// <summary>
        /// Class has public scope
        /// </summary>
        Public = 0x00000001,
        /// <summary>
        /// Class is nested with public visibility
        /// </summary>
        NestedPublic = 0x00000002,
        /// <summary>
        /// Class is nested with private visibility
        /// </summary>
        NestedPrivate = 0x00000003,
        /// <summary>
        /// Class is nested with family visibility
        /// </summary>
        NestedFamily = 0x00000004,
        /// <summary>
        /// Class is nested with assembly visibility
        /// </summary>
        NestedAssembly = 0x00000005,
        /// <summary>
        /// Class is nested with family and assembly
        /// visibility
        /// </summary>
        NestedFamANDAssem = 0x00000006,
        /// <summary>
        /// Class is nested with family or assembly
        /// visibility
        /// </summary>
        NestedFamORAssem = 0x00000007,

        #endregion

        #region Class Layout
        /// <summary>
        /// Use this mask to retrieve class layout
        /// information. These 2 bits contain one of the
        /// following values
        /// </summary>
        LayoutMask = 0x00000018,
        /// <summary>
        /// Class fields are auto-laid out
        /// </summary>
        AutoLayout = 0x00000000,
        /// <summary>
        /// Class fields are laid out sequentially
        /// </summary>
        SequentialLayout = 0x00000008,
        /// <summary>
        /// Layout is supplied explicitly
        /// </summary>
        ExplicitLayout = 0x00000010,

        #endregion

        #region Class semantics
        /// <summary>
        /// Use this mask to retrive class semantics
        /// information. This bit contains one of the
        /// following values
        /// </summary>
        ClassSemanticsMask = 0x00000020,
        /// <summary>
        /// Type is a class
        /// </summary>
        Class = 0x000000000,
        /// <summary>
        /// Type is an interface
        /// </summary>
        Interface = 0x00000020,

        #endregion

        #region Special Semantics
        /// <summary>
        /// Class is abstract
        /// </summary>
        Abstract = 0x00000080,
        /// <summary>
        /// Class cannot be extended
        /// </summary>
        Sealed = 0x00000100,
        /// <summary>
        /// Class name is special
        /// </summary>
        SpecialName = 0x00000400,

        #endregion

        #region Implementation
        /// <summary>
        /// Class/Interface is imported
        /// </summary>
        Import = 0x00001000,
        /// <summary>
        /// Reserved (Class is serializable)
        /// </summary>
        Serializable = 0x00002000,

        #endregion

        #region String Formatting
        /// <summary>
        /// Use this mask to retrieve string information for
        /// native interop. These 2 bits contain one of the
        /// following values
        /// </summary>
        StringFormatMask = 0x00030000,
        /// <summary>
        /// LPSTR is interpreted as ANSI
        /// </summary>
        AnsiClass = 0x00000000,
        /// <summary>
        /// LPSTR is interpreted as Unicode
        /// </summary>
        UnicodeClass = 0x00010000,
        /// <summary>
        /// LPSTR is interpreted automatically
        /// </summary>
        AutoClass = 0x00020000,
        /// <summary>
        /// A non-standard encoding specified by
        /// CustomStringFormatMask
        /// </summary>
        CustomFormatClass = 0x00030000,
        /// <summary>
        /// Use this mask to retrieve non-standard
        ///encoding information for native interop. The
        ///meaning of the values of these 2 bits is
        ///unspecified
        /// </summary>
        CustomStringFormatMask = 0x00C00000,
        #endregion

        #region Class Initialization
        /// <summary>
        /// Initialize the class before first static field
        ///access
        /// </summary>
        BeforeFieldInit = 0x00100000,

        #endregion

        #region Additional Flags
        /// <summary>
        /// CLI provides 'special' behavior, depending
        ///upon the name of the Type
        /// </summary>
        RTSpecialName = 0x00000800,
        /// <summary>
        /// Type has security associate with it
        /// </summary>
        HasSecurity = 0x00040000,
        /// <summary>
        /// This ExportedType entry is a type forwarder
        /// </summary>
        IsTypeForwarder = 0x00200000

        #endregion
    }
}
