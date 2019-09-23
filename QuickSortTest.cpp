// QuickSortTest.cpp: определяет точку входа для консольного приложения.
//#include "stdafx.h"

#include <iostream>
#include <ctime>
#include <chrono>
#include "class.h"

using namespace std;


int main(int argc, char* argv[])
{
	using namespace std::chrono;
	try
	{
		int N = stoi(argv[1]); //but 1st argument must be an int or it'll crash
		int MAXRANGE = stoi(argv[2]);
		int a1[N];
		srand(time(NULL));
		for (int i = 0; i < N; i++){
			a1[i] = rand() % MAXRANGE + 1;
		} 
		SortingClass qs_last = SortingClass(a1,N);
		SortingClass hs = SortingClass(a1,N);
		SortingClass qs_rand = SortingClass(a1,N);
		//qs_rand.toString("Unsorted:", N);

		duration<double,ratio<1,1000>> 
		time_span_q_last = qs_last.QS(0, N-1);

		duration<double,ratio<1,1000>> 
		time_span_h = hs.HS(0, N-1);
		
		duration<double,ratio<1,1000>> 
		time_span_q_rand = qs_rand.QS_rand(0, N-1);

		cout << "hs <------> " << time_span_h.count() << " ms\n";
		//qs_last.toString("Sorted:", N);
		cout << "qs_last <-> " << time_span_q_last.count() << " ms\n";
		//qs_rand.toString("Sorted:", N);
		cout << "qs_rand <-> " << time_span_q_rand.count() << " ms\n";


		return 0;
	}
	catch (const invalid_argument e)
	{
		cerr << "You have input non-number value\n";
	}
}

