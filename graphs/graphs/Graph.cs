using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Graphs
{
    //implement following methods
    //a)addEdge                    DONE
    //b)AreAdjacent                
    //c)N(v) v as vertex of graph  

    //implement adjacency-list approach of storing graphs
    //in which graph is an ARRAY, which index represents a vertex
    //ARRAY[i] stores a list of adjacent to i vertices
    public class Graph
    {
        public int Size { get; set; } //size of ARRAY
        public List<WeightedEdge> []AdjacencyList { get; set; } // array of edges outOfVertex

        public Graph() {}
        public Graph(int size)
        {
            Size = size;
            AdjacencyList = new List<WeightedEdge>[Size];
            for (int i = 0; i < Size; i++)
            {
                AdjacencyList[i] = new List<WeightedEdge>();
            } 
        }

        /// <summary>
        /// Add adjacent vertex [dest] to the adjacency list of [src] and vice versa
        /// This pair (src, dest) is an edge of graph G
        /// </summary>
        /// <param name="src">source vertex</param>
        /// <param name="dest">destination vertex</param>
        /// <param name="w">weight of edge (1 by default)</param>
        public virtual void AddEdge(int src, int dest, int w = 1)
        {
            src--; dest--;
            if (!AdjacencyList[src].Exists(e => e.Dest == dest))
            {
                AdjacencyList[src].Add(new WeightedEdge(src, dest, w));   
                AdjacencyList[dest].Add(new WeightedEdge(dest, src, w));
            }
        }

        //could pass both edges and vertices
        public bool AreAdjacent(Object ob1, Object ob2)
        {
            if (ob1.GetType() == ob2.GetType())
            {
                if (ob1.GetType().IsValueType)
                {
                    //then they are both vertices
                    return AdjacencyList[(int) ob1].Exists(e => e.Dest == (int) ob2);
                }

                if (ob1.GetType() == typeof(WeightedEdge))
                {
                    //then they are both of WeightedEdge type
                    WeightedEdge e1 = (WeightedEdge) ob1;
                    WeightedEdge e2 = (WeightedEdge) ob2;

                    return (e1.IsIncident(e2.Src) || e1.IsIncident(e2.Dest));
                }
                return false;

            }
            return false;
        }

        public virtual void PrintGraph(){
            for (int i = 0; i <Size ; i++) {
                List<WeightedEdge> list = AdjacencyList[i];
                Console.WriteLine();
                Console.Write((i+1) + ":");
                for (int j = 0; j <list.Count; j++)
                {
                    Console.Write($"[({list[j].Src+1},{list[j].Dest+1}):{list[j].Weight}]");
                }
                Console.Write(" ");
            }
        }
    }

    public class OrientedGraph : Graph
    {
        public OrientedGraph(int size) : base(size) {}

        public override void AddEdge(int src, int dest, int w = 1)
        {
            src--; dest--;
            //if among edges outOf src is edge(dest, w) {w could be different}
            var edge = AdjacencyList[src].Find(e => dest == e.Dest && w != e.Weight);
            if (AdjacencyList[src].Count != 0 && edge != null)
            {
                //then change weight of that edge to new weight
                edge.Weight = w;
            }
            else AdjacencyList[src].Add(new WeightedEdge(src, dest, w));
        }

        public override void PrintGraph()
        {
            for (int i = 0; i <Size ; i++) {
                List<WeightedEdge> list = AdjacencyList[i];
                Console.WriteLine();
                Console.Write((i+1) + ":");
                for (int j = 0; j <list.Count; j++)
                {
                    Console.Write($"[({list[j].Src+1},{list[j].Dest+1}):{list[j].Weight}]");
                }
                Console.Write(" ");
            }
        }
    }
}