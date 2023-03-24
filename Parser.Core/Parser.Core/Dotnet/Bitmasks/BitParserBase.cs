using Parser.Core.Dotnet.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Bitmasks
{

    public interface IBitParser
    {
        MetadataTableType SpecifiedTable(dynamic item, out int index);
    }
    public abstract class BitParserBase : IBitParser
    {
        public abstract string Name { get; }

        public abstract short BitNumber { get; }

        public abstract Enum BitmaskType { get; }

        public Dictionary<Enum, List<MetadataTableType>> Bits = new Dictionary<Enum, List<MetadataTableType>>
        {
            {
                BitmaskType1.HasSemantics,new List<MetadataTableType>()
                {
                    MetadataTableType.Event,
                    MetadataTableType.Property,
                } 
            },
            {
                BitmaskType1.MethodDefOrRef,new List<MetadataTableType>()
                {
                    MetadataTableType.MethodDef,
                    MetadataTableType.MemberRef,
                } 
            },
            {
                BitmaskType1.MemberForwarded,new List<MetadataTableType>()
                {
                    MetadataTableType.Field,
                    MetadataTableType.MethodDef,
                }
            },
            {
                BitmaskType1.HasFieldMarshall,new List<MetadataTableType>()
                {
                    MetadataTableType.Field,
                    MetadataTableType.Param,
                }
            },
            {
                BitmaskType1.TypeOrMethodDef,new List<MetadataTableType>()
                {
                    MetadataTableType.TypeDef,
                    MetadataTableType.MethodDef,
                }
            },

            {
                BitmaskType2.TypeDefOrRef,new List<MetadataTableType>()
                {
                    MetadataTableType.TypeDef,
                    MetadataTableType.TypeRef,
                    MetadataTableType.TypeSpec,
                }
            },
            {
                BitmaskType2.HasConstant,new List<MetadataTableType>()
                {
                    MetadataTableType.Field,
                    MetadataTableType.Param,
                    MetadataTableType.Property,
                }
            },
            {
                BitmaskType2.HasDeclSecurity,new List<MetadataTableType>()
                {
                    MetadataTableType.TypeDef,
                    MetadataTableType.MethodDef,
                    MetadataTableType.Assembly,
                }
            },
            {
                BitmaskType2.Implementation,new List<MetadataTableType>()
                {
                    MetadataTableType.File,
                    MetadataTableType.AssemblyRef,
                    MetadataTableType.ExportedType,
                }
            },
            {
                BitmaskType2.ResolutionScope,new List<MetadataTableType>()
                {
                    MetadataTableType.Module,
                    MetadataTableType.ModuleRef,
                    MetadataTableType.AssemblyRef,
                    MetadataTableType.TypeRef,
                }
            },

            {
                BitmaskType3.MemberRefParent,new List<MetadataTableType>()
                {
                    MetadataTableType.TypeDef,
                    MetadataTableType.TypeRef,
                    MetadataTableType.ModuleRef,
                    MetadataTableType.MethodDef,
                    MetadataTableType.TypeSpec,
                }
            },
            {
                BitmaskType3.CustomAttributeType,new List<MetadataTableType>()
                {
                    MetadataTableType.None,
                    MetadataTableType.None,
                    MetadataTableType.MethodDef,
                    MetadataTableType.MemberRef,
                    MetadataTableType.None,
                }
            },

            {
                BitmaskType5.HasCustomAttribute,new List<MetadataTableType>()
                {
                    MetadataTableType.MethodDef,
                    MetadataTableType.Field,
                    MetadataTableType.TypeRef,
                    MetadataTableType.TypeDef,
                    MetadataTableType.Param,
                    MetadataTableType.InterfaceImpl,
                    MetadataTableType.MemberRef,
                    MetadataTableType.Module,
                    MetadataTableType.Permission,
                    MetadataTableType.Property,
                    MetadataTableType.Event,
                    MetadataTableType.StandAloneSig,
                    MetadataTableType.ModuleRef,
                    MetadataTableType.TypeSpec,
                    MetadataTableType.Assembly,
                    MetadataTableType.AssemblyRef,
                    MetadataTableType.File,
                    MetadataTableType.ExportedType,
                    MetadataTableType.ManifestResource,
                }
            },
        };

        public MetadataTableType SpecifiedTable(dynamic item, out int index)
        {
            index = item >> BitNumber;
            if (item == 0) { return Bits[BitmaskType][0]; }
            char[] tables = Convert.ToString(item, 2).ToCharArray();
            var result = tables[^BitNumber..];
            int res = Convert.ToInt32(new string(result), 2);
            return Bits[BitmaskType][res];
        }
    }

    public enum BitmaskType1
    {
        HasSemantics,
        MethodDefOrRef,
        MemberForwarded,
        HasFieldMarshall,
        // 泛型参数 Owner 是type还是method
        TypeOrMethodDef,
    }

    public enum BitmaskType2
    {
        TypeDefOrRef,
        HasConstant,
        HasDeclSecurity,
        Implementation,
        ResolutionScope,
    }

    public enum BitmaskType3
    {
        MemberRefParent,
        CustomAttributeType,
    }

    public enum BitmaskType5
    {
        HasCustomAttribute,
    }
}
