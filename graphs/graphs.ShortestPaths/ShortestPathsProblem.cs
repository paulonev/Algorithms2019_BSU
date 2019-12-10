using System;
using System.Collections.Generic;
using System.Linq;
using Graphs;

namespace graphs.ShortestPaths
{
    /// <summary>
    /// This static class provides shortest-paths issue solution
    /// Dijkstra's single-source shortest-paths solution
    /// </summary>
    public class ShortestPathsProblem
    {
        private static readonly Utils.Utils util = new Utils.Utils();

        public static void Dijkstra(Graph g, int src)
        {
           //TODO: min-priority queue
            List<int> Q = new List<int>(); //Q = V-Path
            for (int i = 0; i < g.Size; i++) Q.Add(i);
            Dijkstra(g, --src, Q);
        }

        private static void Dijkstra(Graph g, int src, List<int> Q)
        {
            util.InitializeSingleSource(g, src);
//            List<int> Path = new List<int>();
            while (Q.Count > 0)
            {
//                int u = util.GetClosestVtx(Q, g.MinWeights); //closest to source {src}
                var u = (g.MinWeights
                    .OrderBy(pair => pair.Value)
                    .First())
                    .Key;
//                Path.Add(u);
                Q.Remove(u);

                foreach (var edge in g.AdjacencyList[u])
                {
                    util.Relaxation(g, edge);
                }
                
                g.MinWeights[u] = Int32.MaxValue;
            }
        }
        
    }
}
