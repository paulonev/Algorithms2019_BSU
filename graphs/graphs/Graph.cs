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
        private readonly int N; //size of ARRAY
        private List<WeightedEdge> []edges; // array of edges outOfVertex

        public Graph(int n)
        {
            N = n;
            edges = new List<WeightedEdge>[N];
            for (int i = 0; i < N; i++)
            {
                edges[i] = new List<WeightedEdge>();
            } 
        }

        public void AddEdge(int src, int dest, int w)
        {
            //if among edges outOf src is edge(dest, w) {w could be different}
            var edge = edges[src-1].Find(e => dest == e.Dest && w != e.Weight);
            if (edges[src-1].Count != 0 && edge != null)
            {
                //then change weight of that edge to new weight
                edge.Weight = w;
            }
            else edges[src-1].Add(new WeightedEdge(dest, w));
        }
        
        public void PrintGraph(){
            for (int i = 0; i <N ; i++) {
                List<WeightedEdge> list = edges[i];
                for (int j = 0; j <list.Count; j++) {
                    Console.WriteLine("vertex-" + (i+1) + " is connected to " +
                                       list[j].Dest + " with weight " +  list[j].Weight);
                }
            }
        }
    }
}