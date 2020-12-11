using System.Collections.Generic;

namespace img_tool.src.Interfaces
{
    public abstract class OptionReaderBase
    {
        protected abstract bool ReadOptions(List<IOption> options);
    }
}
