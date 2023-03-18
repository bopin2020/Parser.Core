using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    internal static class Tables
    {
        internal static Dictionary<MetadataTableType,Type> Vault = new()
        {
            {MetadataTableType.AssemblyOS,typeof(AssemblyOSCalc) },
            {MetadataTableType.AssemblyProcessor,typeof(AssemblyProcessorCalc) },
            {MetadataTableType.AssemblyRef,typeof(AssemblyRefCalc) },
            {MetadataTableType.AssemblyRefProcessor,typeof(AssemblyRefProcessorCalc) },
            {MetadataTableType.Assembly,typeof(AssemblyTableCalc) },

            {MetadataTableType.ClassLayout,typeof(ClassLayoutTableCalc) },
            {MetadataTableType.Constant,typeof(ConstantTableCalc) },

            {MetadataTableType.MethodDef,typeof(MethodDefTableCalc) },
            {MetadataTableType.Module,typeof(ModuleCalc) },

            {MetadataTableType.TypeRef,typeof(TypeRefTableCalc) },
        };
    }
}
