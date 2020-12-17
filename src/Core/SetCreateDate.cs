using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using src.Interfaces;
using src.Shared;

namespace src.Core
{
    public class SetCreateDate : TaskBase
    {
        DateTime _datePattern;

        public SetCreateDate( List<IOption> options ) : base(options)
        {
            if ( !ReadOptions(options))
            {
                throw new ArgumentException( $"Provided options invalid for SetFileCreateDate, check command line");
            }
        }

        protected override bool ReadOptions(List<IOption> options)
        {
            var datePattern = options.SingleOrDefault( x => x.OptionType == OptionTypes.CreationDate);
            if (datePattern is null)
            {
                ConsoleLog.WriteError($">> CreateDate option must be provided for SetFileCreateDate");
                return false;
            }
            _datePattern = (datePattern as DateOption).Data;

            return base.ReadOptions(options);      
        }

        public override void Apply( FileInfo file, int index )
        {
            file.CreationTime = _datePattern;
            if (!_test)
            {
                //File.SetLastWriteTime(file, outDate);
            }
            if ( _verbose )
            {
                ConsoleLog.WriteInfo($">> File '{file.FullName}' has been updated, create date now is: {_datePattern}");
            }
        }
    }
}

