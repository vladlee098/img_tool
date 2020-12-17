using System.Collections.Generic;

namespace src.Interfaces
{
    public interface ITaskValidator
    {
        bool ValidateTasks(List<TaskTypes> taskTypes);
    }
}
