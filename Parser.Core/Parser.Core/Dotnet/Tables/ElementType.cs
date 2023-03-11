using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// II.23.1.16 Element types used in signatures 
    /// ecma335 page281
    /// 
    /// The following table lists the values for ELEMENT_TYPE constants
    /// </summary>
    public enum ElementType
    {
        /// <summary>
        /// Marks end of a list
        /// </summary>
        ELEMENT_TYPE_END = 0x00,
        ELEMENT_TYPE_VOID = 0x01,
        ELEMENT_TYPE_BOOLEAN = 0x02,
        ELEMENT_TYPE_CHAR = 0x03,
        ELEMENT_TYPE_I1 = 0x04,
        ELEMENT_TYPE_U1 = 0x05,
        ELEMENT_TYPE_I2 = 0x06,
        ELEMENT_TYPE_U2 = 0x07,
        ELEMENT_TYPE_I4 = 0x08,
        ELEMENT_TYPE_U4 = 0x09,
        ELEMENT_TYPE_I8 = 0x0a,
        ELEMENT_TYPE_U8 = 0x0b,
        ELEMENT_TYPE_R4 = 0x0c,
        ELEMENT_TYPE_R8 = 0x0d,
        ELEMENT_TYPE_STRING = 0x0e,
        /// <summary>
        /// Followed by type
        /// </summary>
        ELEMENT_TYPE_PTR = 0x0f,
        /// <summary>
        /// Followed by type
        /// </summary>
        ELEMENT_TYPE_BYREF = 0x10,
        /// <summary>
        /// Followed by TypeDef or TypeRef token
        /// </summary>
        ELEMENT_TYPE_VALUETYPE = 0x11,
        /// <summary>
        /// Followed by TypeDef or TypeRef token
        /// </summary>
        ELEMENT_TYPE_CLASS = 0x12,
        /// <summary>
        /// Generic parameter in a generic type
        /// definition, represented as number
        /// (compressed unsigned integer)
        /// </summary>
        ELEMENT_TYPE_VAR = 0x13,
        /// <summary>
        /// type rank boundsCount bound1 
        /// loCount lo1
        /// </summary>
        ELEMENT_TYPE_ARRAY = 0x14,
        /// <summary>
        /// Generic type instantiation. Followed by
        /// type type-arg-count type-1 ... type-n
        /// </summary>
        ELEMENT_TYPE_GENERICINST = 0x15,
        ELEMENT_TYPE_TYPEDBYREF = 0x16,
        /// <summary>
        /// System.IntPtr
        /// </summary>
        ELEMENT_TYPE_I = 0x18,
        /// <summary>
        /// System.UIntPtr
        /// </summary>
        ELEMENT_TYPE_U = 0x19,
        /// <summary>
        /// Followed by full method signature
        /// </summary>
        ELEMENT_TYPE_FNPTR = 0x1b,
        /// <summary>
        /// System.Object
        /// </summary>
        ELEMENT_TYPE_OBJECT = 0x1c,
        /// <summary>
        /// Single-dim array with 0 lower bound
        /// </summary>
        ELEMENT_TYPE_SZARRAY = 0x1d,
        /// <summary>
        /// Generic parameter in a generic method
        /// definition, represented as number
        /// (compressed unsigned integer)
        /// </summary>
        ELEMENT_TYPE_MVAR = 0x1e,
        /// <summary>
        /// Required modifier
        /// followed by a
        /// TypeDef or TypeRef token
        /// </summary>
        ELEMENT_TYPE_CMOD_REQD = 0x1f,
        /// <summary>
        /// Optional modifier :
        /// followed by a
        /// TypeDef or TypeRef token
        /// </summary>
        ELEMENT_TYPE_CMOD_OPT = 0x20,
        /// <summary>
        /// Implemented within the CLI
        /// </summary>
        ELEMENT_TYPE_INTERNAL = 0x21,
        /// <summary>
        /// Or’d with following element types
        /// </summary>
        ELEMENT_TYPE_MODIFIER = 0x40,
        /// <summary>
        /// Sentinel for vararg method signature
        /// </summary>
        ELEMENT_TYPE_SENTINEL = 0x41,
        /// <summary>
        /// Denotes a local variable that points at a
        /// pinned object
        /// </summary>
        ELEMENT_TYPE_PINNED = 0x45,

        /// <summary>
        /// ecma335 have no the specified Element Name just given a number and meaning
        /// </summary>
        #region SpecialElement

        /// <summary>
        /// Indicates an argument of type
        /// System.Type.
        /// </summary>
        ELEMENT_TYPE_ARGUMENT = 0x50,
        /// <summary>
        /// Used in custom attributes to specify a
        /// boxed object
        /// </summary>
        ELEMENT_TYPE_ATTRIBUTEBOXEDOBJECT = 0x51,
        /// <summary>
        /// Reserved
        /// </summary>
        Reserved = 0x52,
        /// <summary>
        /// Used in custom attributes to indicate a
        /// FIELD
        /// </summary>
        ELEMENT_TYPE_ATTRIBUTEFIELD = 0x53,
        /// <summary>
        /// Used in custom attributes to indicate a property
        /// </summary>
        ELEMENT_TYPE_ATTRIBUTEPROPERTY = 0x54,
        /// <summary>
        /// Used in custom attributes to specify an
        /// enum
        /// </summary>
        ELEMENT_TYPE_ATTRIBUTEENUM = 0x55,

        #endregion


    }
}
