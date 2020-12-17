using System.Collections.Generic;

namespace src.Interfaces
{
    public abstract class OptionReaderBase
    {
        protected abstract bool ReadOptions(List<IOption> options);
    }
}
