using System;
using System.Collections.Generic;
using Graphs;
using static graphs.Searches.Searches;

namespace graphs.Utils
{
    public class Utils
    {
        private static int posInf = 99999999;

        public Utils()
        {}
        
        public List<int> Topological_Sort(Graph graph)
        {
            DFS(graph);
            graph.TpgSortList.Reverse();
            return graph.TpgSortList;
        }
        

        /// <summary>
        /// Method that initializes predecessors and minimal paths for each vertex from src 
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="src"></param>
        public void InitializeSingleSource(Graph graph, int src)
        {
            int size = graph.Size;
            graph.MinWeights = new Dictionary<int, int>();
            for (int i = 0; i < size; i++)
            {
                graph.Preds[i] = -1;
                graph.MinWeights.Add(i, posInf);
            }
            graph.MinWeights[src] = 0; //for source vtx predecessor is 0
        }

        /// <summary>
        /// Optimizes distances from src to edge.Dest through edge.Src
        /// </summary>
        /// <param name="graph">Graph with src vertex</param>
        /// <param name="edge">Adjacent edge to temp vertex</param>
        public void Relaxation(Graph graph, WeightedEdge edge)
        {
            //consider (u,v) edge
            int u = edge.Src;
            int v = edge.Dest;
            int uvWeight = edge.Weight;
//            int uvWeight = graph.AdjacencyList[u].Find(it => it.Dest == v).Weight;
            if (graph.MinWeights[v] > graph.MinWeights[u] + uvWeight)
            {
                graph.MinWeights[v] = graph.MinWeights[u] + uvWeight;
                graph.Preds[v] = u;
            }
        }

        public void Print_Path(Graph g, int src, int dest)
        {
            src--;
            dest--;
            if( (src >= 0 && src < g.Size) || (dest >= 0 && dest < g.Size)) 
                __Print_Path(g, src, dest);
            else throw new ArgumentException("Specified source and/or dest are invalid");
        }
        
        /// <summary>
        /// Prints vertices that form shortest path from
        /// given source vertex to given destination vertex
        /// </summary>
        /// <param name="g"></param>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        private void __Print_Path(Graph g, int src, int dest)
        {
            if (dest == src) Console.Write(src);
            else if (g.Preds[dest] == -1) //if dest doesn't have predecessor 
                Console.WriteLine($"No path from {src} to {dest} exists");
            else Print_Path(g, src, g.Preds[dest]);
        }
        
        /// <summary>
        /// To findout which notUsed vertex add to the spanning tree
        /// </summary>
        /// <param name="notUsedV">set of notused vertices</param>
        /// <param name="weights">distances from vtx to spanTree</param>
        /// <returns></returns>
        public int GetClosestVtx(List<int> notUsedV, int[] weights)
        {
            int min = notUsedV[0];
            foreach (int vtx in notUsedV)
            {
                if (weights[vtx] < weights[min]) min = vtx;
            }
            return min;
        }
    }
}
