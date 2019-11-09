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

        //        size    min   max   key
        [TestCase(100000, -100000, 100000, 700)]
        public void FindWithBinarySearch(int N, int min, int max, int key)
        {
            //An object that has randomly generated sorted array of N in [min, max] range
            int[] array = GeneratedArray.Generate(N,min,max);

            Stopwatch st1 = Stopwatch.StartNew();
            int res1 = (new SearchMethods()).BinarySearch(array, key);
            st1.Stop();
        
            Stopwatch st2 = Stopwatch.StartNew();
            int res2 = (new SearchMethods()).LinearSearch(array, key);
            st2.Stop();

            Stopwatch st3 = Stopwatch.StartNew();
            int res3 = (new SearchMethods()).InterpolationSearch(array, key);
            st3.Stop();

            //Element [key] was found at position [result] in [time] ms - output to file
            using (System.IO.StreamWriter file = 
            new System.IO.StreamWriter(@"../../../../out/result.txt"))
            {
                file.WriteLine(String.Format("Binary: Element {0} was found at position {1} in time {2}", key,res1, st1.GetTimeString()));
                file.WriteLine(String.Format("Linear: Element {0} was found at position {1} in time {2}", key,res2, st2.GetTimeString()));
                file.WriteLine(String.Format("Interp: Element {0} was found at position {1} in time {2}", key,res3, st3.GetTimeString()));
            }               
        }
    }
}