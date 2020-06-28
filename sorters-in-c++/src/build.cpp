// QuickSortTest.cpp: определяет точку входа для консольного приложения.
//#include "stdafx.h"

#include <iostream>
#include <ctime>
#include <chrono>
#include "/home/paul/coding/algorithms-data-structures/sorters-in-c++/h/class.h"
#include "class.cpp"
// K N MAXRANGE
using namespace std;


int main(int argc, char* argv[])
{
	using namespace std::chrono;
	try
	{
		steady_clock::time_point t1 = steady_clock::now();
		
		for (int K = 6; K <= 50; K+=2)
		{
			//k = {6,8,10,12,14}
			//MAXRANGE = {1000, 100000, 10000000}
			//N = {1000,10000,100000,1000000}
			for(int N = 1000; N <= 1000000; N*=10)
			{
				int a1[N];
				srand(time(NULL));
				for(long MAXRANGE = 1000; MAXRANGE <= 10000000; MAXRANGE *= 100){
					for (int i = 0; i < N; i++){
						a1[i] = rand() % MAXRANGE + 1;	
					}

					SortingClass qs_last = SortingClass(a1,N);
					SortingClass hs = SortingClass(a1,N);
					SortingClass qs_rand = SortingClass(a1,N);
					SortingClass ms = SortingClass(a1,N); // merge_sort variable
					SortingClass h_ms = SortingClass(a1,N); // hybrid merge_sort variable

					duration<double,ratio<1,1000>> 
					time_span_q_last = qs_last.QS(0, N-1); 

					duration<double,ratio<1,1000>> 
					time_span_h = hs.HS(0, N-1, K);
					
					duration<double,ratio<1,1000>> 
					time_span_q_rand = qs_rand.QS_rand(0, N-1);

					duration<double,ratio<1,1000>>
					time_span_m = ms.MS(0,N-1);

					duration<double,ratio<1,1000>>
					time_span_h_ms = h_ms.H_MS(0,N-1,K);

					// checked that merge is faster on sorted arrays than quicksort with last
					//time_span_q_last = qs_last.QS(0, N-1);
					//time_span_m = ms.MS(0,N-1);

					//cout << "HS: "; hs.toString("Sorted:", N);
					cout <<"MIN_SIZE = " << K << " SIZE = " << N << " MAXRANGE = [0," << MAXRANGE << "]" << endl;
					cout << "hs <------> " << time_span_h.count() << " ms\n";
					//cout << "QS_L: " ; qs_last.toString("Sorted:", N);
					cout << "qs_last <-> " << time_span_q_last.count() << " ms\n";
					//cout << "QS_RAND: "; qs_rand.toString("Sorted:", N);
					cout << "qs_rand <-> " << time_span_q_rand.count() << " ms\n";
					cout << "h_ms <----> " << time_span_h_ms.count() << " ms\n";
					cout << "ms <------> " << time_span_m.count() << " ms\n";
				
				}	
						
			}

		}
		
		steady_clock::time_point t2 = steady_clock::now();
		duration<double,ratio<1,1>> program_terminate = duration_cast<duration<double>>(t2-t1);
		cout << "Program terminated in " << program_terminate.count() << " seconds ...\n";
		
		return 0;
	}
	catch (const invalid_argument e)
	{
		cerr << "You have input non-number value\n";
	}
}

/*int a[] = {15,3,12,10,7,5,2,1};
		int arrSize = sizeof(a)/sizeof(*a);
		cout << "Unsorted: ";
		for (int i = 0; i < arrSize; i++)
		{
			cout << a[i] << " ";
		}
		cout << "\n";
		SortingClass ms = SortingClass(a,arrSize);	
		SortingClass qs_last = SortingClass(a,arrSize);

		duration<double,ratio<1,1000>>
		time_span_m = ms.MS(0,arrSize-1);
		
		duration<double,ratio<1,1000>> 
		time_span_q_last = qs_last.QS(0, arrSize-1);
		
		cout << "Merge Sort:\n";
		ms.toString("Sorted: ", arrSize);
		cout << "Time: " << time_span_m.count() << endl;

		cout << "Quick Sort:\n";
		qs_last.toString("Sorted: ", arrSize);
		cout << "Time: " << time_span_q_last.count() << endl;
*/