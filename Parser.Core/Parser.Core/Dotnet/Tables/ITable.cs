using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public virtual dynamic CheckIndexFromWhatever(DotnetParser parser, IntPtr baseAddr, ref int offset, dynamic item,int index = Constants.IndexLimited)
        {
            ushort flag = PeekData(baseAddr, offset);
            // 读取2字节
            // 可能的情况
            // 1. 本来数据就是0000
            // 2. 数据应该是4个字节  但是0x00000001   前两个就是00
            if (flag <= index && flag >= 0 && index <= Constants.IndexLimited)
            {
                //item = Convert.ToUInt16(Marshal.ReadInt16(baseAddr, offset));
                item = ReadUInt16(baseAddr, offset);
                offset += 2;
            }
            else
            {
                // 
                if(flag == 0)
                {
                    Console.WriteLine("0");
                }

                item = ReadUInt32(baseAddr, offset);
                offset += 4;
            }

            return item;
        }


        public ushort PeekData(IntPtr baseAddr, int offset)
        {
            //short result = Marshal.ReadInt16(baseAddr, offset);
            ushort result = ReadUInt16(baseAddr, offset);
            // signed convert to unsigned number
            if(result < 0)
            {
                result = (ushort)(result + 0xffff0000);
            }
            return result;
        }

        #region SupportFunctions

        /// <summary>
        /// 获取索引 应该使用无符号数
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="ofs"></param>
        /// <returns></returns>
        public unsafe ushort ReadUInt16(IntPtr ptr, int ofs)
        {
            return ReadUInt16(ptr + ofs);
        }

        public unsafe ushort ReadUInt16(IntPtr ptr)
        {
            byte* addr = (byte*)ptr;
            return Unsafe.ReadUnaligned<ushort>(addr);
        }

        public unsafe uint ReadUInt32(IntPtr ptr, int ofs)
        {
            return ReadUInt32(ptr + ofs);
        }

        public unsafe uint ReadUInt32(IntPtr ptr)
        {
            byte* addr = (byte*)ptr;
            return Unsafe.ReadUnaligned<uint>(addr);
        }

        #endregion
    }
}
