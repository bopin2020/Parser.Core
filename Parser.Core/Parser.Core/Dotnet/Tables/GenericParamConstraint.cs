using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// II.22.21 GenericParamConstraint : 0x2C
    /// page 255
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.GenericParamConstraint)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct GenericParamConstraint
    {
        /// <summary>
        /// an index into the GenericParam table, specifying to which generic
        ///parameter this row refers
        /// </summary>
        public int Owner { get; set; }
        /// <summary>
        /// an index into the TypeDef, TypeRef, or TypeSpec tables, specifying from
        /// which class this generic parameter is constrained to derive
        /// 
        /// or which interface this
        /// generic parameter is constrained to implement
        /// </summary>
        public int Constraint { get; set; }
    }

    public class GenericParamConstraintCalc : TableBase<GenericParamConstraint>
    {
        public override MetadataTableType Type => MetadataTableType.GenericParamConstraint;

        public override GenericParamConstraint Create(DotnetParser parser, IntPtr baseAddr)
        {
            throw new Exception();
        }
    }
}
