using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// II.22.11 DeclSecurity : 0x0E 
    /// page 244
    /// 
    /// Security  derived from
    /// System.Security.Permissions.SecurityAttribute
    /// 
    /// can be attached to a TypeDef, a Method, or an Assembly
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.DeclSecurity)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct DeclSecurityTable
    {
        public ushort Action { get; set; }
        /// <summary>
        /// an index into the TypeDef, MethodDef, or Assembly table; more precisely, a
        /// HasDeclSecurity
        /// HasDeclSecurity: 2 bits to encode tag
        /// 00 TypeDef
        /// 01 MethodDef
        /// 10 Assembly
        /// </summary>
        public dynamic Parent { get; set; }
        public MetadataTableType ParentType { get; set; }
        public uint ParentIndex { get; set; }
        /// <summary>
        /// an index into the Blob heap
        /// </summary>
        public dynamic PermissionSet { get; set; }

    }

    public class DeclSecurityTableCalc : TableBase<DeclSecurityTable>
    {
        public override MetadataTableType Type => MetadataTableType.DeclSecurity;

        public override DeclSecurityTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            DeclSecurityTable declSecurity = new DeclSecurityTable();
            declSecurity.Action = ReadUInt16(baseAddr + offset);
            offset += 2;
            declSecurity.Parent = CheckIndexFromWhatever(parser, baseAddr, ref offset, declSecurity.Parent);

            declSecurity.ParentType = parser.Bitparser["HasDeclSecurity"].SpecifiedTable(declSecurity.Parent, out int index);
            declSecurity.ParentIndex = (uint)index;

            declSecurity.PermissionSet = CheckIndexFromBlobStream(parser, baseAddr, ref offset, declSecurity.PermissionSet);
            Position = offset;
            return declSecurity;
        }
    }
}
