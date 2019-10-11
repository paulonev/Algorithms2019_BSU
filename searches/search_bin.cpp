#include <iostream>
#include <cstring>
using namespace std;

int linearSearch(int* arr, int arrSize, int key);
int binarySearch(int* arr, int left, int right, int key);
int interpolationSearch(int* arr, int left, int right, int key);

int main(int argc, char** argv)
{
    //int a[] = {4,5,5,7,8,12,18,30,44,60,60,62,65};
    int a[] = {2,4,7,10,20,20,23,27,30,35};
    int arrSize = sizeof(a)/sizeof(*a);
    int elemToFind;
    
    cout << "Choose element from here to know it's position\n";
    for (int i = 0; i < arrSize; i++)
    {
        cout << a[i] << " ";
    }
    cout << "\n";
    cin >> elemToFind;
    

    printf("Position of {%i} in array: %i\n", elemToFind, interpolationSearch(a, 0, arrSize-1, elemToFind));
    return 0;
}

/*
Search array for a KEY.
Returns [index of element in array] - if it presents in array
Returns [-1] - if key not found or pointer is NULL
arr  ---> array to be searched
leftI --> left bound of subarray
rightI -> right bound of subarray
key  ---> searching element
*/
int binarySearch(int* arr, int leftI, int rightI, int key)
{
    if(arr == NULL || leftI > rightI)
    {
        return -1;
    }
    int midI = leftI + (rightI - leftI)/2;
    
    //choose minimum index of equal elements
    if(key == arr[midI])
    {
        if(arr[midI-1] == key)
        {
            binarySearch(arr, leftI, midI-1, key);
        }    
        else return midI;
    }
    else if(key > arr[midI])
    {
        binarySearch(arr, midI+1, rightI, key);
    }
    else binarySearch(arr, leftI, midI-1, key);   
}

/*
Returns [position of element] if it exists
Returns [-1] - if key not found or pointer is NULL
arr  ---> array to be searched
leftI --> left bound of subarray
rightI -> right bound of subarray
key  ---> searching element
*/
int interpolationSearch(int* a, int leftI, int rightI, int key)
{
    if(a == NULL || leftI > rightI)
    {
        return -1;
    }

    int param = (key - a[leftI]) / (a[rightI] - a[leftI]);
    int midI = leftI + param * (rightI - leftI);
    
    //choose minimum index of equal elements
    if(key == a[midI])
    {
        if(a[midI-1] == key)
        {
            interpolationSearch(a, leftI, midI-1, key);
        }    
        else return midI;
    }
    else if(key > a[midI])
    {
        interpolationSearch(a, midI+1, rightI, key);
    }
    else interpolationSearch(a, leftI, midI-1, key);

}
/*int midI;
    int param;
    if(a == NULL)
    {
        return -1;
    }

    while(leftI <= rightI)
    {
        param = (key - a[leftI]) / (a[rightI] - a[leftI]);
        midI = leftI + param * (rightI - leftI);

        if(a[midI] == key)
        {
            if(a[midI-1] == key)
            {
                rightI = midI - 1;
            }
        }
        else if (a[midI] < key)
        {
            leftI = midI+1;
        }        
        else rightI = midI - 1;

    }

    return midI;*/