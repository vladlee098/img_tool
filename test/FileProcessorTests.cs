using System;
using img_tool.src.Core;
using NUnit.Framework;

namespace img_tool.test
{
    [TestFixture]
    public class FileProcessorTests
    {
        [Test]
        public void ExecuteTasks_Test()
        {
            string[] bySizeArgs = new string[] { "ds", @"-i:e:\@@@test" , "-z:5", "-r", "-f:*Rot*", "-force"};

            var (tasks, options) = ArgParser.Parse(bySizeArgs, new TaskValidator());
            Assert.AreEqual( 1, tasks.Count);
            Assert.AreEqual( 4, options.Count);

            var factory = new MockFileProcessorFactory();
            var processor = factory.Create( tasks, string.Empty, 0, false, true, true);
            processor.ApplyTasks();

        }        
    }
}
