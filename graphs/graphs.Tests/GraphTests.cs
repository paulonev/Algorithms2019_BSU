using System;
using NUnit.Framework;
using graphs;

namespace graphs.Tests
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
        }

        [TestCase(1, 5)]
        public void FloydTest(int src, int dest)
        {
            //implementation of Floyd-Warshall SSP algorithm
            //out - shortest paths from {src} to {dest} and path itself
            
        }
        
    }
}