using System;
using System.Collections.Generic;
using Graphs;
using static graphs.Searches.Searches;

namespace graphs.Utils
{
    public class Utils
    {
        public List<int> Topological_Sort(Graph graph)
        {
            DFS(graph);
            graph.TpgSortList.Reverse();
            return graph.TpgSortList;
        }
    }
}
