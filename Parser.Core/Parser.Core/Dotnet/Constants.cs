using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet
{
    internal static class Constants
    {
        /// <summary>
        /// https://www.ntcore.com/files/dotnetformat.htm
        /// if the rows are > 0xFFFF, a dword is necessary to store the number, otherwise a word will do the job
        /// </summary>
        public const ushort IndexLimited = 0xffff;
    }
}
