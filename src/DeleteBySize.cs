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
        int _maxSize;

        public DeleteBySize( List<IOption> options )
        {
            if ( !Validate(options))
            {
                throw new ArgumentException( $"Provided options invalid for DeleteBySize, check command line");
            }
        }

        public DeleteBySize()
        {
            _maxSize = 1; // 1 KB by defalt
        }

        // public override string ToString()
        // {
        //     var sb = new StringBuilder();
        //     sb.AppendLine(">> DeleteBySize:");
        //     sb.AppendLine($"   - target directory: {this._sourceDir}");
        //     sb.AppendLine($"   - file mask: {this._mask}");
        //     sb.AppendLine($"   - include sub directories: {this._recursive}");
        //     sb.AppendLine($"   - max file size: {_maxSize}");
        //     return sb.ToString();
        // }

        public bool Validate(List<IOption> options)
        {
            var maxSize = options.SingleOrDefault( x => x.OptionType == OptionTypes.MaxSizeKB);
            _maxSize = maxSize is null ? 1 : (maxSize as NumberOption).Data;
            return true;        
        }

        public void Apply( FileInfo file )
        {
            if ((file.Length / 1024) < _maxSize)
            {
                //Win32ApiWrapper.MoveToRecycleBin(fi.FullName);
                ConsoleLog.WriteInfo($">> File '{file.FullName}' has been moved to RecycleBin, size: {file.Length / 1024}KB");
            }
        }
    }
}
