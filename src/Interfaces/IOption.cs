namespace src.Interfaces
{
    public enum OptionTypes
    {
        FileMask,
        FileName,
        CreationDate,
        LastWriteDate,
        MaxSizeKB,
        SourceDirectory,
        TargetDirectory,
        IncludeSubDirectories,
        FileAttribute,
        Verbose,
        Force,
        Test
    };

    public interface IOption
    {
        string LineText { get; }
        OptionTypes OptionType { get; }
        bool TryParse( string arg );   
    }
}
