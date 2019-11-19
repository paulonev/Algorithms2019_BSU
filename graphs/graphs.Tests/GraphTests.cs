using System;
using Graphs.Floyd;
using NUnit.Framework;

namespace Graphs.Tests
{
    [TestFixture]
    public class GraphTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PrintTest()
        {
            int vertices = 4;
            Graph gr = new Graph(vertices);
            
            gr.AddEdge(1,2,8);
            gr.AddEdge(1,3,3);
            
            gr.AddEdge(2,1,5);
            gr.AddEdge(2,4,2);
            
            gr.AddEdge(3,2,7);
            gr.AddEdge(3,4,10);
         
            gr.AddEdge(4,1,18);
            gr.AddEdge(4,3,-8);

            gr.PrintGraph();

            Console.WriteLine();
            Graph gr1 = new OrientedGraph(vertices);
            
            gr1.AddEdge(1,2,8);
            gr1.AddEdge(1,3,3);
            
            gr1.AddEdge(2,1,5);
            gr1.AddEdge(2,4,2);
            
            gr1.AddEdge(3,2,7);
            gr1.AddEdge(3,4,10);
         
            gr1.AddEdge(4,1,18);
            gr1.AddEdge(4,3,-8);

            gr1.PrintGraph();

//            gr.AreAdjacent(2, 3);
        }

        [TestCase(1,5)]
        public void FloydTest(int src, int dest)
        {
            //implementation of Floyd-Warshall SSP algorithm
            //out - value of shortest paths from {src} to {dest} and path itself
            int vertices = 5;
            Graph gr = new Graph(vertices);
            
            gr.AddEdge(1,2,8);
            gr.AddEdge(1,3,3);
            gr.AddEdge(1,4,-4);
            gr.AddEdge(1,5,1);

            gr.AddEdge(2,4,7);
            
            gr.AddEdge(3,2,6);
            gr.AddEdge(3,4,3);
            
            gr.AddEdge(4,1,5);
            gr.AddEdge(4,3,-2);
            gr.AddEdge(4,5,1);
            
            gr.AddEdge(5,2,3);
            
            Assert.AreEqual(-3,PathFinders.FloydPathfinder(gr,src,dest));
            //Console.Write(PathFinders.FloydPathfinder(gr,src,dest));
        }
        
    }
}