using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    internal static class Tables
    {
        internal static List<Type> Vault = new()
        {
            typeof(AssemblyOS),
            typeof(AssemblyProcessor),
            typeof(AssemblyRef),
            typeof(AssemblyRefOS),
            typeof(AssemblyRefProcessor),
            typeof(AssemblyTable),
            typeof(ClassLayoutTable),
            typeof(ConstantTable),
            typeof(CustomAttributeTable),
            typeof(DeclSecurityTable),

            typeof(EventMapTable),
            typeof(EventTable),
            typeof(ExportedTypeTable),
            typeof(FieldLayoutTable),
            typeof(FieldMarshalTable),
            typeof(FieldRVA),
            typeof(FieldTable),
            typeof(FileTable),
            typeof(GenericParamConstraint),
            typeof(GenericParamTable),

            typeof(ImplMapTable),
            typeof(InterfaceImplTable),
            typeof(ManifestResourceTable),
            typeof(MemberRefTable),
            typeof(MethodDefTable),
            typeof(MethodSpecTable),
            typeof(ModuleRef),
            typeof(ModuleTable),
            typeof(NestedClassTable),
            typeof(ParamTable),

            typeof(PropertyMap),
            typeof(PropertyTable),
            typeof(StandAloneSigTable),
            typeof(TypeDefTable),
            typeof(TypeRefTable),
            typeof(TypeSpec),
        };
    }
}
