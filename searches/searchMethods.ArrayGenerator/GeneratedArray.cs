using System;

namespace searchMethods.ArrayGenerator
{
    public class GeneratedArray
    {
        public static int[] Generate(int N, int min, int max)
        {
            //1. Use template method
            Random rand = new Random();
            int[] array = new int[N];
            for (int i = 0; i < N; i++)  {
                array[i] = rand.Next(min, max+1);
            }

            //2. Array won't always be in increasing order, so we should sort it using QuickSort(O(nlogn))
            Array.Sort(array);
            return array;
        }
    }
}
