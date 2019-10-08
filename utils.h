
class Utils
{
public:
    void InsertionSort(int*, int, int);
    void HSort(int*, int, int, int);

    int partition(int*, int, int);
    void QSort(int*, int, int);
    void QSort_rand(int*, int, int);
    
    void Merge(int*, int firstIndex, int midIndex, int lastIndex);
    void MSort(int*, int, int);
    void Hybrid_MSort(int*, int, int, int min_size);
    
};