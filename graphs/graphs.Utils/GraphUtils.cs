using System;
using System.Collections.Generic;
using Graphs;
using graphs.ADT;
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
