
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace img_tool.src
{
    public class DeleteByAttribute : ITask
    {
        FileAttributes _fileAttribute = FileAttributes.Archive;

        public DeleteByAttribute( List<IOption> options )
        {
            if ( !Validate(options))
            {
                throw new ArgumentException( $"Provided options invalid for DeleteByAttribute, check command line");
            }
        }

        public DeleteByAttribute()
        {
        }

        public bool Validate(List<IOption> options)
        {
            var fileAttribute = options.SingleOrDefault( x => x.OptionType == OptionTypes.FileAttribute);
            _fileAttribute = fileAttribute is null ? FileAttributes.Archive : (fileAttribute as FileAttributeOption).Data;
            return true;
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

        public void Apply(FileInfo file, int index)
        {
            if ((file.Attributes & _fileAttribute) == _fileAttribute)
            {
                try
                {
                    //Win32ApiWrapper.MoveToRecycleBin(fi.FullName);
                    ConsoleLog.WriteInfo($">>File '{file.FullName}' has been moved to RecycleBin, attribute: {_fileAttribute.ToString()}");
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
