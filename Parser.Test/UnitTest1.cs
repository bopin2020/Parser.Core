using Parser.Core;
using Parser.Core.Dotnet.Tables;
using Parser.Core.Utilites;
using System.Diagnostics;

namespace Parser.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            // Rubeus.exe
            // Seatbelt
            // msgnet.exe
            string testFramework = @"D:\Desktop\petools\sharp\Rubeus.exe";
            DotnetParser dotnetParser = DotnetParser.LoadFile(testFramework);
            dotnetParser.DisposeCallback.Push(() => Console.WriteLine(""));
            dotnetParser.DisposeCallback.Push(() => Console.WriteLine(""));

            Console.WriteLine(dotnetParser.GetDateStamp());
            //Console.WriteLine(dotnetParser.GetStringsStreamUTF8());
            //Console.WriteLine(dotnetParser.GetUSStreamUTF8());
            //Console.WriteLine(dotnetParser.GetBlobStream().HexDump());
            //dotnetParser.OriginalData.HexDump();

            // pinvoke api
            //var imports = dotnetParser.GetMetadataTable<ModuleRef>(MetadataTableType.ModuleRef);

            //foreach (var item in dotnetParser.GetMetadataTable<ImplMapTable>(MetadataTableType.ImplMap))
            //{
            //    Console.WriteLine(imports.ToArray()[item.ImportScope].StringName + "\t"  + item.StringImportName);
            //}

            dotnetParser.MetadataAddr.MemoryDump(48);
            Assert.IsTrue(dotnetParser.IsDotnetPE());
            Assert.IsTrue(dotnetParser.TryDispose(out _));
        }

        [Test]
        public void TestNonPEFile()
        {
            string testFramework = @"D:\Desktop\petools\bof\arp.x64.o";
            PEParser dotnetParser = new PEParserUS(testFramework);
            Assert.IsTrue(dotnetParser.IsPE());
            Assert.Pass();
        }

        [Test]
        public void TestNativePEFile()
        {
            string testFramework = @"c:\windows\system32\cmd.exe";
            PEParser parser = new PEParserUS(testFramework);
            Console.WriteLine(parser.GetDateStamp());
            Assert.Pass();
        }
    }
}