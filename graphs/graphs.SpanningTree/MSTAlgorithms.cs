using System;
using System.Collections.Generic;
using System.IO;
using Graphs;

namespace graphs.SpanningTree
{
    public static class MST
    {
        private static int posInf = Int32.MaxValue;
        
        /// <summary>
        /// Method is used to find a spanningTree of Graph G using Prim's algorithm
        /// </summary>
        /// <param name="n">size of graph</param>
        /// <param name="G">specified graph</param>
        /// <plan>
        /// 1. Init c[v] and e[v] foreach v in graph, select starting v randomly
        /// 2. c[v] = inf, as the cheapest cost to nonexisting yet tree T, e[v] = nothing(-1 as nonexisting vtx), as a vtx in T to which v is connected
        /// 3. Q initially contains all vertices, but then v is excluded from Q whenever it's added to T
        /// 4. Loop while Q.Size > 0 
        /// 5. On each iteration new vtx that will be added to tree is selected and deleted from Q
        /// 6. Selection is based on taking vtx that has cheapest weight of edge that connects it to T(ANY vtx in T)
        /// </plan>
        /// <returns>weight of spanningTree</returns>
        public static int Prim_MST(int n, Graph G)
        {
            int firstV = new Random().Next(n); //between 0 and n-1
            int[] minWeights = new int[n]; //for i-th elements contains min weight of edges (w,v) for v
            for (int i = 0; i < n; i++) minWeights[i] = posInf;
            
            List<int> notUsedV = new List<int>(); //vtcs that haven't been added to tree T
            for (int i = 0; i < n; i++)
            {
                if (i == firstV) continue;
                notUsedV.Add(i);
            }

            int[] predecessors = new int [n]; //num of parent vtx
            for (int i = 0; i < n; i++) predecessors[i] = -1;
            
            List<WeightedEdge> spanTree = Prim(G.AdjacencyList, notUsedV, firstV, minWeights, predecessors);
            PrintTree(spanTree);
            return CountWeight(spanTree);
        }
    
        private static List<WeightedEdge> Prim(List<WeightedEdge>[] adjLists, List<int> Q, int curV,
            int[] minWeights, int[] prd)
        {
            List<WeightedEdge> tree = new List<WeightedEdge>();
            while (Q.Count > 0)
            {
                foreach (var edge in adjLists[curV])  
                {
                    int u = edge.Dest; //consider edge (curV, u)
                    if (Q.Contains(u) && edge.Weight < minWeights[u])
                    {
                        minWeights[u] = edge.Weight;
                        prd[u] = curV;
                    }
                }
                //find vtx in Q that has minimal minWeights
                curV = GetClosestVtx(Q, minWeights);
                Q.Remove(curV);
                tree.Add(new WeightedEdge(prd[curV],curV,minWeights[curV]));
            }

            return tree;
        }

        private static int GetClosestVtx(List<int> notUsedV, int[] weights)
        {
            int min = notUsedV[0];
            foreach (int vtx in notUsedV)
            {
                if (weights[vtx] < weights[min]) min = vtx;
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

        private static void PrintTree(List<WeightedEdge> tree)
        {
            Console.WriteLine("Edge  : Weight");
            foreach (var edge in tree)
            {
                Console.WriteLine($"{edge.Src} - {edge.Dest} : {edge.Weight}");
            }
        }

        public static int Kruskal_MST(int n, Graph G)
        {
            int[] vertices = new int[n];
            for (int i = 0; i < n; i++) vertices[i] = i;

            List<WeightedEdge> spanTree = Kruskal_MST(G, vertices);
            return CountWeight(spanTree);
        }

        private static List<WeightedEdge> Kruskal_MST(Graph gr, int[] vtcs)
        {
            List<WeightedEdge> tree = new List<WeightedEdge>();
            List<DJS> sets = new List<DJS>();
            foreach (var vtx in vtcs)
            {
                sets.Add(new DJS().MAKE_SET(vtx));
            }
            DJSUtils ut = new DJSUtils(sets);
            
            WeightedEdge[] grE = gr.GetSortedEdges();
            
            foreach (WeightedEdge edge in grE)
            {
                //consider edge (u,v)
                int u = edge.Src;
                int v = edge.Dest;
                if (ut.FIND_SET(u) != ut.FIND_SET(v))
                {
                    tree.Add(edge);
                    ut.UNION(u,v);
                }
            }

            return tree;
        }
    }
}
