using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// II.22.25 MemberRef : 0x0A 
    /// page 258
    /// 
    /// The MemberRef table combines two sorts of references, to Methods and to Fields of a class, known as
    ///‘MethodRef’ and ‘FieldRef’, respectively
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.MemberRef)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct MemberRefTable
    {
        /// <summary>
        /// an index into the MethodDef, ModuleRef,TypeDef, TypeRef, or TypeSpec tables
        /// </summary>
        public dynamic Class { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public dynamic Name { get; set; }

        public string StringName { get; set; }

        /// <summary>
        /// an index into the Blob heap
        /// </summary>
        public dynamic Signature { get; set; }
    }

    public class MemberRefTableCalc : TableBase<MemberRefTable>
    {
        public override MetadataTableType Type => MetadataTableType.MemberRef;

        public string Marshall { get; private set; }

        public override MemberRefTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            MemberRefTable memberRef = new MemberRefTable();

            memberRef.Class = CheckIndexFromWhatever(parser, baseAddr, ref offset, memberRef.Class);
            memberRef.Name = CheckIndexFromStringStream(parser, baseAddr, ref offset, memberRef.Class);
            memberRef.Signature = CheckIndexFromBlobStream(parser, baseAddr, ref offset, memberRef.Class);
            memberRef.StringName = Marshal.PtrToStringAnsi(parser.GetOffset(parser.StringStreamAddr,memberRef.Name));
            Position = offset;
            return memberRef;
        }
    }
}
