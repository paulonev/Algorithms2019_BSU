using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Graphs;

namespace graphs.Searches
{
    /// <summary>
    /// This static class is used to implement BFS and DFS search algorithms
    /// </summary>
    public class Searches
    {
        public static int Time { get; set; }
        
        /// <summary>
        /// Deep-first search of graph
        /// This method goes over a directed/undirected graph G
        /// Recursive strategy..
        /// </summary>
        public static void DFS(Graph g)
        {
            int size = g.Size;
//            for (int i = 0; i < size; i++) g.Colors[i] = 0;
            for (int i = 0; i < size; i++) g.Preds[i] = -1;
            Time = 0;

            for (int i = 0; i < size; i++)
            {
                if (g.Colors[i] == 0)
                    DFS_VISIT(g,i);
            }
        }

        private static void DFS_VISIT(Graph graph, int u)
        {
            graph.Colors[u] = 1; //color vtx to grey, because algorithm reached it
            graph.TimeIn[u] = ++Time;

            foreach (var edge in graph.AdjacencyList[u])
            {
                //consider edge (u,v)
                int v = edge.Dest;
                if (graph.Colors[v] == 0)
                {
                    graph.Preds[v] = u;
                    DFS_VISIT(graph, v);
                }
            }

            graph.Colors[u] = 2; //color vtx to black, because algorithm looked up all it's adj vertices
            graph.TimeOut[u] = ++Time;
            graph.TpgSortList.Add(u+1);
        }

        /// <summary>
        /// Way of traversing directed acyclic graph from source vtx
        /// Returns vertices that belong to connected component 
        /// </summary>
        /// <param name="graph">directed acyclic graph</param>
        /// <param name="vertices">vertices not yet been examined</param>
        public static List<int> BFS(Graph graph, List<int> vertices)
        {
            int src = vertices[0];
            graph.InitializeSingleSource(src+1);
            Queue<int> Q = new Queue<int>();
            Q.Enqueue(src);
            List<int> verticesPerComponent = new List<int>();
            verticesPerComponent.Add(src);
            
            while (Q.Count != 0)
            {
                int u = Q.Dequeue();
                foreach (var edge in graph.AdjacencyList[u])
                {
                    int v = edge.Dest;
                    if (graph.Colors[v] == 0)
                    {
                        graph.Colors[v] = 1;
                        graph.Marks[v] = graph.Marks[u] + 1;
                        graph.Preds[v] = u;
                        Q.Enqueue(v);
                        verticesPerComponent.Add(v);
                    }
                }

                graph.Colors[u] = 2;
            }

            return verticesPerComponent;
        }
    }
}
