using System;
using System.Collections.Generic;
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
        
    }
}
