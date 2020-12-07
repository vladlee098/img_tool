using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace img_tool.src
{
    public class SetCreateDate : ITask
    {
        DateTime _datePattern;

        public SetCreateDate( List<IOption> options )
        {
            if ( !Validate(options))
            {
                throw new ArgumentException( $"Provided options invalid for SetFileCreateDate, check command line");
            }
        }

        public bool Validate(List<IOption> options)
        {
            var datePattern = options.SingleOrDefault( x => x.OptionType == OptionTypes.CreationDate);
            if (datePattern is null)
            {
                ConsoleLog.WriteError($">> CreateDate option must be provided for SetFileCreateDate");
                return false;
            }
            _datePattern = (datePattern as DateOption).Data;
            return true;        
        }

        public void Apply( FileInfo file, int index )
        {
            file.CreationTime = _datePattern;
            //File.SetLastWriteTime(file, outDate);
            ConsoleLog.WriteInfo($">> File '{file.FullName}' has been updated, create date now is: {_datePattern}");
        }
    }
}

