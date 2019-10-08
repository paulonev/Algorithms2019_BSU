#include "utils.h"
#include <ctime>
#include <cstdlib>
//#include <cstdio>

/*HSort or Hybrid Sort method is a mix
* of Qsort and Insertion Sort algorithms
arr	     ----> pointer to array to be sorted
l  	     ----> starting index
r 	     ----> ending index
min_size ----> if size(subarray) <= min_size then start insertion_sort
*/
void Utils::HSort(int* arr, int l, int r, int min_size)
{
	if (l<r)
	{
		int pi;
		if(r-l+1 <= min_size)
			InsertionSort(arr,l,r);
		else{
			pi = partition(arr,l,r);
			HSort(arr,l,pi-1, min_size);
			HSort(arr,pi+1,r, min_size);
		}
	}
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
		int pi = partition(arr, l, r);
		QSort(arr, l, pi - 1);
		QSort(arr, pi + 1, r);
	}
}

/*
This method returns index of correct position of pivot
taken as last elem of subarray
And places all elems smaller than pivot on the left-hand side
and all greater on the right-hand side from pivot
arr[] ----> array to sort
left  ----> starting index, right - ending index
*/
int Utils::partition(int* arr, int left, int right)
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

/*
This is merge sort execution function
*/
void Utils::MSort(int* a, int left, int right)
{
	if(left<right)
	{
		int midIdx = (left+right) / 2;
		MSort(a,left,midIdx);
		MSort(a,midIdx + 1,right);
		Merge(a,left,midIdx,right);
	}
}

/*
firstIndex ->	left edge of join
midIndex ---> 	what is the center of two subarrays to join
lastIndex --> 	right edge of join
Indexes of 1st subarray : [firstIndex, midIndex]
Indexes of 2nd subarray : [midIndex+1,lastIndex]
*/
void Utils::Merge(int* arr, int firstIndex, int midIndex, int lastIndex)
{
	//amount of elems of joined array & reserve array for those
	int totalElems = lastIndex - firstIndex + 1;
	int* temp_arr = new int[totalElems];

	//create iterators for 2 subarray and temparr
	int leftIter = firstIndex, rightIter = midIndex + 1, tempIter = 0;
	
	//put elems sorted in temparr
	while(leftIter <= midIndex && rightIter <= lastIndex)
	{
		if(arr[leftIter] <= arr[rightIter])
		{
			temp_arr[tempIter++] = arr[leftIter++];
		}
		else 
		{
			temp_arr[tempIter++] = arr[rightIter++];
		}
	}

	//if there're any elems in left subarray that were not added
	while(leftIter <= midIndex)
		temp_arr[tempIter++] = arr[leftIter++];
	
	//if there're any elems in right subarray that were not added
	while(rightIter <= lastIndex)
		temp_arr[tempIter++] = arr[rightIter++];
	
	//Merged array is sorted
	//Imply changes to initial array
	for (int i = 0; i< totalElems; i++)
	{
		arr[firstIndex + i] = temp_arr[i];
	}
	
	delete[] temp_arr;
}

/*Hybrib_MSort is a mix
* of Merge Sort and Insertion Sort algorithms
arr	     ----> pointer to array to be sorted
l  	     ----> starting index
r 	     ----> ending index
min_size ----> if size(subarray) <= min_size then start insertion_sort
*/
void Utils::Hybrid_MSort(int* a, int left, int right, int min_size)
{
	if(left<right)
	{
		if(right-left+1 <= min_size)
			InsertionSort(a,left,right);
		else 
		{
			int mid = (left+right)/2;
			Hybrid_MSort(a,left,mid,min_size);
			Hybrid_MSort(a,mid+1,right,min_size);
			Merge(a,left,mid,right);
		}
	}
}