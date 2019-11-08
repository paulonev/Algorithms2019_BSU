using NUnit.Framework;
using searchMethods.ArrayGenerator;

namespace searchMethods.Tests
{
    [TestFixture]
    public class searchingTests
    {
        [Test]        
        public void FindWithLinearSearch()
        {
            //1. generate array of N size
            
            int[] array = new int[]{-10,1,4,10,15,22,34,35,44,111};
            //int[] array2 = null;
            SearchMethods sm = new SearchMethods();
            int elemToFind = 22;

            int result = sm.LinearSearch(array,elemToFind);

            int expected = 5;
            
            Assert.AreEqual(result, expected);
                                               
        }

        [Test]
        public void FindWithInterpolationSearch()
        {
            int[] array = new int[]{-10,1,4,10,15,22,34,35,44,111};
            //int[] array2 = null;
            SearchMethods sm = new SearchMethods();
            
            int searchedElement = 44;
            
            int result = sm.InterpolationSearch(array,searchedElement);
            
            Assert.AreEqual(result, 8);
                                               
        }

        //        size    min   max   key
        [TestCase(10000, -1000, 1000, 700)]
        public void FindWithBinarySearch(int N, int min, int max, int key)
        {
            //An object that has randomly generated sorted array of N in [min, max] range
            int[] array = GeneratedArray.Generate(N,min,max);

            int result = (new SearchMethods()).BinarySearch(array, key);
            
            //TODO: create another way of testing
            //Maybe upload data to some file
            Assert.AreEqual(result, 9);               
        }
    }
}