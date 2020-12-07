using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace img_tool.src
{
    public class RenameFile : ITask
    {
        string _mask;

        public RenameFile( List<IOption> options )
        {
            if ( !Validate(options))
            {
                throw new ArgumentException( $"Provided options invalid for RenameByMask, check command line");
            }
        }

        public bool Validate(List<IOption> options)
        {
            var mask = options.SingleOrDefault( x => x.OptionType == OptionTypes.FileName);
            if (mask is null)
            {
                ConsoleLog.WriteError($">> Mask option must be provided for RenameByMask");
                return false;
            }
            _mask = (mask as TextOption).Data;
            return true;        
        }

        public void Apply( FileInfo file, int index )
        {
            var oldName = file.Name;
            var newPath = Path.Combine( file.DirectoryName, _mask + "-" + index.ToString("0000") + file.Extension);
            //file.MoveTo( newPath );
            ConsoleLog.WriteInfo($">> File '{oldName}' has been renamed to: {newPath}");
        }
    }
}

