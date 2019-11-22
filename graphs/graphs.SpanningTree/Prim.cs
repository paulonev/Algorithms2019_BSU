using System;
using System.Collections.Generic;
using Graphs;

namespace graphs.SpanningTree
{
    public static class SpanningAlgorithms
    {
        
        /// <summary>
        /// Method is used to find a spanningTree of Graph G using Prim's algorithm
        /// </summary>
        /// <param name="n">size of graph</param>
        /// <param name="G">specified graph</param>
        /// <returns>weight of spanningTree</returns>
        public static int Prim(int n, Graph G)
        {
            int firstV = new Random().Next(n); //between 0 and N-1
//            List<int> UsedV = new List<int>();
            List<int> notUsedV = new List<int>();
            for(int i=0; i<n; i++) notUsedV.Add(i);
            List<WeightedEdge> spanTree = Prim(G.AdjacencyList, notUsedV, firstV, new List<WeightedEdge>());
            return CountWeight(spanTree);
        }

        private static List<WeightedEdge> Prim(List<WeightedEdge>[] adjLists, List<int> notUsedV, int curV,
            List<WeightedEdge> spanTree)
        {
            if (notUsedV.Count == 0) return spanTree;
            WeightedEdge newEdge = MinEdge(adjLists[curV], notUsedV);
            spanTree.Add(newEdge);
            notUsedV.Remove(adjLists[curV].FindIndex(e=>e == newEdge));
            return Prim(adjLists, notUsedV, newEdge.Dest, spanTree);
        }

        private static WeightedEdge MinEdge(List<WeightedEdge> adjList, List<int> notUsedV)
        {
            WeightedEdge min = adjList.Find(e => notUsedV.Contains(e.Dest));
            int minIdx = adjList.FindIndex(e=>e == min);
            for (int i = minIdx+1; i < adjList.Count-1; i++)
            {
                if (adjList[i].Weight < min.Weight && notUsedV.Contains(adjList[i].Dest))
                {
                    min = adjList[i];
                }
            }

            return min;
        }

        private static int CountWeight(List<WeightedEdge> spanTree)
        {
            int sum = 0;
            foreach (var edge in spanTree)
            {
                sum += edge.Weight;
            }
            
            return sum;
        }
    }
}
