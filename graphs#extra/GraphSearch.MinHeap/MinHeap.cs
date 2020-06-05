using System;
using System.Collections.Generic;
using System.Linq;
using Graphs;

namespace GraphSearch.MinHeap
{
    
//    public struct HeapNode
//    {
//        public int Vtx { get; set; }
////        public int Key { get; set; }
//
//        public HeapNode(int v, int key)
//        {
//            Vtx = v;
////            Key = key;
//        }
//
//        /// <summary>
//        /// For finding index of element in Heap_array
//        /// </summary>
//        /// <param name="obj"></param>
//        /// <returns></returns>
//        public override bool Equals(object obj)
//        {
//            if (!(obj is HeapNode node))
//            {
//                return false;
//            }
//
//            return this.Vtx == node.Vtx && this.Key == node.Key;
//        }
//
//        public override int GetHashCode()
//        {
//            return base.GetHashCode();
//        }
//    }
    
    public class MinHeap
    {
        /// <summary>
        /// directed acyclic non-negative edges
        /// </summary>
//        private DirectedGraph graph;

        /// <summary>
        /// An instance for mapping vertex to its dijkstra score
        /// </summary>
        public Dictionary<int, int> ScoreMap { get; set; }

        /// <summary>
        /// Capacity of heap
        /// </summary>
        public int Capacity { get; set; }
        
        /// <summary>
        /// Array view of heap
        /// </summary>
        public int[] HeapArray { get; set; } 
        
        /// <summary>
        /// Current size of heap
        /// </summary>
        public int HeapSize { get; set; }

        /// <summary>
        /// Gets the index of parent of i
        /// </summary>
        /// <param name="i">specified element</param>
        /// <returns></returns>
        public int Parent(int i) => (i-1) / 2;
        
        
        /// <summary>
        /// Gets the index of left child of i
        /// </summary>
        /// <param name="i">specified element</param>
        /// <returns></returns>
        public int Left(int i)   =>  2*i + 1;
        
        
        /// <summary>
        /// Gets the index of right child of i
        /// </summary>
        /// <param name="i">specified element</param>
        /// <returns></returns>
        public int Right(int i)  =>  2*i + 2;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i">vertex</param>
        /// <returns>dijkstra greedy score of a vertex</returns>
        private int DijkstraScore(int i) => ScoreMap[i];
        
        public MinHeap(Dictionary<int, int> mapper)
        {
            ScoreMap = mapper;
            Capacity = mapper.Keys.Count;
            HeapArray = new int[Capacity];
            HeapSize = 0;
        }
        
//        public MinHeap(DirectedGraph graph)
//        {
//            this.graph = graph;
//            Capacity = this.graph.Size;
//            HeapArray = new int[Capacity];
//            HeapSize = 0;
//        }

        
        /// <summary>
        /// Insert Node [vertex, score]  in heap_array
        /// Time complexity [log n] where n- size of graph
        /// </summary>
        /// <param name="vtx">vertex</param>
        public void InsertKey(int vtx)
        {
            HeapSize++;
            int i = HeapSize - 1;
            //add Node to the end of heap array
            HeapArray[i] = vtx;
//            Heap_array[i] = new HeapNode(vtx,graph.Marks[vtx]);

            //insert it in right(according to min-heap) position
            //so that any it's child node has bigger Mark 
            //Bubble-Upping node
            while (i != 0 && 
                   DijkstraScore(HeapArray[Parent(i)]) > DijkstraScore(HeapArray[i]))
            {
                Swap(i,  Parent(i));
                i = Parent(i);
            }
        }

        /// <summary>
        /// Returns index of vtx with minimal Mark
        /// </summary>
        /// <returns></returns>
        public int ExtractMin()
        {
            if (HeapSize == 1) 
            { 
                HeapSize--; 
                return HeapArray[0]; 
            } 
  
            // Store the minimum value, and remove it from heap 
            int root = HeapArray[0]; 
            HeapArray[0] = HeapArray[HeapSize-1]; 
            HeapSize--; 
            
            MinHeapify(0);  //put root Node in right position of min-heap
  
            return root; 
        }

        /// <summary>
        /// Changes Mark for i-th Node to the new_value
        /// </summary>
        /// <param name="vtx">Node</param>
        /// <param name="newValue">smaller Mark than before</param>
        public void DecreaseKey(int vtx, int newValue)
        {
            int i = Array.IndexOf(HeapArray, vtx);
            ScoreMap[HeapArray[i]] = newValue;
            
            while (i != 0 && DijkstraScore(HeapArray[Parent(i)]) > DijkstraScore(HeapArray[i])) 
            { 
                Swap(i, Parent(i)); 
                i = Parent(i); 
            } 
        }
        
        /// <summary>
        /// Maintain heap property
        /// </summary>
        /// <param name="i">index of Node that's in wrong position</param>
        private void MinHeapify(int i)
        {
            //take children of i-th Node
            int lIndex = Left(i);
            int rIndex = Right(i);
            int smallest = i;

            if (lIndex < HeapSize && DijkstraScore(HeapArray[lIndex]) < DijkstraScore(HeapArray[i]) )
                smallest = lIndex;
            if (rIndex < HeapSize && DijkstraScore(HeapArray[rIndex]) < DijkstraScore(HeapArray[smallest]) ) 
                smallest = rIndex; 
            if (smallest != i) 
            { 
                Swap(i, smallest); 
                MinHeapify(smallest); 
            }

        }
        
        /// <summary>
        /// Swaps two Nodes of array
        /// </summary>
        /// <param name="x">index of 1st Node</param>
        /// <param name="y">index of 2nd Node</param>
        private void Swap(int x, int y)
        {
            int temp = HeapArray[x];
            HeapArray[x] = HeapArray[y];
            HeapArray[y] = temp;
        }


    }
}
