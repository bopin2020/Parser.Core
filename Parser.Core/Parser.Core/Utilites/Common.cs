using Parser.Core.Dotnet.Tables;
using Parser.Core.Dotnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Utilites
{
    internal static class Common
    {
        public static bool CheckBits(byte num,int offset)
        {
            if(offset > 8) { throw new ArgumentOutOfRangeException(nameof(offset)); }
            char[] tables = Convert.ToString(num, 2).ToCharArray();
            return CheckCore(tables,offset);
        }

        public static bool CheckBits(short num, int offset)
        {
            if (offset > 16) { throw new ArgumentOutOfRangeException(nameof(offset)); }
            char[] tables = Convert.ToString(num, 2).ToCharArray();
            return CheckCore(tables, offset);
        }

        public static bool CheckBits(ushort num, int offset)
        {
            if (offset > 16) { throw new ArgumentOutOfRangeException(nameof(offset)); }
            char[] tables = Convert.ToString(num, 2).ToCharArray();
            return CheckCore(tables, offset);
        }

        public static bool CheckBits(int num, int offset)
        {
            if (offset > 16) { throw new ArgumentOutOfRangeException(nameof(offset)); }
            char[] tables = Convert.ToString(num, 2).ToCharArray();
            return CheckCore(tables, offset);
        }

        public static bool CheckBits(uint num, int offset)
        {
            if (offset > 16) { throw new ArgumentOutOfRangeException(nameof(offset)); }
            char[] tables = Convert.ToString(num, 2).ToCharArray();
            return CheckCore(tables, offset);
        }

        public static bool CheckBits(long num, int offset)
        {
            if (offset > 16) { throw new ArgumentOutOfRangeException(nameof(offset)); }
            char[] tables = Convert.ToString(num, 2).ToCharArray();
            return CheckCore(tables, offset);
        }

        public static bool CheckBits(ulong num, int offset)
        {
            if (offset > 16) { throw new ArgumentOutOfRangeException(nameof(offset)); }
            char[] tables = Convert.ToString((long)num,2).ToCharArray();
            return CheckCore(tables, offset);
        }

        private static bool CheckCore(char[] tables,int offset)
        {
            Array.Reverse(tables);
            for (int i = 0; i < tables.Length; i++)
            {
                if (tables[offset] == '1')
                {
                    return true;
                }
            }
            return false;
        }
    }
}
