using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace graphs
{
    public class WeightedEdge
    {
        public int Weight { get; set; }
        public int Dest { get; set; }
    
        public WeightedEdge(int dest, int weight)
        {
            Dest = dest;
            Weight = weight;
        }

    }

    //implement adjacency-list approach of storing graphs
    //in which graph is an ARRAY, which index represents a vertex
    //ARRAY[i] stores a list of adjacent to i vertices
    public class Graph
    {
        public int Size { get; set; } //size of ARRAY
        public List<WeightedEdge> []Edges { get; set; } // array of edges outOfVertex

        public Graph(int size)
        {
            Size = size;
            Edges = new List<WeightedEdge>[Size];
            for (int i = 0; i < Size; i++)
            {
                Edges[i] = new List<WeightedEdge>();
            } 
        }

        public void AddEdge(int src, int dest, int w)
        {
            src--; dest--;
            //if among edges outOf src is edge(dest, w) {w could be different}
            var edge = Edges[src].Find(e => dest == e.Dest && w != e.Weight);
            if (Edges[src].Count != 0 && edge != null)
            {
                //then change weight of that edge to new weight
                edge.Weight = w;
            }
            else Edges[src].Add(new WeightedEdge(dest, w));
        }
        
        public void PrintGraph(){
            for (int i = 0; i <Size ; i++) {
                List<WeightedEdge> list = Edges[i];
                Console.Write((i+1) + ":");
                for (int j = 0; j <list.Count; j++)
                {
                    Console.Write(String.Format("({0},{1})", list[j].Dest+1, list[j].Weight));
                }
                Console.Write(" ");
            }
        }
    }
}