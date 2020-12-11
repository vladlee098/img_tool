using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using img_tool.src.Interfaces;

namespace img_tool.src.Core
{
    public class TextOption : OptionBase<string>
    {
        public TextOption(string name, OptionTypes optionType, string lineText) : base( name, optionType, lineText)
        {
        }

        public override bool TryParse( string arg )      
        {
            var arr = arg.Split(':');
            if (arr.Length == 2)
            {
                Data = arr[1];
                return true;
            }
            return false;
        }
    }

    public class PathOption : OptionBase<string>
    {
        public PathOption(string name, OptionTypes optionType, string lineText) : base( name, optionType, lineText)
        {
        }

        public override bool TryParse( string arg )      
        {
            var arr = arg.Split(':');
            if (arr.Length == 3)
            {
                Data = Path.Combine( arr[1] + ":" + arr[2] );
            }
            else if (arr.Length == 2)
            {
                Data = arr[1];
            }
            if ( string.IsNullOrEmpty(Data))
            {
                throw new ArgumentException($"Command line argument '{arg}' is invalid. ");
            }

            _dataParsed = Directory.Exists(Data);
            return _dataParsed;
        }
    }

    public class NumberOption : OptionBase<int>
    {
        public NumberOption(string name, OptionTypes optionType, string lineText ) : base( name, optionType, lineText)
        {
        }

        public override bool TryParse( string arg )      
        {
            var arr = arg.Split(':');
            if (arr.Length == 2)
            {
                int val;
                if ( int.TryParse( arr[1], out val) )
                {
                    Data = val; 
                    return true;
                }
                return false;
            }
            return false;
        }
    }

    public class FileAttributeOption : OptionBase<FileAttributes>
    {
        public FileAttributeOption(string name, OptionTypes optionType, string lineText) : base( name, optionType, lineText)
        {
        }

        public override bool TryParse( string arg )      
        {
            var arr = arg.Split(':');
            if (arr.Length == 2)
            {
                var attr = arr[1];
                if ( attr == "H" || attr == "h")
                {
                    this.Data = FileAttributes.Hidden;
                }
                if ( attr == "S" || attr == "s")
                {
                    this.Data = FileAttributes.System;
                }
                if ( attr == "R" || attr == "r")
                {
                    this.Data = FileAttributes.ReadOnly;
                }
                if ( attr == "A" || attr == "a")
                {
                    this.Data = FileAttributes.Archive;
                }
                _dataParsed = true;
            }
            return _dataParsed;
        }
    }

    public class FlagOption : OptionBase<bool>
    {
        public FlagOption(string name, OptionTypes optionType, string lineText) : base( name, optionType, lineText)
        {
        }

        public override bool TryParse( string arg )      
        {
            Data = true;
            return true;
        }
    }

    public class DateOption : OptionBase<DateTime>
    {
        public DateOption(string name, OptionTypes optionType, string lineText ) : base( name, optionType, lineText)
        {
        }

        public override bool TryParse( string arg )      
        {
            var arr = arg.Split(':');
            if (arr.Length == 2)
            {
                DateTime val;
                if ( DateTime.TryParse( arr[1], out val) )
                {
                    Data = val; 
                    return true;
                }
                return false;
            }
            return false;
        }
    }    
}
