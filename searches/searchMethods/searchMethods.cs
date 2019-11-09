using System;

namespace searchMethods
{
    public class SearchMethods
    {
    
    /*
    Returns [position of element] if it exists
    Returns [-1] - if key not found or pointer is NULL
    a  ---> array to be searched
    leftI --> left bound of subarray
    rightI -> right bound of subarray
    key  ---> searching element
    */
    public int InterpolationSearch(int[] a, int key)
    {
        if(a == null) return -1;
        else if (a.Length == 0) throw new ArgumentException("Empty array");
        return InterpolationSearch(a, 0, a.Length - 1, key);
    }

    private int InterpolationSearch(int[] a, int leftI, int rightI, int key)
    {
        if(a == null || leftI > rightI)
        {
            return -1;
        }

        //find mid element according to the proportion
        int midI = leftI + ( (key - a[leftI]) / (a[rightI] - a[leftI])  * (rightI - leftI) );
        
        //choose minimum index of equal elements
        if(key == a[midI])
        {
            if(a[midI-1] == key)
            {
                return InterpolationSearch(a, leftI, midI-1, key);
            }    
            else return midI;
        }
        else if(key > a[midI])
        {
            return InterpolationSearch(a, midI+1, rightI, key);
        }
        else return InterpolationSearch(a, leftI, midI-1, key);

    }

    /*
    Search KEY in array.
    Returns [index of element in array] - if it presents in array
    Returns [-1] - if key not found or pointer is NULL
    a    ---> array to be searched
    leftI --> left bound of subarray
    rightI -> right bound of subarray
    key  ---> searching element
    */
    public int BinarySearch(int[] a, int key)
    {
        if(a == null) return -1;
        else if (a.Length == 0) throw new ArgumentException("Empty array");
        return BinarySearch(a, 0, a.Length - 1, key);
    }

    private int BinarySearch(int[] arr, int leftI, int rightI, int key)
    {
        if(leftI > rightI)
        {
            return -1;
        }
        int midI = leftI + (rightI - leftI)/2;
        
        //choose minimum index of equal elements
        if(key == arr[midI])
        {
            if(arr[midI-1] == key)
            {
                return BinarySearch(arr, leftI, midI-1, key);
            }    
            else return midI;
        }
        else if(key > arr[midI])
        {
            return BinarySearch(arr, midI+1, rightI, key);
        }
        else return BinarySearch(arr, leftI, midI-1, key);
    }

    //Returns [index of first occurence] of element in array
    //Returns [-1] if element wasn't in array
    public int LinearSearch(int[] a, int key)
    {
        //do exception if a.length is wrong or a is null
        if(a == null) return -1;
        else if (a.Length == 0) throw new ArgumentException("Empty array");
        return LinearSearch(a,a.Length,key);
    }

    private int LinearSearch(int[] a, int size, int key)
    {
        for (int i = 0; i < size; i++)
        {
            if (a[i] == key) return i;
        }
        return -1;
    }
    
    }
}
