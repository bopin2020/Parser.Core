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
    }
}