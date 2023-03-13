using System;
using System.Collections.Generic;
using System.Linq;
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
        public short Action { get; set; }
        /// <summary>
        /// an index into the TypeDef, MethodDef, or Assembly table; more precisely, a
        /// HasDeclSecurity
        /// </summary>
        public int Parent { get; set; }
        /// <summary>
        /// an index into the Blob heap
        /// </summary>
        public int PermissionSet { get; set; }

    }
}
