using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using src.Interfaces;

namespace src.Core
{    
    public class OptionReader : OptionReaderBase
    {
        protected bool _verbose;
        protected bool _force;
        protected bool _test;

        protected readonly List<IOption> _options;

        public OptionReader(List<IOption> options)
        {
            _options = options;
        }
        
        protected override bool ReadOptions(List<IOption> options)
        {
            var force = options.SingleOrDefault( x => x.OptionType == OptionTypes.Force);
            _force = force is null ? false : true;

            var verbose = options.SingleOrDefault( x => x.OptionType == OptionTypes.Verbose);
            _verbose = verbose is null ? false : true;

            var test = options.SingleOrDefault( x => x.OptionType == OptionTypes.Test);
            _test = test is null ? false : true;

            return true;
        }
    }
}
