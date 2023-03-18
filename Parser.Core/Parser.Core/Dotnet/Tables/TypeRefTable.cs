using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// 
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.TypeRef)]
    [MetadataTableLevel(MetadataTableLevel.Sophisticated)]
    public struct TypeRefTable
    {
        /// <summary>
        /// an index into a Module, ModuleRef, AssemblyRef or TypeRef table,
        /// or null; more precisely, a ResolutionScope
        /// </summary>
        public dynamic ResolutionScope { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public dynamic TypeName { get; set; }

        public string StringTypeName { get; set; }


        /// <summary>
        /// an index into the String heap
        /// 
        /// TypeNamespace can be null, or non-null
        /// </summary>
        public dynamic TypeNamespace { get; set; }

        public string StringTypeNamespace { get; set; }

    }

    public class TypeRefTableCalc : TableBase<TypeRefTable>
    {
        public override MetadataTableType Type => MetadataTableType.TypeRef;

        public override TypeRefTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            TypeRefTable typeref = new TypeRefTable();
            typeref.ResolutionScope = CheckIndexFromStringStream(parser,baseAddr,ref offset, typeref.ResolutionScope);
            typeref.TypeName = CheckIndexFromStringStream(parser,baseAddr,ref offset, typeref.TypeName);
            typeref.TypeNamespace = CheckIndexFromStringStream(parser,baseAddr,ref offset, typeref.TypeNamespace);
            typeref.StringTypeName = Marshal.PtrToStringAnsi(parser.StringStreamAddr + typeref.TypeName);
            typeref.StringTypeNamespace = Marshal.PtrToStringAnsi(parser.StringStreamAddr + typeref.TypeNamespace);
            Position = offset;
            return typeref;
        }
    }
}
