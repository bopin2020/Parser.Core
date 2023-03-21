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
        public uint RVA { get; set; }
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

        public string StringName { get; set; }

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
            methodDef.RVA = ReadUInt32(baseAddr + offset);
            offset += 4;
            methodDef.ImplFlags = (MethodImplAttributes)ReadUInt16(baseAddr + offset);
            offset += 2;
            methodDef.Flags = (MethodAttributes)ReadUInt16(baseAddr + offset);
            offset += 2;

            methodDef.Name = CheckIndexFromStringStream(parser, baseAddr, ref offset, methodDef.Name);
            methodDef.Signature = CheckIndexFromBlobStream(parser, baseAddr, ref offset, methodDef.Signature);
            methodDef.ParamList = CheckIndexFromWhatever(parser, baseAddr, ref offset, methodDef.ParamList);

            methodDef.StringName = Marshal.PtrToStringAnsi(parser.GetOffset(parser.StringStreamAddr,methodDef.Name));
            Position = offset;
            return methodDef;
        }
    }
}
