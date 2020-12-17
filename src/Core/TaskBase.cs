using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using src.Interfaces;

namespace src.Core
{
    public abstract class TaskBase : OptionReader, ITask
    {
        public TaskBase(List<IOption> options) : base(options)
        {
        }

        public abstract void Apply( FileInfo file, int index );
        
    }
}
