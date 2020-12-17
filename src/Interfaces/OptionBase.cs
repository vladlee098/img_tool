namespace src.Interfaces
{
    public abstract class OptionBase<T> : IOption
    {
        protected bool _dataParsed = false;

        public T DefaultValue { get; private set; }
        public OptionTypes OptionType { get; private set; }
        public readonly string Name;
        public string LineText { get; private set; }
        public virtual T Data { get; protected set; }

        public OptionBase( string name, OptionTypes optionType, string lineText)
        {
            Name = name;
            OptionType = optionType;
            LineText = lineText;
        }

        public abstract bool TryParse( string arg );
    }    
}
