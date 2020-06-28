using System;

namespace stringMatching
{
    class KnuthMorrisPratt : SubstringSearch
    {
        private int[] _table;
        
        public KnuthMorrisPratt(string text, string pattern) : base(text, pattern)
        {
            _table = new int[pattern.Length];
            _start = DateTime.Now;
            Preprocessing();
        } 
        
        /// <summary>
        /// In Preprocessing for KMP find out a prefix-suffix table
        /// for each element in pattern
        /// _table[i] - contains len(longest suffix that's also a prefix) of _substring[0..i-1]
        /// </summary>
        public override void Preprocessing()
        {
            int n = _pattern.Length;
            int i;
            int j;
            _table[0] = 0;

            for (i = 1; i < n; i++) // O(n) for time, O(n) for space
            {
                j = _table[i - 1];
                while (j > 0 && _pattern[j] != _pattern[i])
                {
                    j = _table[j - 1];
                }

                if (_pattern[j] == _pattern[i])
                    j++;

                _table[i] = j;
            }
        }

        public override void Search()
        {
            int textLoc = 0;
            int patLoc = 0;

            for (; textLoc < _text.Length; textLoc++)
            {
                while (patLoc > 0 && _pattern[patLoc] != _text[textLoc])
                {
                    //get new safe alignment for comparing
                    patLoc = _table[patLoc - 1];
                }

                if (_pattern[patLoc] == _text[textLoc])
                {
                    patLoc++;
                    if (patLoc == _pattern.Length)
                    {
                        Matches.Add(textLoc - patLoc + 1);
                        patLoc = 0;
                    }
                }
            }

            ExecTime = (DateTime.Now - _start).Milliseconds;
        }
    }
}