using System;
using System.Collections.Generic;
using System.Linq;

namespace img_tool.src
{
    public enum TaskTypes
    {
        RenameByDate,
        SetFileDate,
        DeleteBySize,
        DeleteByAttribute
    };
    
    
    public interface ITask
    {
        void Run();
        bool ValidateInputs(List<IOption> options);

    }
}
