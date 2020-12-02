using System;
using System.Collections.Generic;
using System.IO;

namespace img_tool.src
{
    public enum OptionTypes
    {
        FileMask,
        DatePattern,
        SetCreationDate,
        SetLastWriteDate,
        MaxSizeKB,
        SourceDirectory,
        TargetDirectory,
        IncludeSubDirectories,
        FileAttribute
    };

    public interface IOption
    {
        string LineText { get; }
        OptionTypes OptionType { get; }
        bool TryParse( string arg );   
    }

    public abstract class OptionBase<T> : IOption
    {
        protected bool _dataParsed = false;

        public T DefaultValue { get; private set; }
        public OptionTypes OptionType { get; private set; }
        public readonly string Name;
        public string LineText { get; private set; }
        public virtual T Data { get; protected set; }

        public OptionBase( string name, OptionTypes optionType, string lineText, T defaultValue)
        {
            Name = name;
            OptionType = optionType;
            LineText = lineText;
            DefaultValue = defaultValue;
        }

        public abstract bool TryParse( string arg );
    }    
}
