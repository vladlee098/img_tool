using System;
using img_tool.src;
using NUnit.Framework;

namespace img_tool.test
{
    [TestFixture]
    public class CommandLineTests
    {

        [Test]
        public void DeleteBySize_Test()
        {
            string[] bySizeArgs = new string[] { "ds", @"-i:e:\@@@test" , "-z:5", "-r", "-w", "-f:*Rot*"};

            var (tasks, options) = ArgParser.Parse(bySizeArgs);
            Assert.AreEqual( 1, tasks.Count);
            Assert.AreEqual( 5, options.Count);
        }

        [Test]
        public void DeleteBySize_NoSourceDir()
        {
            string[] bySizeArgs = new string[] { "ds", "da", "-z:5", "-r", "-w", "-f:*Rot*"};

            var (tasks, options) = ArgParser.Parse(bySizeArgs);
            Assert.AreEqual( 1, tasks.Count);
            Assert.AreEqual( 5, options.Count);
        }

        [Test]
        public void DeleteBySize_InvalidSourceDir()
        {
            string[] testArgs = new string[] { "rd", "-d" , "-i"};

            Assert.Throws<ArgumentException>(() => ArgParser.Parse(testArgs));
        }
    }
}
