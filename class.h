#include <iostream>
#include <chrono>

using namespace std;
using namespace std::chrono;

class SortingClass
{
    int* a;
public:
    SortingClass(int* , int );
    SortingClass();
    ~SortingClass();
    duration<double,ratio<1,1000>> QS(int, int);
    duration<double,ratio<1,1000>> HS(int, int, int);
    duration<double,ratio<1,1000>> QS_rand(int, int);
    void toString(string, int);
};