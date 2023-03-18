# Parser.Core

> 解析 .NET PE File Format

PE OptionalHeader - CLIHeader as well as IMAGE_COR20_HEADER

IMAGE_COR20_HEADER
	Metadata (MetadataHeader)

MetadataHeader
	StreamHeaders
	#~ Table



> 目前解析到了 StreamHeaders结构 即

* #~
* #Strings
* #US
* #GUID
* Blob

![image-20230312163937879](image/image-20230312163937879.png)



> 接下来解析具体的每一张表



![image-20230312163918048](image/image-20230312163918048.png)



> 解析每张表的Rows



![image-20230313153309622](image/image-20230313153309622.png)





> 解析Module,  TypeRef 表中 Rows的数据



![image-20230318150840123](README.assets/image-20230318150840123.png)

