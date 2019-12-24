using System;
using System.Collections.Generic;
using System.IO;
using Graphs;

namespace graphs.SpanningTree
{
    public static class MST
    {
        private static int posInf = Int32.MaxValue;
        private static Utils.GraphUtils _graphUtil = new Utils.GraphUtils();

        /// <summary>
        /// Method is used to find a spanningTree of Graph G using Prim's algorithm
        /// </summary>
        /// <param name="n">size of graph</param>
        /// <param name="G">specified graph</param>
        /// <plan>
        /// 1. Init c[v] and e[v] foreach v in graph, select starting vertex V randomly
        /// 2. c[v] = inf, as the cheapest cost to nonexisting yet tree T, e[v] = nothing(-1 as nonexisting vtx), as a vtx in T to which v is connected
        /// 3. Q initially contains all vertices, but then v is excluded from Q whenever it's added to T
        /// 4. Loop while Q.Size > 0 
        /// 5. On each iteration new vtx that will be added to tree is selected and deleted from Q
        /// 6. Selection is based on taking vtx that has cheapest weight of edge that connects it to T(ANY vtx in T)
        /// </plan>
        /// <returns>weight of spanningTree</returns>
        public static int Prim_MST(Graph G)
        {
            //==========Init block begin============
            int n = G.Size;
            int firstV = new Random().Next(n); //select starting vertex
            int[] minWeights = new int[n]; //store minimal distances from each vertex to graph
            for (int i = 0; i < n; i++)
            {
                minWeights[i] = posInf;
                G.Preds[i] = -1;
            }
            
            List<int> notUsedV = new List<int>(); //vertices that haven't been added to tree T yet
            for (int i = 0; i < n; i++)
            {
                if (i == firstV) continue;
                notUsedV.Add(i);
            }

//            int[] predecessors = new int [n]; //num of parent for vertex
//            for (int i = 0; i < n; i++) 
            //==========Init block end============
            
            List<WeightedEdge> spanTree = 
                Prim(G.AdjacencyList, notUsedV,firstV, minWeights, G.Preds);
            PrintTree(spanTree);
            return CountWeight(spanTree);
        }
    
        /// <summary>
        /// Main logic of Prim's MST algorithm
        /// </summary>
        /// <param name="adjLists"></param>
        /// <param name="Q"></param>
        /// <param name="curV"></param>
        /// <param name="minWeights"></param>
        /// <param name="prd"></param>
        /// <returns></returns>
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
                curV = _graphUtil.GetClosestVtx(Q, minWeights);
                Q.Remove(curV);
                tree.Add(new WeightedEdge(prd[curV],curV,minWeights[curV]));
            }

            return tree;
        }
        
        
        /// <summary>
        /// Summarize weights of weighted edges of spanning tree
        /// </summary>
        /// <param name="spanTree">complete spanning tree of graph</param>
        /// <returns></returns>
        private static int CountWeight(List<WeightedEdge> spanTree)
        {
            int sum = 0;
            foreach (var edge in spanTree)
            {
                sum += edge.Weight;
            }
            
            return sum;
        }

        //====================<KRUSKAL>====================
        public static int Kruskal_MST(Graph graph)
        {
            List<WeightedEdge> spanTree = Kruskal_MST_sec(graph);
            PrintTree(spanTree);
            return CountWeight(spanTree);
        }

        /// <summary>
        /// Main logic of Kruskal's MST algorithm using disjoint sets
        /// </summary>
        /// <param name="gr">Researched graph</param>
        /// <returns></returns>
        private static List<WeightedEdge> Kruskal_MST_sec(Graph gr)
        {
            List<WeightedEdge> tree = new List<WeightedEdge>();
            List<DJS> sets = new List<DJS>();
            DJSUtils ut = new DJSUtils(sets);

            for (int i = 0; i < gr.Size; i++)
            {
                ut.MAKE_SET(i); //O(V)
            }
            
            WeightedEdge[] grE = gr.GetSortedEdges(); //O(E logE)
            
            foreach (WeightedEdge edge in grE) //O(E)
            {
                //consider edge (u,v)
                int u = edge.Src;
                int v = edge.Dest;
                DJS uSet = ut.FIND_SET(u).Set;
                DJS vSet = ut.FIND_SET(v).Set;
                
                if (uSet != vSet)
                {
                    tree.Add(edge);
                    ut.UNION(uSet, vSet, (s1, s2) => {
                        if (s1.Size > s2.Size) return 1;
                        if (s1.Size < s2.Size) return -1;
                        return 0;
                    });
                }
            }

            return tree;
        }
        
        private static void PrintTree(List<WeightedEdge> tree)
        {
            Console.WriteLine("Edge  : Weight");
            foreach (var edge in tree)
            {
                Console.WriteLine($"{edge.Src+1} - {edge.Dest+1} : {edge.Weight}");
            }
        }
    }
}
