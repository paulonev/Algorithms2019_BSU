#include "utils.h"
#include <ctime>
#include <cstdlib>
//#include <cstdio>
#define MIN_SIZE 50

/*HSort or Hybrid Sort method is a mix
* of Qsort and Insertion Sort algorithms
arr	----> pointer to array to be sorted
l  	----> starting index
r 	----> ending index

void Utils::HSort(int* arr, int l, int r)
{
	if(r-l+1 <= MIN_SIZE)
	{
		InsertionSort(arr,r-l+1);
	}
	else QSort(arr,l,r);
}*/

void Utils::HSort(int* arr, int l, int r)
{
	if (l<r)
	{
		int pi = findPosition(arr,l,r);
		
		if(r-l+1 <= MIN_SIZE)
			InsertionSort(arr,r,l);
		else{
			HSort(arr,l,pi-1);
			HSort(arr,pi+1,r);
		}
	}
}

/*
In this Qsort pivot is the last element
arr	----> pointer to array to be sorted
pi	----> partition index
l   ----> starting index
r   ----> ending index
*/
void Utils::QSort(int* arr, int l, int r)
{
	if (l<r)
	{
		//as the result arr[pi] will stand in the right place
		int pi = findPosition(arr, l, r);
		QSort(arr, l, pi - 1);
		QSort(arr, pi + 1, r);
	}
}

void Utils::QSort_rand(int* arr, int left, int right)
{
	//srand(time(NULL));
	int pivot = arr[left + rand() % (right - left)];
	int i = left; int j = right;

	while (i <= j)
	{
		while (arr[i] < pivot)  i++;
		while (arr[j] > pivot)	j--;

		if (i <= j) {
			int temp = arr[i];
			arr[i] = arr[j];
			arr[j] = temp;
			i++;
			j--;
		}
	}
	if (left < j)
		QSort_rand(arr, left, j);
	if (right > i)
		QSort_rand(arr, i, right);
	
}

/*
This method returns index of correct position of pivot
And places all elems smaller than pivot on the left side
and all greater on the right side of pivot
arr[] ----> array to sort
left  ----> starting index, right - ending index

*/
int Utils::findPosition(int* arr, int left, int right)
{
	int pivot = *(arr+right);
	int i = left; int j = right;
	
	do
	{
		while (arr[i] < pivot)  i++; 
		while (arr[j] >= pivot) j--;

		if (i <= j) {
			int temp = arr[i];
			arr[i] = arr[j];
			arr[j] = temp;
		}
	} while (i <= j);

	int temp1 = arr[i];
	arr[i] = arr[right];
	arr[right] = temp1;

	return i;
	
}

void Utils::InsertionSort(int* arr, int l, int r){
//insertion sort; N = r-l+1 - size of array
	int t,j; // 2
	//		1		1	 1  => N times => 3*N
	for(int i=1+l; i<=r; i++) 
	{
		t = arr[i]; //2
		//   1	 1		1			  1
		for(j=i; j>0 && arr[j-1] > t; j--) //N-1 in worst case => 4(N-1)
			arr[j] = arr[j-1]; //3
		arr[j] = t; //2
		//inside first FOR loop => 7 + 4(N-1)
	}
	// 3*N * (7+4(N-1)) = 21*N + 12 N(N-1)= 12N^2 + 9 N = O(N^2)
}