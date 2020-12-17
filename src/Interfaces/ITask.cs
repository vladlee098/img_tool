using System.IO;

namespace src.Interfaces
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
    }
}
