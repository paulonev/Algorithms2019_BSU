using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StableMatching
{ 
    class Program
    {
        static void Main(string[] args)
        {
            /// you're able to write matrices of preferences of each group and call the method
            /// TwoGroupsMatching(int[,] matrixA, int[,] matrixB) to get this pairs
            int[,] studentsPrefs = new int [,]
            {
                {2,1,3,4},
                {1,2,3,4},
                {3,2,1,4},
                {3,2,4,1}
            };
            int[,] hospitalsPrefs = new int [,]
            {
                {2,1,4,3},
                {2,4,1,3},
                {3,4,1,2},
                {3,1,4,2}
            };
  
            /// making AtoB groups matching of 10to10 candidates and outputting the results
            var output = TwoGroupsMatching(size: 10);
            System.Console.WriteLine("Result: ");
            foreach (var pair in output)
                System.Console.WriteLine($"{pair.Key} -> {pair.Value}");
        }

        ///
        /// Algorithm solving stable matching problem. Given 2 sets A, B of equal size N
        /// and preferences - lists of elements of opposite group - foreach in A and each in B 
        /// The goal is to find pairs foreach A with each of B that are stable, i.e shouldn't
        /// be any pair (a1,b1) where exists such (a2,b2) that a1 prefers b2 to b1, and a2 prefers b1 to b2
        ///
        static Dictionary<int, int> TwoGroupsMatching(int size)
        {
            //size = participants
            if (size < 1)
            {
                throw new ArgumentException($"Program is unable to suggest pairs for {size} participants");
            }
            Preferences studs = new Preferences(size);
            Console.WriteLine($"Students preferences: {studs}");
            Preferences hosps = new Preferences(size);
            Console.WriteLine($"Hospitals preferences: {hosps}");
            return GaleShapley(studs, hosps, size);
        }

        static Dictionary<int, int> TwoGroupsMatching(int[,] proposers, int[,] responders)
        {
            int propCnt = proposers.GetLength(1);  //participants
            if (propCnt < 1)
            {
                throw new ArgumentException($"Program is unable to suggest pairs for {propCnt} participants");
            }
            Preferences studs = new Preferences(proposers);
            Console.WriteLine($"Students preferences: {studs}");
            Preferences hosps = new Preferences(responders);
            Console.WriteLine($"Hospitals preferences: {hosps}");
            return GaleShapley(studs, hosps, propCnt);
        }

        ///
        /// The process of finding stable pairs, known as Gale-Shapley algorithm
        /// Aprefs and Bprefs are dictionaries: (elemA, {it's prefs}), (elemB, {it's prefs})
        /// Apairs and Bpairs are dictionaries: (propose_maker, propose_admitter) where proposer_maker being elems from A or from B
        /// they maintain the current pairs of objects
        /// Algorithm terminates when each pair is complete, i.e. there isn't any pair like (a1,0) or (0,b2) - pairsCounter is exactly to control this
        /// Useful link to understanding the concept
        /// https://www.algorithm-archive.org/contents/stable_marriage_problem/stable_marriage_problem.html
        /// 
        static Dictionary<int, int> GaleShapley(Preferences Aprefs, Preferences Bprefs, int propCnt)
        {
            var Apairs = new Dictionary<int, int>();
            var Bpairs = new Dictionary<int, int>();
            foreach (var item in Aprefs.Prefs[1])
            {
                Apairs.Add(item, 0);
                Bpairs.Add(item, 0);
            }
            
            var keyCollection = new List<int>(Apairs.Keys);
            
            int pairsCounter = 0;   //counting created pairs
            while (pairsCounter < propCnt)
            {
                foreach (var a in keyCollection)
                {
                    if (Apairs[a] == 0)   // a doesn't have partner
                    {
                        for (int i=0; i<propCnt; ++i)
                        {
                            int b = Aprefs.Prefs[a][i];
                            if (Bpairs[b] == 0)  // b is free
                            {
                                Apairs[a] = b; 
                                Bpairs[b] = a;
                                pairsCounter++;
                                break;
                            }
                            else if (a != Bpairs[b] && IsPreferableForB(b, newCandidate: a, oldCandidate: Bpairs[b], Bprefs))
                            {
                                Apairs[Bpairs[b]] = 0; //reject previous candidate
                                Apairs[a] = b;  //make pair with new candidate
                                Bpairs[b] = a;
                                break;
                            }
                            // else ++i; // offer next 
                        }
                    }
                }
            }

            return Apairs;
        }

        ///
        /// This is the procedure for checking preferences of b
        /// Returns true - if newCandidate is preferable to b, than oldCandidate
        ///
        static bool IsPreferableForB(int b, int newCandidate, int oldCandidate, Preferences B)
            => B.Prefs[b].IndexOf(newCandidate) < B.Prefs[b].IndexOf(oldCandidate) 
                    ? true : false;
    }

    class Preferences
    {
        /// (itemA, {it's preferences from B}) or (itemB, {it's preferences from A})
        public Dictionary<int, List<int>> Prefs { get; set; }

        public Preferences(int N)
        {
            Prefs = new Dictionary<int, List<int>>(N);
            for(int i=0; i<N; i++)
            {
                var temp = new List<int>(Enumerable.Range(1, N));
                temp.Shuffle();
                Prefs.Add(i+1, temp);
            }
        }

        public Preferences(int[,] matrix)
        {
            int n = matrix.GetLength(1);
            Prefs = new Dictionary<int, List<int>>();
            for (int i = 0; i < n; i++)
            {
                var list = new List<int>(n);
                for (int j = 0; j < n; j++)
                {
                    list.Add(matrix[i,j]);
                }
                Prefs.Add(i+1, list);
            }
        }

        public override string ToString()      
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\n");
            foreach (var key in Prefs.Keys)
            {
                sb.Append($"{key}:[ ");
                foreach (var item in Prefs[key])
                {
                    sb.Append(item + " ");
                }
                sb.Append("],\n");
            }
            sb.Append("}");
            return sb.ToString();
        }
    }

    public static class ListExtensions
    {
        ///
        /// Shuffling list of preferences <list>
        ///
        public static void Shuffle(this IList<int> list)
        {
            var rand = new Random();
            for (int i = list.Count-1; i > 0; i--)
            {
                int j = rand.Next(i+1);
                var t = list[i];
                list[i] = list[j];
                list[j] = t;
            }
        }
    }
}
