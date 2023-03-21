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
    /// AssemblyRef 相当于 native PE中的导入表
    /// 基本上分析AssemblyRef 就知道Assembly调用了哪些功能
    /// 
    /// TODO: 是否可能 AssemblyRef 动态加载  延迟初始化
    /// 
    /// .assembly extern directive
    ///     .publickeytoken
    /// </summary>
    [MetadataTableTypeDef(MetadataTableType.AssemblyRef)]
    [MetadataTableLevel(MetadataTableLevel.Important)]
    public struct AssemblyRef
    {
        public ushort MajorVersion { get; set; }
        public ushort MinorVersion { get; set; }
        public ushort BuildNumber { get; set; }
        public ushort RevisionNumber { get; set; }

        public AssemblyFlags Flags { get; set; }
        /// <summary>
        /// (an index into the Blob heap, indicating the public key or token
        /// that identifies the author of this Assembly
        /// </summary>
        public dynamic PublicKeyOrToken { get; set; }
        /// <summary>
        /// an index into the String heap
        /// 
        /// 最大为int  也有可能为short 
        /// 需要根据实际情况而定
        /// </summary>
        public dynamic Name { get; set; }

        public string StringName { get; set; }

        /// <summary>
        /// an index into the String heap
        /// </summary>
        public dynamic Culture { get; set; }

        public string StringCulture { get; set; }


        /// <summary>
        /// an index into the Blob heap
        /// </summary>
        public dynamic HashValue { get; set; }

    }

    public class AssemblyRefCalc : TableBase<AssemblyRef>
    {
        public override MetadataTableType Type => MetadataTableType.AssemblyRef;

        public override AssemblyRef Create(DotnetParser parser, IntPtr baseAddr)
        {
            int offset = 0;
            AssemblyRef assemblyRef = new AssemblyRef();
            assemblyRef.MajorVersion = ReadUInt16(baseAddr + offset); offset += 2;
            assemblyRef.MinorVersion = ReadUInt16(baseAddr + offset); offset += 2;
            assemblyRef.BuildNumber = ReadUInt16(baseAddr + offset); offset += 2;
            assemblyRef.RevisionNumber = ReadUInt16(baseAddr + offset); offset += 2;

            assemblyRef.Flags = (AssemblyFlags)ReadUInt32(baseAddr + offset); offset += 4;

            assemblyRef.PublicKeyOrToken = CheckIndexFromBlobStream(parser, baseAddr, ref offset, assemblyRef.PublicKeyOrToken);
            assemblyRef.Name = CheckIndexFromStringStream(parser, baseAddr, ref offset, assemblyRef.Name);
            assemblyRef.Culture = CheckIndexFromStringStream(parser, baseAddr, ref offset, assemblyRef.Culture);
            assemblyRef.HashValue = CheckIndexFromBlobStream(parser, baseAddr, ref offset, assemblyRef.HashValue);

            assemblyRef.StringName = Marshal.PtrToStringAnsi(parser.GetOffset(parser.StringStreamAddr,assemblyRef.Name));
            assemblyRef.StringCulture = Marshal.PtrToStringAnsi(parser.GetOffset(parser.StringStreamAddr,assemblyRef.Culture));

            Position = offset;
            return assemblyRef;
        }
    }
}
