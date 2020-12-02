
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
        string _sourceDir;
        string _mask;
        bool _recursive;
        FileAttributes _fileAttribute;

        public DeleteByAttribute( List<IOption> options )
        {
            if ( !ValidateInputs(options))
            {
                throw new ArgumentException( $"Provided options invalid for DeleteByAttribute, check command line");
            }
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

            var fileAttribute = options.SingleOrDefault( x => x.OptionType == OptionTypes.FileAttribute);
            _fileAttribute = fileAttribute is null ? FileAttributes.Archive : (fileAttribute as FileAttributeOption).Data;

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(">> DeleteByAttribute:");
            sb.AppendLine($"   - target directory: {this._sourceDir}");
            sb.AppendLine($"   - file mask: {this._mask}");
            sb.AppendLine($"   - include sub directories: {this._recursive}");
            sb.AppendLine($"   - file attribute: {_fileAttribute.ToString()}");
            return sb.ToString();
        }

        public void Run()
        {
            ConsoleLog.WriteTask( this.ToString());

            var goAhead = ConsoleLog.AskToApprove($">> Confirm delete by attribute task (Y/N): ");
            if (!goAhead)
            {
                return;
            }
                        
            var files = (from file in Directory.EnumerateFiles(
                            _sourceDir,
                            _mask,
                            _recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                        select file).ToList();

            if (files == null || files.Count() == 0)
            {
                ConsoleLog.WriteInfo($"No files found in '{_sourceDir}' with mask: '{_mask}'");
                return;
            }
            ConsoleLog.WriteInfo($">>Found {files.Count()} files inside {_sourceDir}, with mask: '{_mask}'");

            Parallel.For(0, files.Count, index => 
            { 
                var fi = new FileInfo(files[index]);
                if ((fi.Attributes & _fileAttribute) == _fileAttribute)
                {
                    try
                    {
                        //Win32ApiWrapper.MoveToRecycleBin(fi.FullName);
                        ConsoleLog.WriteInfo($">>File '{fi.FullName}' has been deleted, attribute: {_fileAttribute.ToString()}");
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
