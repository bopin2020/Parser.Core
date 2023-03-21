using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    internal static class Tables
    {
        internal static Dictionary<MetadataTableType,Type> Vault = new()
        {
            {MetadataTableType.Assembly,typeof(AssemblyTableCalc) },
            {MetadataTableType.AssemblyOS,typeof(AssemblyOSCalc) },
            {MetadataTableType.AssemblyProcessor,typeof(AssemblyProcessorCalc) },
            {MetadataTableType.AssemblyRef,typeof(AssemblyRefCalc) },
            {MetadataTableType.AssemblyRefOS,typeof(AssemblyRefOSCalc) },
            {MetadataTableType.AssemblyRefProcessor,typeof(AssemblyRefProcessorCalc) },
            {MetadataTableType.ClassLayout,typeof(ClassLayoutTableCalc) },
            {MetadataTableType.Constant,typeof(ConstantTableCalc) },
            {MetadataTableType.CustomAttribute,typeof(CustomAttributeTableCalc) },
            {MetadataTableType.DeclSecurity,typeof(DeclSecurityTableCalc) },
            {MetadataTableType.Event,typeof(EventTableCalc) },
            {MetadataTableType.EventMap,typeof(EventMapTableCalc) },
            {MetadataTableType.ExportedType,typeof(ExportedTypeTableCalc) },
            {MetadataTableType.Field,typeof(FieldTableCalc) },
            {MetadataTableType.FieldLayout,typeof(FieldLayoutTableCalc) },
            {MetadataTableType.FieldMarshal,typeof(FieldMarshalTableCalc) },
            {MetadataTableType.FieldRVA,typeof(FieldRVACalc) },
            {MetadataTableType.File,typeof(FileTableCalc) },
            {MetadataTableType.GenericParam,typeof(GenericParamTableCalc) },
            {MetadataTableType.GenericParamConstraint,typeof(GenericParamConstraintCalc) },
            {MetadataTableType.ImplMap,typeof(ImplMapTableCalc) },
            {MetadataTableType.InterfaceImpl,typeof(InterfaceImplTableCalc) },
            {MetadataTableType.ManifestResource,typeof(ManifestResourceTableCalc) },
            {MetadataTableType.MethodDef,typeof(MethodDefTableCalc) },
            {MetadataTableType.MethodSemantics,typeof(MethodSemanticsTableCalc) },
            {MetadataTableType.MethodImpl,typeof(MethodImplTableCalc) },
            {MetadataTableType.MemberRef,typeof(MemberRefTableCalc) },
            {MetadataTableType.Module,typeof(ModuleCalc) },
            {MetadataTableType.ModuleRef,typeof(ModuleRefCalc) },
            {MetadataTableType.MethodSpec,typeof(MethodSpecTableCalc) },
            {MetadataTableType.NestedClass,typeof(NestedClassTableCalc) },
            {MetadataTableType.Param,typeof(ParamTableCalc) },
            {MetadataTableType.Property,typeof(PropertyTableCalc) },
            {MetadataTableType.PropertyMap,typeof(PropertyMapCalc) },
            {MetadataTableType.StandAloneSig,typeof(StandAloneSigTableCalc) },
            {MetadataTableType.TypeDef,typeof(TypeDefTableCalc) },
            {MetadataTableType.TypeRef,typeof(TypeRefTableCalc) },
            {MetadataTableType.TypeSpec,typeof(TypeSpecCalc) },
        };
    }
}
