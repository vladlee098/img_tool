using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace img_tool.src
{
    public enum TaskTypes
    {
        RenameFile,
        SetFileDate,
        DeleteBySize,
        DeleteByAttribute
    };
    
    
    public interface ITask
    {
        void Apply( FileInfo file, int index );
        bool Validate(List<IOption> options);

    }
}
