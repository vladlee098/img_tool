using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using src.Interfaces;
using src.Shared;

namespace src.Core
{
    public class DeleteByAttribute : TaskBase
    {
        FileAttributes _fileAttribute = FileAttributes.Archive;

        public DeleteByAttribute( List<IOption> options ) : base(options)
        {
            if ( !ReadOptions(options))
            {
                throw new ArgumentException( $"Provided options invalid for DeleteByAttribute, check command line");
            }
        }

        protected override  bool ReadOptions(List<IOption> options)
        {
            var fileAttribute = options.SingleOrDefault( x => x.OptionType == OptionTypes.FileAttribute);
            _fileAttribute = fileAttribute is null ? FileAttributes.Archive : (fileAttribute as FileAttributeOption).Data;

            return base.ReadOptions(options);        
        }

        // public override string ToString()
        // {
        //     var sb = new StringBuilder();
        //     sb.AppendLine(">> DeleteByAttribute:");
        //     sb.AppendLine($"   - target directory: {this._sourceDir}");
        //     sb.AppendLine($"   - file mask: {this._mask}");
        //     sb.AppendLine($"   - include sub directories: {this._recursive}");
        //     sb.AppendLine($"   - file attribute: {_fileAttribute.ToString()}");
        //     return sb.ToString();
        // }

        public override void Apply(FileInfo file, int index)
        {
            if ((file.Attributes & _fileAttribute) == _fileAttribute)
            {
                try
                {
                    if (!_test)
                    {
                        //Win32ApiWrapper.MoveToRecycleBin(fi.FullName);
                    }

                    if ( _verbose )
                    {
                        ConsoleLog.WriteInfo($">>File '{file.FullName}' has been moved to RecycleBin, attribute: {_fileAttribute.ToString()}");
                    }
                }
                catch (AggregateException aex)
                {
                    ConsoleLog.WriteError($"Error while deleting '{file.FullName}': {aex.Message}");
                }
                catch (IOException ex)
                {
                    ConsoleLog.WriteError($"Error while deleting '{file.FullName}': {ex.Message}");
                }
            }                }
    }
}
