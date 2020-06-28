using System;
using NUnit.Framework;
using searchMethods.ArrayGenerator;
using System.Diagnostics; //for Stopwatch implementation
using searchMethods.Extensions;

namespace searchMethods.Tests
{
    [TestFixture]
    public class searchingTests
    {
        [Test]        
        public void FindWithLinearSearch()
        {
            int[] array = new int[]{-10,1,4,10,15,22,34,35,44,111};
            //int[] array2 = null;
            int key = 22;

            int result = (new SearchMethods()).LinearSearch(array,key);

            int expected = 5;
            
            Assert.AreEqual(result, expected);
                                               
        }

        [Test]
        public void FindWithInterpolationSearch()
        {
            int[] array = new int[]{-10,1,4,10,15,22,34,35,44,111};
            //int[] array2 = null;
            int key = 44;
            
            int result = (new SearchMethods()).InterpolationSearch(array,key);
            
            Assert.AreEqual(result, 8);
                                               
        }

        //        size     min      max    key
        [TestCase(100000, 0, 5000000, 35)]
        [TestCase(10000000, 0, 5000000, 12892)]
        public void FindWithBinarySearch(int N, int min, int max, int key)
        {
            //An object that has randomly generated sorted array of N in [min, max] range
            int[] array = GeneratedArray.Generate(N,min,max);

            Stopwatch st1 = Stopwatch.StartNew();
            SearchMethods bs = new SearchMethods();
            int res1 = bs.BinarySearch(array, key);
            int count1 = bs.Iterations;
            st1.Stop();
        
            Stopwatch st2 = Stopwatch.StartNew();
            SearchMethods ls = new SearchMethods();
            int res2 = ls.LinearSearch(array, key);
            int count2 = ls.Iterations;
            st2.Stop();

            Stopwatch st3 = Stopwatch.StartNew();
            SearchMethods ipS = new SearchMethods();
            int res3 = ipS.InterpolationSearch(array, key);
            int count3 = ipS.Iterations;
            st3.Stop();

            //Element [key] was found at position [result] in [time] ms - output to file
            using (System.IO.StreamWriter file = 
            new System.IO.StreamWriter(@"../../../../out/result.txt",true))
            {
                file.WriteLine(String.Format("Params: Size={0}, Min={1}, Max={2}", N,min,max));
                file.WriteLine(String.Format("Binary: Element {0} was found at position {1} in time {2} in {3} iterations", key,res1, st1.GetTimeString(), count1));
                file.WriteLine(String.Format("Linear: Element {0} was found at position {1} in time {2} in {3} iterations", key,res2, st2.GetTimeString(), count2));
                file.WriteLine(String.Format("Interp: Element {0} was found at position {1} in time {2} in {3} iterations\n", key,res3, st3.GetTimeString(), count3));
            }               
        }
    }
}