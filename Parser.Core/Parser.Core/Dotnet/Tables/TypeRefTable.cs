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
            if (Marshal.ReadInt16(baseAddr, offset) < parser.GetStringsStream().Length)
            {
                typeref.ResolutionScope = Marshal.ReadInt16(baseAddr, offset);
                offset += 2;
            }
            else
            {
                typeref.ResolutionScope = Marshal.ReadInt32(baseAddr, offset);
                offset += 4;
            }

            if (Marshal.ReadInt16(baseAddr, offset) < parser.GetStringsStream().Length)
            {
                typeref.TypeName = Marshal.ReadInt16(baseAddr, offset);
                offset += 2;
            }
            else
            {
                typeref.TypeName = Marshal.ReadInt32(baseAddr, offset);
                offset += 4;
            }

            typeref.StringTypeName = Marshal.PtrToStringAnsi(parser.StringStreamAddr + typeref.TypeName);

            if (Marshal.ReadInt16(baseAddr, offset) < parser.GetStringsStream().Length)
            {
                typeref.TypeNamespace = Marshal.ReadInt16(baseAddr, offset);
                offset += 2;
            }
            else
            {
                typeref.TypeNamespace = Marshal.ReadInt32(baseAddr, offset);
                offset += 4;
            }
            typeref.StringTypeNamespace = Marshal.PtrToStringAnsi(parser.StringStreamAddr + typeref.TypeNamespace);

            Position = offset;
            return typeref;
        }
    }
}
