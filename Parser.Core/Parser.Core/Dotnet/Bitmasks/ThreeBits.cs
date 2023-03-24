using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Bitmasks
{
    public abstract class ThreeBits : BitParserBase
    {
        protected string Prefix => "";

        public override short BitNumber => 3;
    }

    public class MemberRefParentParser : ThreeBits
    {
        public override string Name => Prefix + "MemberRefParent";

        public override Enum BitmaskType => BitmaskType3.MemberRefParent;
    }

    public class CustomAttributeTypeParser : ThreeBits
    {
        public override string Name => Prefix + "CustomAttributeType";

        public override Enum BitmaskType => BitmaskType3.CustomAttributeType;
    }
}
