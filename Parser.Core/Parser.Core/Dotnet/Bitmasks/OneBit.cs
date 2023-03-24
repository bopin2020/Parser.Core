using Parser.Core.Dotnet.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Bitmasks
{
    public abstract class OneBit : BitParserBase
    {
        protected string Prefix => "";

        public override short BitNumber => 1;
    }

    public class HasFieldMarshallParser : OneBit
    {
        public override string Name => Prefix + "HasFieldMarshall";

        public override Enum BitmaskType => BitmaskType1.HasFieldMarshall;
    }

    public class MethodDefOrRefParser : OneBit
    {
        public override string Name => Prefix + "MethodDefOrRef";

        public override Enum BitmaskType => BitmaskType1.MethodDefOrRef;
    }

    public class MemberForwardedParser : OneBit
    {
        public override string Name => Prefix + "MemberForwarded";

        public override Enum BitmaskType => BitmaskType1.MemberForwarded;
    }

    public class HasSemanticsParser : OneBit
    {
        public override string Name => Prefix + "HasSemantics";

        public override Enum BitmaskType => BitmaskType1.HasSemantics;
    }

    public class TypeOrMethodDefParser : OneBit
    {
        public override string Name => Prefix + "TypeOrMethodDef";

        public override Enum BitmaskType => BitmaskType1.TypeOrMethodDef;
    }
}
