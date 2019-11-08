using NUnit.Framework;

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
            int[] array2 = null;
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
            int[] array2 = null;
            SearchMethods sm = new SearchMethods();
            
            int searchedElement = 44;
            
            int result = sm.InterpolationSearch(array,searchedElement);
            
            Assert.AreEqual(result, 8);
                                               
        }

        [Test]
        public void FindWithBinarySearch()
        {
            int[] array = new int[]{-10,1,4,10,15,22,34,35,44,111};
            int[] array2 = null;
            SearchMethods sm = new SearchMethods();
            
            int searchedElement = 111;

            int result = sm.BinarySearch(array,searchedElement);

            Assert.AreEqual(result, 9);
                                               
        }
    }
}