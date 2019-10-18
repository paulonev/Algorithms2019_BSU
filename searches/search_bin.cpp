#include <iostream>
#include <cstring>
#include <ctime>
#include "/home/paul_okunev/Documents/Algorithms2019_BSU/utils.cpp"
#include <chrono>
using namespace std;

//add counter of entrances to recursion
//check on big arrays of ascending order
//count time complexity

int linearSearch(int* arr, int arrSize, int key);
int binarySearch(int* arr, int left, int right, int key);
int interpolationSearch(int* arr, int left, int right, int key);

const int UPPER = 100000;  //upper bound of range
const int LOWER = -100000; //lower bound of range
const long N = 1000000; //size of array

int main(int argc, char** argv)
{
    using namespace std::chrono;

    Utils ut = Utils();
    srand(time(NULL));
    int a[N];
    for (int i = 0; i < N; i++){
	    a[i] = (rand() % (UPPER - LOWER + 1)) + LOWER; 	
	}
    
    ut.QSort(a,0,N-1);  
    int elemToFind;
    
    cout << "Type value to know it's position in array\n";
    cin >> elemToFind;
    
    steady_clock::time_point t1 = steady_clock::now();
    printf("IS - Position of {%i} in array: %i\n", elemToFind, interpolationSearch(a, 0, N-1, elemToFind));
	steady_clock::time_point t2 = steady_clock::now();
    duration<double,ratio<1,1000>> time_span_1 = duration_cast<duration<double,ratio<1,1000>>>(t2-t1);
    cout << "Time - " << time_span_1.count() << endl;
    
    t1 = steady_clock::now();
    printf("BS - Position of {%i} in array: %i\n", elemToFind, binarySearch(a, 0, N-1, elemToFind));
    t2 = steady_clock::now();
    time_span_1 = duration_cast<duration<double,ratio<1,1000>>>(t2-t1);
    cout << "Time - " << time_span_1.count() << endl;
    
    t1 = steady_clock::now();
    printf("LS - Position of {%i} in array: %i\n", elemToFind, linearSearch(a, N, elemToFind));
    t2 = steady_clock::now();
    time_span_1 = duration_cast<duration<double,ratio<1,1000>>>(t2-t1);
    cout << "Time - " << time_span_1.count() << endl;
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

//Returns [index of first occurence] of element in array
//Returns [-1] if element wasn't in array
int linearSearch(int* a, int arrSize, int key)
{
    for (int i = 0; i < arrSize; i++)
    {
        if (a[i] == key) return i;
    }
    return -1;
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