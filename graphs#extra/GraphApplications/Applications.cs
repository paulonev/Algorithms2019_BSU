using System;
using System.Linq;
using Graphs;

namespace DepthFirstSearch
{
    public class Compute
    {
        public static void StronglyConnectedComponents(Graph graph)
        {
            //Kosaraju's two-pass algorithm  
            //First pass - compute "magic" numbers for each node of REVERSED graph
            //Second pass - find SCC by applying DFS on nodes with decreasing "magic" numbers
            //2*O(n+m) efficient
            DepthSearchApplications scc = new DepthSearchApplications(graph.GetReverseGraph());

            scc.DfsTool();
            scc.CurrentPass++;
            scc.AppGraph = graph; //set forward graph for SCC's second pass
            scc.DfsTool();

            int counter = 1;
            //sort SCC by value, that is by their sizes
            var SCC_s = scc.Leader.ToList();
            SCC_s.Sort((x,y) => x.Value.CompareTo(y.Value));
            foreach (var pair in SCC_s)
            {
                Console.WriteLine($"SCC #{counter++}: Leader {pair.Key}, Size {pair.Value}");
            }
            //SortedList - a collection of Key-Value pairs, sorted by key 
        }

        public static void DijkstraSingleSource(Graph graph, int source)
        {
            
        }
    }
}