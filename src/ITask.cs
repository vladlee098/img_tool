using System;
using System.Linq;

namespace img_tool.src
{
    public enum TaskTypes
    {
        RenameByDateMask,
        SetFileDate,
        DeleteBySize,
        DeleteByAttribute
    };
    
    
    public interface ITask
    {
        void Run();
        bool ValidateInputs();

    }
}
