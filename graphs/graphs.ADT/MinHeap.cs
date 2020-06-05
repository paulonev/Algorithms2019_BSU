using System;
using System.Linq;
using Graphs;

namespace graphs.ADT
{
    public struct HeapNode
    {
        public int Vtx { get; set; }
        public int Key { get; set; }

        public HeapNode(int v, int key)
        {
            Vtx = v;
            Key = key;
        }

        /// <summary>
        /// For finding index of element in Heap_array
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is HeapNode node))
            {
                return false;
            }

            return this.Vtx == node.Vtx && this.Key == node.Key;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    
    public class MinHeap
    {
        /// <param name="graph">directed acyclic non-negative edges</param>
        private Graph graph;

        /// <summary>
        /// Capacity of heap
        /// </summary>
        public int Capacity { get; set; }
        
        /// <summary>
        /// Array view of heap
        /// </summary>
        public HeapNode[] Heap_array { get; set; } 
        
        /// <summary>
        /// Current size of heap
        /// </summary>
        public int Heap_size { get; set; }

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
        
        
        public MinHeap(Graph graph)
        {
            this.graph = graph;
            Capacity = this.graph.Size;
            Heap_array = new HeapNode[Capacity];
            Heap_size = 0;
        }

        /// <summary>
        /// Insert Node [vertex, score]  in heap_array
        /// Time complexity [log n] where n- size of graph
        /// </summary>
        /// <param name="vtx">vertex</param>
        public void InsertKey(int vtx)
        {
            Heap_size++;
            int i = Heap_size - 1;
            //add Node to the end of heap array
            Heap_array[i] = new HeapNode(vtx,graph.Marks[vtx]);

            //insert it in right(according to min-heap) position
            //so that any it's child node has bigger Mark 
            //Bubble-Upping node
            while (i != 0 && Heap_array[Parent(i)].Key > Heap_array[i].Key)
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
            if (Heap_size == 1) 
            { 
                Heap_size--; 
                return Heap_array[0].Vtx; 
            } 
  
            // Store the minimum value, and remove it from heap 
            int root = Heap_array[0].Vtx; 
            Heap_array[0] = Heap_array[Heap_size-1]; 
            Heap_size--; 
            
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
            var heapNode = Heap_array.First(n => n.Vtx == vtx);
            int i = Array.IndexOf(Heap_array, heapNode);
            Heap_array[i].Key = newValue;

            while (i != 0 && Heap_array[Parent(i)].Key > Heap_array[i].Key) 
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
            int l = Left(i);
            int r = Right(i);
            int smallest = i;

            if (l < Heap_size && Heap_array[l].Key < Heap_array[i].Key)
                smallest = l;
            if (r < Heap_size && Heap_array[r].Key < Heap_array[smallest].Key) 
                smallest = r; 
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
            HeapNode temp = Heap_array[x];
            Heap_array[x] = Heap_array[y];
            Heap_array[y] = temp;
        }


    }
}
