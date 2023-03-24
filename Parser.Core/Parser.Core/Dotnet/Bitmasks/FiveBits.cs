using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Bitmasks
{
    public abstract class FiveBits : BitParserBase
    {
        protected string Prefix => "";

        public override short BitNumber => 5;
    }
    public class HasCustomAttributeParser : FiveBits
    {
        public override string Name => Prefix + "HasCustomAttribute";

        public override Enum BitmaskType => BitmaskType5.HasCustomAttribute;
    }
}
