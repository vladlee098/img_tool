﻿using System;
using src.Core;
using NUnit.Framework;

namespace img_tool.test
{
    [TestFixture]
    public class ArgParserTests
    {

        [Test]
        public void DeleteBySize_Test()
        {
            string[] bySizeArgs = new string[] { "ds", @"-i:e:\@@@test" , "-z:5", "-r", "-f:*Rot*"};

            var (tasks, options) = ArgParser.Parse(bySizeArgs, new TaskValidator());
            Assert.AreEqual( 1, tasks.Count);
            Assert.AreEqual( 4, options.Count);
        }

        [Test]
        public void DeleteBySize_DeleteByAttribute_TwoTasksError()
        {
           string[] testArgs = new string[] { "ds", "da", "-z:5", "-r", "-w", "-f:*Rot*" };
           Assert.Throws<ArgumentException>(() => ArgParser.Parse(testArgs, new TaskValidator()));
        }

        [Test]
        public void DeleteBySize_NoSourceDir()
        {
            string[] bySizeArgs = new string[] { "ds", "-z:5", "-r", "-f:*Rot*" };

            var (tasks, options) = ArgParser.Parse(bySizeArgs, new TaskValidator());
            Assert.AreEqual(1, tasks.Count);
            Assert.AreEqual(3, options.Count);
        }

        [Test]
        public void DeleteBySize_InvalidSourceDir()
        {
            string[] testArgs = new string[] { "rd", "-d" , "-i"};

            Assert.Throws<ArgumentException>(() => ArgParser.Parse(testArgs, new TaskValidator()));
        }

        [Test]
        public void SetFileCreateDate_NoDateError()
        {
            string[] testArgs = new string[] { "cd", "-d" , @"-i:e:\@@@test", "-f:*Rot*" };

            Assert.Throws<ArgumentException>(() => ArgParser.Parse(testArgs, new TaskValidator() ));
        }
    }
}
