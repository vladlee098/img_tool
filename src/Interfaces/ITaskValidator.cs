using System.Collections.Generic;

namespace img_tool.src.Interfaces
{
    public interface ITaskValidator
    {
        bool ValidateTasks(List<TaskTypes> taskTypes);
    }
}
