using System;
using System.Collections.Generic;
using System.Linq;
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
        public int Class { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public int Name { get; set; }
        /// <summary>
        /// an index into the Blob heap
        /// </summary>
        public int Signature { get; set; }
    }

    public class MemberRefTableCalc : TableBase<MemberRefTable>
    {
        public override MetadataTableType Type => MetadataTableType.MemberRef;

        public override MemberRefTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            throw new Exception();
        }
    }
}
