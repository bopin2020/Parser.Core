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
        public dynamic Owner { get; set; }
        /// <summary>
        /// an index into the TypeDef, TypeRef, or TypeSpec tables, specifying from
        /// which class this generic parameter is constrained to derive
        /// 
        /// or which interface this
        /// generic parameter is constrained to implement
        /// </summary>
        public dynamic Constraint { get; set; }
        public MetadataTableType ConstraintType { get; set; }
        public uint ConstraintIndex { get; set; }
    }

    public class GenericParamConstraintCalc : TableBase<GenericParamConstraint>
    {
        public override MetadataTableType Type => MetadataTableType.GenericParamConstraint;

        public override GenericParamConstraint Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            GenericParamConstraint genericParamCons = new GenericParamConstraint();
            genericParamCons.Owner = CheckIndexFromWhatever(parser, baseAddr, ref offset, genericParamCons.Owner);
            genericParamCons.Constraint = CheckIndexFromWhatever(parser, baseAddr, ref offset, genericParamCons.Constraint);

            genericParamCons.ConstraintType = parser.Bitparser["TypeDefOrRef"].SpecifiedTable(genericParamCons.Constraint, out int index);
            genericParamCons.ConstraintIndex = (uint)index;

            Position = offset;
            return genericParamCons;
        }
    }
}
