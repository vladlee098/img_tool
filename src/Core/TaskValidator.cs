using System.Collections.Generic;
using img_tool.src.Interfaces;
using img_tool.src.Shared;

namespace img_tool.src.Core
{
    public class TaskValidator : ITaskValidator
    {
        public virtual bool ValidateTasks(List<TaskTypes> taskTypes)
        {
            if (taskTypes.Count == 0)
            {
                ConsoleLog.WriteError($">> No commands found in arguments.");
                return false;
            }            

            if (taskTypes.Count > 1 )
            {
                ConsoleLog.WriteError($">> Can only process one file command at a time.");
                return false;
            }

            return true;
        }
    }
}
