        /// For example, the first element of a NestedClass row is an index into the TypeDef table,
        /// the size of this index depends on how much rows the TypeDef table counts: if the rows are > 0xFFFF,
        /// a dword is necessary to store the number, otherwise a word will do the job
        /// 
        /// https://www.ntcore.com/files/dotnetformat.htm
        /// ecma 标准漏洞  根据ntcore 也就是CFF这件产品的资料为准
        /// The Microsoft documentation is not so clear about this (at all)

        /// 结构体大小是动态计算的  凡是 index   即当前数值大于 0xffff 2字节返回则该域值使用4字节