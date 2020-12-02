using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace img_tool.src
{
    public class TextOption : OptionBase<string>
    {
        public TextOption(string name, OptionTypes optionType, string lineText, string defaultValue) : base( name, optionType, lineText, defaultValue)
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
        public PathOption(string name, OptionTypes optionType, string lineText, string defaultValue) : base( name, optionType, lineText, defaultValue)
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
            _dataParsed = Directory.Exists(Data);
            return _dataParsed;
        }
    }

    public class NumberOption : OptionBase<int>
    {
        public NumberOption(string name, OptionTypes optionType, string lineText, int defaultValue ) : base( name, optionType, lineText, defaultValue)
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
        public FileAttributeOption(string name, OptionTypes optionType, string lineText, FileAttributes defaultValue) : base( name, optionType, lineText, defaultValue)
        {
        }

        public override bool TryParse( string arg )      
        {
            var arr = arg.Split(':');
            if (arr.Length == 2)
            {
                var attributes = arr[1].Split( new char[] {';', ','});
                foreach(var attr in attributes)
                {
                    if ( attr == "H" || attr == "h")
                    {
                        this.Data &= FileAttributes.Hidden;
                    }
                    if ( attr == "S" || attr == "s")
                    {
                        this.Data &= FileAttributes.System;
                    }
                    if ( attr == "R" || attr == "r")
                    {
                        this.Data &= FileAttributes.ReadOnly;
                    }
                    if ( attr == "A" || attr == "a")
                    {
                        this.Data &= FileAttributes.Archive;
                    }
                    _dataParsed = true;
                }
            }
            return _dataParsed;
        }
    }

    public class FlagOption : OptionBase<bool>
    {
        public FlagOption(string name, OptionTypes optionType, string lineText, bool defaultValue = false) : base( name, optionType, lineText, defaultValue)
        {
        }

        public override bool TryParse( string arg )      
        {
            Data = true;
            return true;
        }
    }
}
