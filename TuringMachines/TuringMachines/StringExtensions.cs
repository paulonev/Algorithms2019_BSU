namespace TuringMachines
{
    public static class StringExtensions
    {
        /// <summary>
        /// Function that builds a prefix-suffix table for a pattern <value>{pat}</value>
        /// For i-th symbol of pattern
        /// table[i] - contains len(longest suffix that's also a prefix) of pat[0..i-1]
        /// </summary>
        /// <param name="pat"></param>
        /// <returns></returns>
        public static int[] PrefixSuffix(this string pat)
        {
            int n = pat.Length;
            int[] fail = new int[n];
            int i;
            int j;
            fail[0] = 0;

            for (i = 1; i < n; i++)
            {
                j = fail[i - 1];
                while (j > 0 && pat[j] != pat[i])
                {
                    j = fail[j - 1];
                }

                if (pat[j] == pat[i])
                    j++;

                fail[i] = j;
            }

            return fail;
        }
    }
}