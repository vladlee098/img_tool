
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace img_tool.src
{
    public sealed class DeleteByAttribute : ITask
    {
        string _targetDir;
        string _mask;
        bool _recursive;
        FileAttributes _fileAttribute;

        public DeleteByAttribute( List<IOption> options )
        {
            _targetDir = (options.Single( x => x.OptionType == OptionTypes.TargetDirectory) as PathOption).Data;
            _mask = (options.Single( x => x.OptionType == OptionTypes.FileMask) as TextOption).Data;
            _recursive = (options.Single( x => x.OptionType == OptionTypes.IncludeSubDirectories) as FlagOption).Data;
            _fileAttribute = (options.Single( x => x.OptionType == OptionTypes.FileAttribute) as FileAttributeOption).Data;
        }

        public bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(_mask))
            {
                this._mask = "*.*";
            }
            if ( (this._fileAttribute & FileAttributes.Archive) == FileAttributes.Archive )
            {
                ConsoleLog.WriteError($">> Invalid FileAttribute argument, must be H, R or S");
                return false;
            }
            if (string.IsNullOrEmpty(_targetDir))
            {
                this._targetDir = ".";
            }
            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(">> DeleteByAttribute:");
            sb.AppendLine($"   - target directory: {this._targetDir}");
            sb.AppendLine($"   - file mask: {this._mask}");
            sb.AppendLine($"   - include sub directories: {this._recursive}");
            sb.AppendLine($"   - file attribute: {_fileAttribute.ToString()}");
            return sb.ToString();
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
                if ((fi.Attributes & _fileAttribute) == _fileAttribute)
                {
                    try
                    {
                        //Win32ApiWrapper.MoveToRecycleBin(fi.FullName);
                        Console.WriteLine($">>File '{fi.FullName}' has been deleted.");
                    }
                    catch (AggregateException aex)
                    {
                        ConsoleLog.WriteError($"Error while deleting '{files[index]}': {aex.Message}");
                    }
                    catch (IOException ex)
                    {
                        ConsoleLog.WriteError($"Error while deleting '{files[index]}': {ex.Message}");
                    }
                }
            } );           
        }
    }
}
