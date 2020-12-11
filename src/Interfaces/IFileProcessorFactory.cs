using System.Collections.Generic;

namespace img_tool.src.Interfaces
{
    public interface IFileProcessorFactory
    {
        IProcessor Create(SortedList<int, ITask> tasks, string file, int index, bool verbose, bool force, bool test);
    }
}
