using System;
using System.Collections;
using System.Collections.Generic;
using Graphs;
using DepthFirstSearch;
using Graphs.Builder;

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
                
                Graph graph = new GraphBuilder().BuildFromFile("/home/paul/coding/algorithms-data-structures/graphs#extra/GraphSearch/SCC.txt");
//                graph.PrintGraph();
                Compute.StronglyConnectedComponents(graph);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
    }
}