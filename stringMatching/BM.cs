using System;
using System.Collections.Generic;

namespace stringMatching
{
    class BoyerMoore : SubstringSearch
    {
        public Dictionary<char, int> Rightmost { get; set; }
        
        public BoyerMoore(string text, string pattern) : base(text, pattern)
        {
            Rightmost = new Dictionary<char, int>();
            _start = DateTime.Now;
            Preprocessing();
        }
        public override void Preprocessing()
        {
            for (int i = 0; i < _pattern.Length; i++)
            {
                if (!Rightmost.ContainsKey(_pattern[i]))
                {
                    Rightmost.Add(_pattern[i], i);
                }
                else
                    Rightmost[_pattern[i]] = i;
            }
        }

        public override void Search()
        {
            int i = 0; //index of 1st substring[0] in text
            int patLast = _pattern.Length - 1;
            int j; //point to last char in substring

            while (i <= _text.Length - _pattern.Length)
            {
                for (j = patLast; j >= 0; --j)
                {
                    if (_text[i + j] != _pattern[j])
                    {
                        int slide = Rightmost.ContainsKey(_text[i + j])
                            ? Rightmost[_text[i + j]]
                            : -1;
                        if (slide < j)
                            i += j - slide;
                        else
                            i++;
                        break;
                    }
                }
                
                if (j == -1)
                {
                    Matches.Add(i);
                    i += _pattern.Length;
                }
            }
            
            ExecTime = (DateTime.Now - _start).Milliseconds;
        }
    }
}