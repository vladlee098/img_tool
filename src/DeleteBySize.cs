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
        string _sourceDir;
        string _mask;
        bool _recursive;
        int _maxSize;

        public DeleteBySize( List<IOption> options )
        {
            if ( !ValidateInputs(options))
            {
                throw new ArgumentException( $"Provided options invalid for DeleteBySize, check command line");
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(">> DeleteBySize:");
            sb.AppendLine($"   - target directory: {this._sourceDir}");
            sb.AppendLine($"   - file mask: {this._mask}");
            sb.AppendLine($"   - include sub directories: {this._recursive}");
            sb.AppendLine($"   - max file size: {_maxSize}");
            return sb.ToString();
        }

        public bool ValidateInputs(List<IOption> options)
        {
            var sourceDir = options.SingleOrDefault( x => x.OptionType == OptionTypes.SourceDirectory);
            if (sourceDir is null)
            {
                ConsoleLog.WriteError($">> Invalid SourceDirectory option.");
                return false;
            }
            _sourceDir = (sourceDir as PathOption).Data;

            var mask = options.SingleOrDefault( x => x.OptionType == OptionTypes.FileMask);
            _mask = mask is null ? "*.*" : (mask as TextOption).Data;

            var recursive = options.SingleOrDefault( x => x.OptionType == OptionTypes.IncludeSubDirectories);
            _recursive = recursive is null ? false : true;

            var maxSize = options.SingleOrDefault( x => x.OptionType == OptionTypes.MaxSizeKB);
            _maxSize = maxSize is null ? 1 : (maxSize as NumberOption).Data;

            return true;        }

        public void Run()
        {
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
                ConsoleLog.WriteInfo($">> No files found in {_sourceDir}");
                return;
            }
            ConsoleLog.WriteInfo($">> Found {files.Count()} files inside {_sourceDir}");

            Parallel.For(0, files.Count, index => 
            { 
                var fi = new FileInfo(files[index]);
                if (fi.Length / 1024 <= _maxSize)
                {
                    try
                    {
                        //Win32ApiWrapper.MoveToRecycleBin(fi.FullName);
                        ConsoleLog.WriteInfo($">> File '{fi.FullName}' has been deleted, size: {fi.Length / 1024}KB");
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
