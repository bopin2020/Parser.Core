using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Core.Dotnet.Bitmasks;

namespace Parser.Core.Dotnet.Tables
{
    [MetadataTableTypeDef(MetadataTableType.GenericParam)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct GenericParamTable
    {
        /// <summary>
        /// the 2-byte index of the generic parameter, numbered left-to-right, from
        /// zero
        /// </summary>
        public short Number { get; set; }
        /// <summary>
        /// a 2-byte bitmask of type GenericParamAttributes
        /// </summary>
        public GenericParamAttributes Flags { get; set; }
        /// <summary>
        /// an index into the TypeDef or MethodDef table, specifying the Type or
        /// Method to which this generic parameter applies
        /// </summary>
        public int Owner { get; set; }

        /// <summary>
        /// a non-null index into the String heap, giving the name for the generic
        /// parameter
        /// </summary>
        public int Name { get; set; }
    }
}
