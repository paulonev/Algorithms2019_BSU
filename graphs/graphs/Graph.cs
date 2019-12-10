using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Graphs
{
    //implement following methods
    //a)addEdge                    DONE
    //b)AreAdjacent                DONE
    //c)N(v) v as vertex of graph  DONE

    //implement adjacency-list approach of storing graphs
    //in which graph is an ARRAY, which index represents a vertex
    //ARRAY[i] stores a list of adjacent to i vertices
    public class Graph
    {
        public int Size { get; } //size of ARRAY
        public List<WeightedEdge> []AdjacencyList { get; set; } // array of edges outOfVertex

        /// <summary>
        /// Colors: white/grey/black of vertices for DFS search
        /// </summary>
        public int[] Colors { get; set; }

        /// <summary>
        /// Predecessor for each vtx
        /// </summary>
        public int[] Preds { get; set; }

        /// <summary>
        /// Built-in timer that shows when vtx was first reached in DFS
        /// </summary>
        public int[] TimeIn { get; set; }
        
        /// <summary>
        /// Built-in timer that shows when DFS finished working with vtx
        /// </summary>
        public int[] TimeOut { get; set; }

        /// <summary>
        /// Topologically sorted vertices of graph
        /// </summary>
        public List<int> TpgSortList { get; }

        /// <summary>
        /// Minimal distances from i-th vertex to spanning tree or source vtx in Dijkstra
        /// </summary>
        public Dictionary<int, int> MinWeights { get; set; }
        
        public Graph() {}
        public Graph(int size)
        {
            Size = size;
            AdjacencyList = new List<WeightedEdge>[Size];
            for (int i = 0; i < Size; i++)
            {
                AdjacencyList[i] = new List<WeightedEdge>();
            }

            Colors = new int[Size];
            Preds = new int[Size];
            TimeIn = new int[Size];
            TimeOut = new int[Size];
            TpgSortList = new List<int>(size);
//            MinWeights = new int[Size];
        }

        /// <summary>
        /// Returns Weighted Edge of graph if it exists
        /// Otherwise return null
        /// </summary>
        /// <param name="src">source vertex of edge</param>
        /// <param name="dest">destination vertex of edge</param>
        /// <returns></returns>
        public WeightedEdge GetEdge(int src, int dest)
        {
            try
            {
                src--;
                dest--;
                if (src >= Size || src < 0)
                {
                    throw new ArgumentException("Impossible source vertex");
                }
                return AdjacencyList[src].Find(e => e.Dest == dest);
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException($"Edge ({src},{dest}) wasn't found");
            }
            
        }

        /// <summary>
        /// Add adjacent vertex [dest] to the adjacency list of [src] and vice versa
        /// This pair (src, dest) is an edge of graph G
        /// </summary>
        /// <param name="source">source vertex</param>
        /// <param name="d">destination vertex</param>
        /// <param name="w">weight of edge (1 by default)</param>
        public virtual void AddEdge(int src, int dest, int w = 1)
        {
            src--;
            dest--;
            if (!AdjacencyList[src].Exists(e => e.Dest == dest))
            {
                AdjacencyList[src].Add(new WeightedEdge(src, dest, w));   
                AdjacencyList[dest].Add(new WeightedEdge(dest, src, w));
            }
            else
            {
                AdjacencyList[src].Find(e => e.Dest == dest && e.Weight != w).Weight = w;
                AdjacencyList[dest].Find(e => e.Dest == src && e.Weight != w).Weight = w;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ob1">Weighted edge or int</param>
        /// <param name="ob2">Weighted edge or int</param>
        /// <returns>True if adjacent, false otherwise</returns>
        public bool? AreAdjacent(Object ob1, Object ob2)
        {
            if (ob1.GetType() == ob2.GetType())
            {
                if (ob1 is int)
                {
                    return AreAdjacentVertices(ob1, ob2);
                }

                if (ob1 is WeightedEdge)
                {
                    return AreAdjacentEdges(ob1, ob2);
                }
                else return false;
            }
            else return false;
        }

        private bool AreAdjacentVertices(Object ob1, Object ob2)
        {
            //when they are both vertices
            int v1 = (int) ob1;
            int v2 = (int) ob2;
            if (v1 > Size || v1 <= 0) return false;  //IndexOutOfRangeException solved
            v1--;
            v2--;
            return AdjacencyList[v1].Contains(new WeightedEdge(v1, v2));
        }

        private bool? AreAdjacentEdges(Object ob1, Object ob2)
        {
            //then they are both of WeightedEdge type
            WeightedEdge e1 = (WeightedEdge) ob1;
            WeightedEdge e2 = (WeightedEdge) ob2;
            e1.Src--; e1.Dest--;
            e2.Src--; e2.Dest--;
            //if edges exists in graph
            if (GraphHasEdge(e1) && GraphHasEdge(e2))
            {
                return e1.IsIncident(e2.Src) || e1.IsIncident(e2.Dest);
            }

            return null;
        }

        private bool GraphHasEdge(WeightedEdge edge)
        {
            return AdjacencyList[edge.Src].Exists(e => e.Dest == edge.Dest);
        }
        
        public void PrintGraph(){
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
            Console.WriteLine();
        }

        public WeightedEdge[] GetSortedEdges()
        {
            List<WeightedEdge> edgesList = new List<WeightedEdge>();
            for (int i = 0; i < AdjacencyList.Length; i++)
            {
                foreach (WeightedEdge edge in AdjacencyList[i])
                {
                    edgesList.Add(edge);
                }
            }

            WeightedEdge[] edges = edgesList.ToArray();
            Array.Sort(edges, new EdgeComparer());
            return edges;
        }

        /// <summary>
        /// Method that counts vertices in neighborhood of VTX
        /// </summary>
        /// <param name="vtx">specified vertex</param>
        /// <returns>non-negative integer</returns>
        /// <exception cref="ArgumentException"></exception>
        public int GetDegree(int vtx)
        {
            vtx--;
            if (vtx >= Size || vtx < 0) throw new ArgumentException("Input vertex isn't belonging to graph");
            List<int> vtxNeighbors = Neighborhood(vtx);
            return vtxNeighbors.Count;
        }
        
        //a set of neighbors of vertex
        private List<int> Neighborhood(int vtx)
        {
            List<int> neighbors = new List<int>();
            foreach (var edge in AdjacencyList[vtx])
            {
                neighbors.Add(edge.Dest);
            }

            return neighbors;
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

    }
}