using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.Dotnet.BlobSignature
{
    /// <summary>
    /// II.23.2 Blobs and signatures 
    /// page 283
    /// 
    /// 2字节 signature is conventionally 通常被用来描述一个函数或方法的类型信息
    /// 它的每个参数，返回值信息等  这些元素成为之签名 - 即表示某个实体的唯一值
    /// 
    /// 在元数据中 签名也被用来描述字段，属性，本地变量信息
    /// 每个签名被存储作为可计算的byte array 存储在 Blob heap
    /// </summary>
    public abstract class BlobSignatures
    {

    }
}
