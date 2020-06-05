using System;
using System.Runtime.InteropServices.ComTypes;
using Graphs;
using DepthFirstSearch;
using GraphsBuilder;

namespace GraphSearch
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
//                int size = 9;
//                Graph graph = new DirectedGraph(size);
//            
//                graph.AddEdge(1,4);
//                graph.AddEdge(4,7);
//                graph.AddEdge(7,1);
//                
//                graph.AddEdge(9,7);
//                graph.AddEdge(9,3);
//                graph.AddEdge(3,6);
//                graph.AddEdge(6,9);
//                
//                graph.AddEdge(8,6);
//                graph.AddEdge(8,5);
//                graph.AddEdge(5,2);
//                graph.AddEdge(2,8);
//                graph.PrintGraph();
//                
                //int src = 4;
                //int dest = 6;
                //Console.WriteLine($"Attempt to find length of SP ({src},{dest})");
                //Console.WriteLine(graph.ShortestPaths(src, dest));
                
                //Try Programming Assignment #1
                //Reading edges from file

                string kosarajuPath =
                    "/home/paul/coding/algorithms-data-structures/graphs#extra/GraphSearch/SCC.txt";
//                Graph graph = new GraphBuilder().BuildFromFileForSCC(kosarajuPath);
//                Compute.StronglyConnectedComponents(graph);
                
                
//                string dijkstraTestPath =
//                    "/home/paul/coding/algorithms-data-structures/graphs#extra/GraphSearch/dijkstraDataTest.txt";
//                
                string dijkstraPath =
                    "/home/paul/coding/algorithms-data-structures/graphs#extra/GraphSearch/dijkstraData.txt";
                DirectedGraph graph1 = new GraphBuilder().BuildFromFileForDijkstra(dijkstraPath);
                SingleSourceShortestPaths ssp = new SingleSourceShortestPaths();
                ssp.Dijkstra(graph1, 1);

                Console.WriteLine(ssp.GetDijkstraOutput());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
    }
}
