using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace img_tool.src
{
    public class DeleteBySize : ITask
    {
        string _targetDir;
        string _mask;
        bool _recursive;
        int _maxSize;

        public DeleteBySize( List<IOption> options )
        {
            // Single will throw exception if not found, so validations must be done before task creation
            _targetDir = (options.Single( x => x.OptionType == OptionTypes.TargetDirectory) as PathOption).Data;
            _mask = (options.Single( x => x.OptionType == OptionTypes.FileMask) as TextOption).Data;
            _recursive = (options.Single( x => x.OptionType == OptionTypes.IncludeSubDirectories) as FlagOption).Data;
            _maxSize = (options.Single( x => x.OptionType == OptionTypes.MaxSizeKB) as NumberOption).Data;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(">> DeleteBySize:");
            sb.AppendLine($"   - target directory: {this._targetDir}");
            sb.AppendLine($"   - file mask: {this._mask}");
            sb.AppendLine($"   - include sub directories: {this._recursive}");
            sb.AppendLine($"   - max file size: {_maxSize}");
            return sb.ToString();
        }

        public bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(_mask))
            {
                this._mask = "*.*";
            }
            if (this._maxSize <= 0)
            {
                this._maxSize = 1;
            }
            if (string.IsNullOrEmpty(_targetDir))
            {
                this._targetDir = ".";
            }
            return true;
        }

        public void Run()
        {
            if ( !ValidateInputs() )
            {
                ConsoleLog.WriteError($">> Invalid inputs, check command line");
                return;
            }
            
            ConsoleLog.WriteTask( this.ToString());

            var goAhead = ConsoleLog.AskToApprove($">> Confirm delete by attribute task (Y/N): ");
            if (!goAhead)
            {
                return;
            }            
                        
            var wildcards = _mask.Split( new char[] { ';', ',' });

            var files = (from wc in wildcards
                        let dirName = Path.GetDirectoryName(wc)
                        let fileName = Path.GetFileName(wc)
                        from file in Directory.EnumerateFiles(
                            string.IsNullOrWhiteSpace(dirName) ? "." : dirName,
                            string.IsNullOrWhiteSpace(fileName) ? "*.*" : fileName,
                            _recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                        select file).ToList();

            if (files == null || files.Count() == 0)
            {
                Console.WriteLine($"No files found in {_targetDir}");
                return;
            }
            Console.WriteLine($"Found {files.Count()} files inside {_targetDir}");

            Parallel.For(0, files.Count, index => 
            { 
                var fi = new FileInfo(files[index]);
                if (fi.Length / 1024 <= _maxSize)
                {
                    try
                    {
                        //Win32ApiWrapper.MoveToRecycleBin(fi.FullName);
                        Console.WriteLine($">>File '{fi.FullName}' has been deleted.");
                    }
                    catch (IOException ex)
                    {
                        var saved = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error while deleting '{files[index]}': {ex.Message}");
                        Console.ForegroundColor = saved;
                    }
                }
            } );           
        }
    }
}
