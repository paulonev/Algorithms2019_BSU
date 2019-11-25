using System;
using Graphs.Floyd;
using graphs.SpanningTree;
using NUnit.Framework;

namespace Graphs.Tests
{
    [TestFixture]
    public class GraphTests
    {
        private int size;
        private Graph graph;
        private Graph graph1;
        
        [SetUp]
        public void Setup()
        {
            size = 4;
            graph = new Graph(size);

            graph.AddEdge(1,2,8);
            graph.AddEdge(1,3,3);

            graph.AddEdge(2,1, 5);
            graph.AddEdge(2,4, 2);

            graph.AddEdge(3, 2, 7);
            graph.AddEdge(3, 4, 10);

            graph.AddEdge(4, 1, 18);
            graph.AddEdge(4, 3, -8);
            
            //----------------------------------
            graph1 = new OrientedGraph(size);
            
            graph1.AddEdge(1,2,8);
            graph1.AddEdge(1,3,3);
            
            graph1.AddEdge(2,1,5);
            graph1.AddEdge(2,4,2);
            
            graph1.AddEdge(3,2,7);
            graph1.AddEdge(3,4,10);
         
            graph1.AddEdge(4,1,18);
            graph1.AddEdge(4,3,-8);


        }

        [Test]
        public void PrintTest()
        {
            graph.PrintGraph();
            Console.WriteLine();
            graph1.PrintGraph();
        }

        [Test]
        public void AdjacencyTest()
        {
            try
            {
                WeightedEdge we1 = new WeightedEdge(1,3);
                WeightedEdge we2 = new WeightedEdge(3,4);

                if (graph.AreAdjacent(we1, we2)) Console.WriteLine("v1 and v2 are adjacent");
                else Console.WriteLine("v1 and v2 aren't adjacent");
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

        }

        [Test]
        public void PrimTests()
        {
            Console.WriteLine("===Prim MST search implementation on graphs===");
            int minWeight = SpanningAlgorithms.Prim(size, graph);
            Console.WriteLine($"Weight of spanningTree: {minWeight}");
        }

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
        }
        
    }
}