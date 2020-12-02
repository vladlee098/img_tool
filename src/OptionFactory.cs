using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace img_tool.src
{
    public class OptionFactory
    {
        static readonly List<IOption> _options = new List<IOption>();

        static OptionFactory()
        {
            _options.Add( new TextOption( "FileSearch", OptionTypes.FileMask, "-f") );
            _options.Add( new TextOption( "DatePattern", OptionTypes.DatePattern, "-p") );
            _options.Add( new FlagOption( "SetCreationDate", OptionTypes.SetCreationDate, "-c") );
            _options.Add( new FlagOption( "SetLastWriteDate", OptionTypes.SetLastWriteDate, "-w") );
            _options.Add( new NumberOption( "MaxSizeKB", OptionTypes.MaxSizeKB, "-z") );
            _options.Add( new PathOption( "SourceDirectory", OptionTypes.SourceDirectory, "-i") );
            //_options.Add( new PathOption( "TargetDirectory", OptionTypes.TargetDirectory, "-t") );
            _options.Add( new FlagOption( "No questions asked, just delete", OptionTypes.IncludeSubDirectories, "-force") );
            _options.Add( new FlagOption( "IncludeSubDirectories", OptionTypes.IncludeSubDirectories, "-r") );
            _options.Add( new FileAttributeOption( "FileAttribute", OptionTypes.FileAttribute, "-a") );
        }

        public static IOption Find( string arg )
        {
            if ( string.IsNullOrEmpty(arg) )
            {
                throw new ArgumentNullException("arg");
            }
            
            var option = _options.FirstOrDefault( x => arg.StartsWith(x.LineText) );
            if (option is null)
            {
                //ConsoleLog.WriteError($">> Invalid argument {arg}.");
                return null;
            }

            return option.TryParse( arg ) ? option : null;
        }
    }
}
