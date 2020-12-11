using System;
using System.Collections.Generic;
using img_tool.src.Core;
using img_tool.src.Interfaces;
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
