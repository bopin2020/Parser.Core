using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Bitmasks
{
    public abstract class TwoBits : BitParserBase
    {
        protected string Prefix => "";

        public override short BitNumber => 2;
    }

    public class TypeDefOrRefParser : TwoBits
    {
        public override string Name => Prefix + "TypeDefOrRef";

        public override Enum BitmaskType => BitmaskType2.TypeDefOrRef;
    }

    public class HasConstantParser : TwoBits
    {
        public override string Name => Prefix + "HasConstant";

        public override Enum BitmaskType => BitmaskType2.HasConstant;
    }

    public class HasDeclSecurityParser : TwoBits
    {
        public override string Name => Prefix + "HasDeclSecurity";

        public override Enum BitmaskType => BitmaskType2.HasDeclSecurity;
    }

    public class ImplementationParser : TwoBits
    {
        public override string Name => Prefix + "Implementation";

        public override Enum BitmaskType => BitmaskType2.Implementation;
    }

    public class ResolutionScopeParser : TwoBits
    {
        public override string Name => Prefix + "ResolutionScope";

        public override Enum BitmaskType => BitmaskType2.ResolutionScope;
    }
}
