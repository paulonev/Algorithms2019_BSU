using System;
using System.Collections.Generic;

namespace stringMatching
{
    public abstract class SubstringSearch
    {
        protected string _text;
        protected string _pattern;
        protected DateTime _start;
        
        public int ExecTime { get; protected set; }
        public List<int> Matches { get; set; } = new List<int>();

        public SubstringSearch(string text, string pattern)
        {
            _text = text;
            _pattern = pattern;
        }
        
        public abstract void Preprocessing();
        public abstract void Search();

    }
}