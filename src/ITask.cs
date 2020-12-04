using System;
using System.Collections.Generic;
using System.IO;
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
        void Apply( FileInfo file );
        bool Validate(List<IOption> options);

    }
}
