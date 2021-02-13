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
    public class SetDateTakenFromFileName : TaskBase
    {
        DateTime _datePattern;

        public SetDateTakenFromFileName( List<IOption> options ) : base(options)
        {
            if ( !ReadOptions(options))
            {
                throw new ArgumentException( $"Provided options invalid for SetDateTakenFromFileName, check command line");
            }
        }

        protected override bool ReadOptions(List<IOption> options)
        {
            return base.ReadOptions(options);      
        }

        public override void Apply( FileInfo file, int index )
        {
            if (!_test)
            {
                
                var tempFileName = Path.GetFileName(file.FullName);
                if (!DateTime.TryParse(tempFileName.Substring(0,10), out _datePattern))
                {
                    ConsoleLog.WriteWarning($">> Unable to get date pattern from: '{tempFileName}'");
                    return;
                }

                var tempDirName = Path.GetDirectoryName(file.FullName);
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

                Win32ApiWrapper.MoveToRecycleBin(file.FullName);
            }
            if ( _verbose )
            {
                ConsoleLog.WriteInfo($">> Image '{file.FullName}' has been updated, date taken now is: {_datePattern}");
            }
        }
    }
}

