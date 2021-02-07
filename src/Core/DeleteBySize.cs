using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using src.Interfaces;
using src.Shared;

namespace src.Core
{
    public class DeleteBySize : TaskBase
    {
        int _maxSize = 1;

        public DeleteBySize( List<IOption> options ) : base(options)
        {
            if ( !ReadOptions(options))
            {
                throw new ArgumentException( $"Provided options invalid for DeleteBySize, check command line");
            }
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

        protected override bool ReadOptions(List<IOption> options)
        {
            var maxSize = options.SingleOrDefault( x => x.OptionType == OptionTypes.MaxSizeKB);
            _maxSize = maxSize is null ? 1 : (maxSize as NumberOption).Data;
            
            return base.ReadOptions(options);        
        }

        public override void Apply( FileInfo file, int index )
        {
            if ((file.Length / 1024) < _maxSize)
            {
                if (!_test)
                {
                    Win32ApiWrapper.MoveToRecycleBin(file.FullName);
                }

                if ( _verbose )
                {
                    ConsoleLog.WriteInfo($">> File '{file.FullName}' has been moved to RecycleBin, size: {file.Length / 1024}KB");
                }
            }
        }
    }
}
