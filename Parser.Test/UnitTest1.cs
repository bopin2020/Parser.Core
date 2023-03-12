using Parser.Core;

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
            string testFramework = @"D:\Desktop\petools\sharp\msgnet.exe";
            DotnetParser dotnetParser = new DotnetParserUS(testFramework);
            Assert.IsTrue(dotnetParser.IsDotnetPE());
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