using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using src.Interfaces;

namespace src.Core
{
    public class OptionFactory
    {
        static readonly List<IOption> _options = new List<IOption>();

        static OptionFactory()
        {
            _options.Add( new TextOption( "FileMask", OptionTypes.FileMask, "-f") );
            _options.Add( new TextOption( "FileNamePrefix", OptionTypes.FileName, "-d") );
            _options.Add( new DateOption( "SetCreationDate", OptionTypes.CreationDate, "-c") );
            _options.Add( new NumberOption( "MaxSizeKB", OptionTypes.MaxSizeKB, "-z") );
            _options.Add( new PathOption( "SourceDirectory", OptionTypes.SourceDirectory, "-i") );
            _options.Add( new FlagOption( "No questions asked, applies action silently", OptionTypes.Force, "-force") );
            _options.Add( new FlagOption( "Provides additional output to the console", OptionTypes.Verbose, "-verbose") );
            _options.Add( new FlagOption( "Does not apply action, just shows output", OptionTypes.Test, "-test") );
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
