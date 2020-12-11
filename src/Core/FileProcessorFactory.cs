using System.Collections.Generic;
using img_tool.src.Interfaces;

namespace img_tool.src.Core
{   
    public class FileProcessorFactory : IFileProcessorFactory
    {
        public virtual IProcessor Create(SortedList<int, ITask> tasks, string file, int index, bool verbose, bool force, bool test)
        {
            return new FileProcessor( tasks, file, index, verbose, force, test);
        }
    }
}
