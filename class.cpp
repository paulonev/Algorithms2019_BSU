#include <iostream>
#include "class.h"
#include "utils.h"

using namespace std;

SortingClass::SortingClass(int* array, int N)
{
    a = new int[N];
    for (int i = 0; i < N; i++)
        a[i] = array[i];
}

SortingClass::~SortingClass()
{
    //TODO: удаление ссылки на массив
    delete[] a;
}

duration<double,ratio<1,1000>> SortingClass::HS(int l, int r)
{
    Utils ut = Utils();
    steady_clock::time_point time11 = steady_clock::now();
    ut.HSort(a,l,r);
	steady_clock::time_point time21 = steady_clock::now();
	return duration_cast<duration<double,ratio<1,1000>>>(time21 - time11);

}

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