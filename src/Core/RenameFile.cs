using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using src.Interfaces;
using src.Shared;

namespace src.Core
{
    public class RenameFile : TaskBase
    {
        string _mask;

        public RenameFile( List<IOption> options ) : base(options)
        {
            if ( !ReadOptions(options))
            {
                throw new ArgumentException( $"Provided options invalid for RenameByMask, check command line");
            }
        }

        protected override bool ReadOptions(List<IOption> options)
        {
            var mask = options.SingleOrDefault( x => x.OptionType == OptionTypes.FileName);
            if (mask is null)
            {
                ConsoleLog.WriteError($">> Mask option must be provided for RenameByMask");
                return false;
            }
            _mask = (mask as TextOption).Data;
            
            return base.ReadOptions(options);      
        }

        public override void Apply( FileInfo file, int index )
        {
            var oldName = file.Name;
            var newPath = Path.Combine( file.DirectoryName, _mask + "-" + index.ToString("0000") + file.Extension);
            if (!_test)
            {
                //file.MoveTo( newPath );
            }
            if ( _verbose )
            {
                ConsoleLog.WriteInfo($">> File '{oldName}' has been renamed to: {newPath}");
            }
        }
    }
}

