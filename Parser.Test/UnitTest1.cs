using Parser.Core;
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
            // msgnet.exe
            string testFramework = @"D:\Desktop\petools\sharp\msgnet.exe";
            DotnetParser dotnetParser = DotnetParser.LoadFile(testFramework);
            dotnetParser.DisposeCallback.Push(() => Console.WriteLine("1"));
            dotnetParser.DisposeCallback.Push(() => Console.WriteLine("2"));

            Console.WriteLine(dotnetParser.GetDateStamp());
            //dotnetParser.OriginalData.HexDump();
            dotnetParser.MetadataAddr.MemoryDump(48);

            Assert.IsTrue(dotnetParser.IsDotnetPE());
            Assert.IsTrue(dotnetParser.TryDispose(out _));
            Assert.Pass();
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