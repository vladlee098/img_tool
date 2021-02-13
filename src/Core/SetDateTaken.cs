using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using src.Interfaces;
using src.Shared;

namespace src.Core
{
    public class SetDateTaken : TaskBase
    {
        DateTime _datePattern;

        public SetDateTaken( List<IOption> options ) : base(options)
        {
            if ( !ReadOptions(options))
            {
                throw new ArgumentException( $"Provided options invalid for SetDateTaken, check command line");
            }
        }

        protected override bool ReadOptions(List<IOption> options)
        {
            var datePattern = options.SingleOrDefault( x => x.OptionType == OptionTypes.CreationDate);
            if (datePattern is null)
            {
                ConsoleLog.WriteError($">> CreateDate option must be provided for SetDateTaken");
                return false;
            }
            _datePattern = (datePattern as DateOption).Data;

            return base.ReadOptions(options);      
        }

        public override void Apply( FileInfo file, int index )
        {
            if (!_test)
            {
                var tempDirName = Path.GetDirectoryName(file.FullName);
                var tempFileName = Path.GetFileName(file.FullName);
                var newName = Path.Combine(tempDirName, "_" + tempFileName);

                using ( var image = new Bitmap(file.FullName) )
                {
                    image.ModifyDateTaken(_datePattern);
                    image.Save(newName);

                    var newFile = new FileInfo(newName);
                    newFile.CreationTime = _datePattern;
                    newFile.LastAccessTime = _datePattern;
                    newFile.LastWriteTime = _datePattern;
                }
            }
            if ( _verbose )
            {
                ConsoleLog.WriteInfo($">> Image '{file.FullName}' has been updated, date taken now is: {_datePattern}");
            }
        }
    }
}

