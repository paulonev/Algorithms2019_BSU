using System;
using GraphColoring.DSatur;
using NUnit.Framework;

namespace GraphColoring.Test
{
    public class GraphColoringTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DSaturAlgorithm_Image1()
        {
            int size = 8;
            Graph graph = new Graph(size);
            
            graph.AddEdge(1,2);
            graph.AddEdge(1,3);
            graph.AddEdge(1,4);
            graph.AddEdge(1,7);
            
            graph.AddEdge(2,5);
            graph.AddEdge(2,6);
            graph.AddEdge(3,7);
            graph.AddEdge(4,7);
            
            graph.AddEdge(5,6);
            graph.AddEdge(5,8);
            graph.AddEdge(6,8);
            graph.AddEdge(7,8);

            DSaturAlgorithm dsa = new DSaturAlgorithm(graph);
            dsa.ColorGraph();

            int colorsUsed = dsa.GetColorCount();
            Console.WriteLine($"Algorithm used {colorsUsed} colors for graph:\n" + $"{graph}");
        }

        [Test]
        public void DSaturAlgorithm_Image2()
        {
            int size = 7;
            Graph graph = new Graph(size);
            
            graph.AddEdge(1,2);
            graph.AddEdge(1,3);
            graph.AddEdge(1,4);
            graph.AddEdge(1,5);
            graph.AddEdge(1,6);
            graph.AddEdge(1,7);
            
            graph.AddEdge(2,3);
            graph.AddEdge(3,7);
            graph.AddEdge(4,7);
            graph.AddEdge(4,5);
            
            graph.AddEdge(5,6);
            graph.AddEdge(6,2);
            
            DSaturAlgorithm dsa = new DSaturAlgorithm(graph);
            dsa.ColorGraph();

            int colorsUsed = dsa.GetColorCount();
            Console.WriteLine($"Algorithm used {colorsUsed} colors for graph:\n" + $"{graph}");
        }
    }
}