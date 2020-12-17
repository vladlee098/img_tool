using System;
using System.Collections.Generic;
using src.Core;
using src.Interfaces;
using NUnit.Framework;

namespace img_tool.test
{
    [TestFixture]
    public class TaskValidatorTests
    {
        [Test]
        public void TwoTasksError()
        {
           var validator = new TaskValidator();
           var taskTypes = new List<TaskTypes>() { TaskTypes.DeleteByAttribute, TaskTypes.DeleteBySize };
           Assert.False(validator.ValidateTasks(taskTypes));
        }        

        [Test]
        public void NoTasksError()
        {
           var validator = new TaskValidator();
           var taskTypes = new List<TaskTypes>();
           Assert.False(validator.ValidateTasks(taskTypes));
        }        
    }
}
