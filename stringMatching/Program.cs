using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace stringMatching
{
    public static class ListExtensions
    {
        public static string GetMatchesToString(this List<int> matches)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Matches: [");
            foreach (var item in matches)
            {
                sb.Append(item + ", ");
            }
            
            var res = sb.ToString().TrimEnd(',', ' ');
            res += "]";
            return res;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
//            string text = "agstttsggsatgatasggstgtasgt";
//            string pattern = "sggs";
            
            using StreamReader sr =
                new StreamReader("/home/paul/coding/algorithms-data-structures/TuringMachines/TuringMachines/data/worldandpeace_oneline.txt");
            
            string text = sr.ReadLine()?.Trim();

            List<string> patterns = new List<string>
            {
                {"князь Андрей"},
                {"должно было случиться что-нибудь важное и несчастливое"},
                {"карета для Наташи"}
            };

            foreach (var pattern in patterns)
            {
                Console.WriteLine($"== Finding matches of \'{pattern}\' ==");

                SubstringSearch kmp = new KnuthMorrisPratt(text: text, pattern: pattern);
                kmp.Search();
                Console.WriteLine($"KMP: {kmp.ExecTime}");
                
                SubstringSearch bm = new BoyerMoore(text: text, pattern: pattern);
                bm.Search();
                Console.WriteLine($"BM: {bm.ExecTime}");

                SubstringSearch rk = new RabinKarp(text: text, pattern: pattern);
                rk.Search();
                Console.WriteLine($"RK: {rk.ExecTime}");
                
                Console.WriteLine(kmp.Matches.GetMatchesToString());
            }
        }
    }
    
}



//            string text = Console.ReadLine();
//            
//            Trie trie = TrieBuilder.BuildSuffixTrie(text);
//            Trie tree = TreeBuilder.BuildTreeFromTrie(trie);
//
//            Console.WriteLine("Output:");
//            tree.TraverseTrie();
//            
//            end of working version
//            int n = int.Parse(Console.ReadLine());
//            List<string> patterns = new List<string>();
//
//            for (int i=0; i<n; i++)
//            {
//                string pat = Console.ReadLine();
//                patterns.Add(pat);
//            }

//            Trie trie = new Trie(patterns);
////            Console.WriteLine(trie.ToString());
//
//            var matches = trie.TrieMatching(text);
//            foreach (var i in matches)
//            {
//                Console.Write(i.ToString() + ' ');
//            }
//            Console.WriteLine();
