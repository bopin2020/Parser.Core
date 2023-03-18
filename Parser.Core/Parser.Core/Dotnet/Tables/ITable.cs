using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
