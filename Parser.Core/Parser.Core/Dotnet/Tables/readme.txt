        /// For example, the first element of a NestedClass row is an index into the TypeDef table,
        /// the size of this index depends on how much rows the TypeDef table counts: if the rows are > 0xFFFF,
        /// a dword is necessary to store the number, otherwise a word will do the job
        /// 
        /// https://www.ntcore.com/files/dotnetformat.htm
        /// ecma ��׼©��  ����ntcore Ҳ����CFF�����Ʒ������Ϊ׼
        /// The Microsoft documentation is not so clear about this (at all)

        /// �ṹ���С�Ƕ�̬�����  ���� index   ����ǰ��ֵ���� 0xffff 2�ֽڷ��������ֵʹ��4�ֽ�