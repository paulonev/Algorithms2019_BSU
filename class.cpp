#include <iostream>
#include "class.h"
#include "utils.h"

using namespace std;

SortingClass::SortingClass()
{
}

SortingClass::SortingClass(int* array, int N)
{
    a = new int[N];
    for (int i = 0; i < N; i++)
        a[i] = array[i];
}

SortingClass::~SortingClass()
{
    delete[] a;
}

/*
Starts hybrid_sort method of Utils class and measure time of execution
l     <-----> lower bound of array
r     <-----> upper bound of array
min_size <--> minimum size of array from which to start insertion_sort of subarrays
*/
duration<double,ratio<1,1000>> SortingClass::HS(int l, int r, int min_size)
{
    Utils ut = Utils();
    steady_clock::time_point time11 = steady_clock::now();
    ut.HSort(a,l,r, min_size);
	steady_clock::time_point time21 = steady_clock::now();
	return duration_cast<duration<double,ratio<1,1000>>>(time21 - time11);

}

/*
Starts merge sort algorithm and measure time of execution
left     <-----> lower bound of array
right    <-----> upper bound of array
*/
duration<double,ratio<1,1000>> SortingClass::MS(int left, int right)
{
    Utils ut = Utils();
    steady_clock::time_point time1 = steady_clock::now();
    ut.MSort(a,left,right);
    steady_clock::time_point time2 = steady_clock::now();
    return duration_cast<duration<double,ratio<1,1000>>>(time2-time1);
}

/*
Starts hybrid merge sort algorithm and measure time of execution
left     <-----> lower bound of array
right    <-----> upper bound of array
*/
duration<double,ratio<1,1000>> SortingClass::H_MS(int left, int right,int min_size)
{
    Utils ut = Utils();
    steady_clock::time_point time1 = steady_clock::now();
    ut.Hybrid_MSort(a,left,right, min_size);
    steady_clock::time_point time2 = steady_clock::now();
    return duration_cast<duration<double,ratio<1,1000>>>(time2-time1);
}

/*
Starts quick sort algorithm with partition for last elem
of each subarray it works with. Measure time of execution
l     <-----> lower bound of array
r     <-----> upper bound of array
*/
duration<double,ratio<1,1000>> SortingClass::QS(int l, int r)
{
    Utils ut = Utils();
    steady_clock::time_point time1 = steady_clock::now();
    ut.QSort(a,l,r);
    steady_clock::time_point time2 = steady_clock::now();
    return duration_cast<duration<double,ratio<1,1000>>>(time2 - time1);

}

duration<double,ratio<1,1000>> SortingClass::QS_rand(int l, int r)
{
    Utils ut = Utils();
    steady_clock::time_point time1 = steady_clock::now();
    ut.QSort_rand(a,l,r);
    steady_clock::time_point time2 = steady_clock::now();
    return duration_cast<duration<double,ratio<1,1000>>>(time2 - time1);

}

void SortingClass::toString(string mes, int N)
{
    cout << mes << " ";
    for (int i = 0; i < N; i++)
        cout << a[i] << " ";
    cout << endl;    
}