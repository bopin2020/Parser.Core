using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Parser.Core.Dotnet.Bitmasks;

namespace Parser.Core.Dotnet.Tables
{
    /// <summary>
    /// .method
    /// II.22.26 MethodDef : 0x06 
    /// page 259
    /// 
    /// This contains informative text only 详细记录了元数据的规定
    /// 
    /// 
    /// 
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.MethodDef)]
    [MetadataTableLevel(MetadataTableLevel.Sophisticated)]
    public struct MethodDefTable
    {
        /// <summary>
        /// 
        /// </summary>
        public int RVA { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MethodImplAttributes ImplFlags { get; set; }
        /// <summary>
        /// </summary>
        public MethodAttributes Flags { get; set; }
        /// <summary>
        /// an index into the String heap
        /// </summary>
        public dynamic Name { get; set; }
        /// <summary>
        /// an index into the Blob heap
        /// </summary>
        public dynamic Signature { get; set; }
        /// <summary>
        /// an index into the Param table
        /// </summary>
        public dynamic ParamList { get; set; }
    }

    public class MethodDefTableCalc : TableBase<MethodDefTable>
    {
        public override MetadataTableType Type => MetadataTableType.MethodDef;

        public override MethodDefTable Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            MethodDefTable methodDef = new MethodDefTable();
            methodDef.RVA = Marshal.ReadInt32(baseAddr + offset);
            offset += 4;

            methodDef.ImplFlags = (MethodImplAttributes)Marshal.ReadInt16(baseAddr + offset);
            offset += 2;


            methodDef.Flags = (MethodAttributes)Marshal.ReadInt16(baseAddr + offset);
            offset += 2;

            if (Marshal.ReadInt16(baseAddr, offset) < parser.GetStringsStream().Length)
            {
                methodDef.Name = Marshal.ReadInt16(baseAddr, offset);
                offset += 2;
            }
            else
            {
                methodDef.Name = Marshal.ReadInt32(baseAddr, offset);
                offset += 4;
            }



            if (Marshal.ReadInt16(baseAddr, offset) < parser.GetBlobStream().Length)
            {
                methodDef.Signature = Marshal.ReadInt16(baseAddr, offset);
                offset += 2;
            }
            else
            {
                methodDef.Signature = Marshal.ReadInt32(baseAddr, offset);
                offset += 4;
            }

            return methodDef;
        }
    }
}
