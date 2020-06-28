using System;

namespace stringMatching
{
    class RabinKarp : SubstringSearch
    {
        private int _radix;
        private int _bigPrime;
        private int _rm;
        private long _patternHash;
        
        public RabinKarp(string text, string pattern) : base(text, pattern)
        {
            _radix = 256;  //unicode table size
            _bigPrime = 997;
            _start = DateTime.Now;
            Preprocessing();
        }
        
        public override void Preprocessing()
        {
            _rm = 1;
            for (int i = 1; i < _pattern.Length; i++)
            {
                _rm = (_radix * _rm) % _bigPrime;
            }

            _patternHash = GetHash(_pattern);
        }

        public override void Search()
        {
            int n = _text.Length;
            int m = _pattern.Length;

            var substring = _text.Substring(0, m);
            long substringHash = GetHash(substring);
            if (_patternHash == substringHash && _pattern.Equals(substring))
                Matches.Add(item: 0);
            
            for (int i = 1; i < n-m; i++)
            {
                substring = _text.Substring(i, m);
                substringHash = (substringHash + _bigPrime - _rm * _text[i - 1] % _bigPrime) % _bigPrime;
                substringHash = (substringHash * _radix + _text[i + m - 1]) % _bigPrime;
                if (_patternHash == substringHash && _pattern.Equals(substring))
                    Matches.Add(item: i);
            }
            
            ExecTime = (DateTime.Now - _start).Milliseconds;
        }

        //Horner's rule
        private long GetHash(string s)
        {
            int m = s.Length;
            long hash = 0;
            for (int i = 0; i < m; i++)
            {
                hash = (_radix * hash + s[i]) % _bigPrime;
            }

            return hash;
        }
    }
}