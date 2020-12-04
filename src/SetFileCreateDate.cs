using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace img_tool.src
{
    public class SetFileCreateDate : ITask
    {
        DateTime _createDate;

        public SetFileCreateDate( List<IOption> options )
        {
            if ( !Validate(options))
            {
                throw new ArgumentException( $"Provided options invalid for SetFileCreateDate, check command line");
            }
        }

        public bool Validate(List<IOption> options)
        {
            var createDate = options.SingleOrDefault( x => x.OptionType == OptionTypes.CreationDate);
            if (createDate is null)
            {
                ConsoleLog.WriteError($">> CreateDate option must be provided for SetFileCreateDate");
            }
            _createDate = (createDate as DateOption).Data;
            return true;        
        }

        public void Apply( FileInfo file )
        {
            file.CreationTime = _createDate;
            //File.SetLastWriteTime(file, outDate);
            ConsoleLog.WriteInfo($">> File '{file.FullName}' has been updated, create date now is: {_createDate}");
        }
    }
}

