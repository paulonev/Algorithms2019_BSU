using System;
using System.Collections.Generic;
using System.Linq;
using Graphs;
using graphs.ADT;

namespace graphs.ShortestPaths
{
    /// <summary>
    /// This static class provides shortest-paths issue solution
    /// Dijkstra's single-source shortest-paths solution
    /// </summary>
    public class ShortestPathsProblem
    {
//        private static readonly Utils.Utils util = new Utils.Utils();
        private static int posInf = 99999999;

        private Graph graph;

        public ShortestPathsProblem(Graph graph)
        {
            this.graph = graph;
        }
        
        /// <summary>
        /// Finds shortest paths(sequence of edges connecting any v to src) for any vertex of Graph
        /// </summary>
        /// <param name="src"></param>
        public void Dijkstra(int src)
        {
           InitializeSingleSource(src); //for each vtx set predecessor[vtx]=NULL, Mark[vtx]=INF, but for *src* Mark(src)=0
           MinHeap Q = new MinHeap(graph); //implement Extract-Min in (log n), InsertKey in (log n), DecreaseKey in (log n) 
           for (int i = 0; i < graph.Size; i++)
           {
               Q.InsertKey(i);
           }
           
           while (Q.Heap_size > 0)
           {
               var u = Q.ExtractMin();
                
               foreach (var edge in graph.AdjacencyList[u])
               {
                   //decrease the value of path if it's possible to
                   Relaxation(Q, edge);
               }
           }
        }
        
        
        /// <summary>
        /// Method that initializes predecessors and minimal paths for each vertex from src 
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="src"></param>
        private void InitializeSingleSource(int src)
        {
            src--;
            int size = graph.Size;
//            graph.MinWeights = new Dictionary<int, int>();
            for (int i = 0; i < size; i++)
            {
                graph.Preds[i] = -1;
                graph.Marks[i] = posInf;
//                graph.MinWeights.Add(i, posInf);
            }
            graph.Marks[src] = 0; //for source vtx predecessor is 0
        }

        /// <summary>
        /// Optimizes distances from src to edge.Dest through edge.Src
        /// </summary>
        /// <param name="graph">Graph with src vertex</param>
        /// <param name="edge">Adjacent edge to temp vertex</param>
        private void Relaxation(MinHeap Q, WeightedEdge edge)
        {
            //consider (u,v) edge
            int src = edge.Src;
            int dest = edge.Dest;
            int uvWeight = edge.Weight;
//            int uvWeight = graph.AdjacencyList[u].Find(it => it.Dest == v).Weight;
            if (graph.Marks[dest] > graph.Marks[src] + uvWeight)
            {
                graph.Marks[dest] = graph.Marks[src] + uvWeight;
                graph.Preds[dest] = src;
                Q.DecreaseKey(dest, graph.Marks[dest]);
            }

        }

        public void Print_Path(int src, int dest)
        {
            src--;
            dest--;
            if( (src >= 0 && src < graph.Size) || (dest >= 0 && dest < graph.Size)) 
                __Print_Path(src, dest);
            else throw new ArgumentException("Specified source and/or dest are invalid");
        }
        
        /// <summary>
        /// Prints vertices that form shortest path from
        /// given source vertex to given destination vertex
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        private void __Print_Path(int src, int dest)
        {
            if (dest == src) Console.Write(src+1);
            else if (graph.Preds[dest] == -1) //if dest doesn't have predecessor 
                Console.WriteLine($"No path from {src+1} to {dest+1} exists");
            else
            {
                int destOut = dest+1;
                Console.Write(destOut + " <-- ");
                __Print_Path(src, graph.Preds[dest]);
            }
        }

        
    }
}
