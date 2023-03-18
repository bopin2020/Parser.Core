using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.Tables
{
    internal interface ITable<T>
    {
        int Position { get; set; }
        MetadataTableType Type { get; }
        T Create(DotnetParser parser,IntPtr baseAddr);
    }

    public abstract class TableBase<T> : ITable<T>
    {
        public virtual int Position { get; set; } = 0;
        public abstract MetadataTableType Type { get; }

        public abstract T Create(DotnetParser parser, IntPtr baseAddr);

        public virtual dynamic CheckIndexFromStringStream(DotnetParser parser, IntPtr baseAddr,ref int offset,dynamic item)
        {
            return CheckIndexFromWhatever(parser, baseAddr, ref offset, item, parser.GetStringsStream().Length);
        }

        public virtual dynamic CheckIndexFromGUIDStream(DotnetParser parser, IntPtr baseAddr, ref int offset, dynamic item)
        {
            return CheckIndexFromWhatever(parser, baseAddr, ref offset, item, parser.GetGUIDStream().Length);
        }

        public virtual dynamic CheckIndexFromUSStream(DotnetParser parser, IntPtr baseAddr, ref int offset, dynamic item)
        {
            return CheckIndexFromWhatever(parser, baseAddr, ref offset, item, parser.GetUSStream().Length);
        }

        public virtual dynamic CheckIndexFromBlobStream(DotnetParser parser, IntPtr baseAddr, ref int offset, dynamic item)
        {
            return CheckIndexFromWhatever(parser, baseAddr, ref offset, item, parser.GetBlobStream().Length);
        }

        public virtual dynamic CheckIndexFromWhatever(DotnetParser parser, IntPtr baseAddr, ref int offset, dynamic item,int index)
        {
            if (Marshal.ReadInt16(baseAddr, offset) < index)
            {
                item = Marshal.ReadInt16(baseAddr, offset);
                offset += 2;
            }
            else
            {
                item = Marshal.ReadInt32(baseAddr, offset);
                offset += 4;
            }

            return item;
        }
    }
}
