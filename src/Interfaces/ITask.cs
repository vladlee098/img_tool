using System.IO;

namespace src.Interfaces
{
    public enum TaskTypes
    {
        RenameFile,
        SetCreateDate,
        DeleteBySize,
        DeleteByAttribute,
        ModifyDateTaken,
        ModifyDateTakenFromFileName,
    };
    
    
    public interface ITask
    {
        void Apply( FileInfo file, int index );
    }
}
