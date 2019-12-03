using System;
using System.Collections.Generic;
using Graphs.Floyd;
using graphs.SpanningTree;
using graphs.Utils;
using NUnit.Framework;

namespace Graphs.Tests
{
    [TestFixture]
    public class GraphTests
    {
        private Graph graph;
        private Graph graph1;
        private Graph graph2;
        
        [SetUp]
        public void Setup()
        {
            graph = new Graph(7);

            graph.AddEdge(1,2,4);
            graph.AddEdge(1,7,8);

            graph.AddEdge(2,3, 8);
            graph.AddEdge(2,4,3);
            graph.AddEdge(2,5,2);
            graph.AddEdge(2,7,1);
            
            graph.AddEdge(3, 4, 4);
            graph.AddEdge(3,6,2);
            
            graph.AddEdge(4, 5, 2);
            graph.AddEdge(4,6,2);
            
            graph.AddEdge(5,6,6);
            graph.AddEdge(5,7,1);
            
            graph.AddEdge(6,7,7);
 
            //----------------------------------
            graph1 = new OrientedGraph(4);
            
            graph1.AddEdge(1,2,8);
            graph1.AddEdge(1,3,3);
            
            graph1.AddEdge(2,1,5);
            graph1.AddEdge(2,4,2);
            
            graph1.AddEdge(3,2,7);
            graph1.AddEdge(3,4,10);
         
            graph1.AddEdge(4,1,18);
            graph1.AddEdge(4,3,-8);
            //-----------------------------------
            
            graph2 = new OrientedGraph(8);
            graph2.AddEdge(1,5);
            
            graph2.AddEdge(2,1);
            graph2.AddEdge(2,6);
            
            graph2.AddEdge(3,2);
            
            graph2.AddEdge(4,8);
            graph2.AddEdge(4,7);
            
            graph2.AddEdge(6,5);
            
            graph2.AddEdge(7,2);
            graph2.AddEdge(7,6);
            
            graph2.AddEdge(8,7);
        }

        [Test]
        public void PrintTest()
        {
            graph.PrintGraph();
//            Console.WriteLine(graph.GetDegree(6));
            Console.WriteLine();
            graph1.PrintGraph();
        }

        [Test]
        public void AdjacencyTest()
        {
            WeightedEdge we1 = new WeightedEdge(6,7);
            WeightedEdge we2 = new WeightedEdge(4,6);

            bool? result = graph.AreAdjacent(we1, we2);
            if (result.HasValue) Console.WriteLine("v1 and v2 are adjacent");
            else Console.WriteLine("v1 and v2 aren't adjacent");
        }

        [Test]
        public void PrimTests()
        {
            Console.WriteLine("===Prim MST search implementation on graphs===");
            int minWeight = MST.Prim_MST(graph);
            Console.WriteLine($"Weight of spanningTree: {minWeight}");
        }

        [Test]
        public void KruskalTests()
        {
            Console.WriteLine("===Kruskal MST search implementation on graphs===");
            int minWeight = MST.Kruskal_MST(graph);
            Console.WriteLine($"Weight of spanningTree: {minWeight}");
        }

        [Test]
        public void TopologicalSortTest()
        {
            Utils ut = new Utils();
            List<int> topSort = ut.Topological_Sort(graph2);

            Console.Write("Graph: ");
            graph2.PrintGraph();
            Console.Write("Topological sort output: ");
            foreach (var vtx in topSort)
            {
                Console.Write(vtx + " ");
            }
            
        }
        
        //Floyd-Warshall searchPaths
//        [TestCase(1,5)]
//        public void FloydTest(int src, int dest)
//        {
//            //implementation of Floyd-Warshall SSP algorithm
//            //out - value of shortest paths from {src} to {dest} and path itself
//            int vertices = 5;
//            Graph gr = new Graph(vertices);
//            
//            gr.AddEdge(1,2,8);
//            gr.AddEdge(1,3,3);
//            gr.AddEdge(1,4,-4);
//            gr.AddEdge(1,5,1);
//
//            gr.AddEdge(2,4,7);
//            
//            gr.AddEdge(3,2,6);
//            gr.AddEdge(3,4,3);
//            
//            gr.AddEdge(4,1,5);
//            gr.AddEdge(4,3,-2);
//            gr.AddEdge(4,5,1);
//            
//            gr.AddEdge(5,2,3);
//            
//            Assert.AreEqual(-3,PathFinders.FloydPathfinder(gr,src,dest));
//            //Console.Write(PathFinders.FloydPathfinder(gr,src,dest));
//        }

        [TearDown]
        public void CleanSource()
        {
            graph = null;
            graph1 = null;
            graph2 = null;
        }
        
    }
}