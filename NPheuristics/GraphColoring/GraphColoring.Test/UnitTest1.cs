using System;
using GraphColoring.DSatur;
using NUnit.Framework;

namespace GraphColoring.Test
{
    public class GraphColoringTests
    {
        [SetUp]
        public void Setup()
        {}

        [Test]
        public void DSaturAlgorithm_Image1()
        {
            string filePath = "/home/paul/coding/algorithms-data-structures/NPheuristics/GraphColoring/graph1Data.txt";

            GraphBuilder.GraphBuilder builder = new GraphBuilder.GraphBuilder();
            Graph graph = builder.BuildGraphFromFile(filePath);
            
            DSaturAlgorithm dsa = new DSaturAlgorithm(graph);
            dsa.ColorGraph();

//            int colorsUsed = dsa.GetColorCount();
//            Console.WriteLine($"Algorithm used {colorsUsed} colors for graph:\n" + $"{graph}");
            Console.WriteLine(dsa.GetDSaturOutput());
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
            graph.AddEdge(3,4);
            graph.AddEdge(4,5);
            
            graph.AddEdge(5,6);
            graph.AddEdge(6,7);
            graph.AddEdge(7,2);            
            DSaturAlgorithm dsa = new DSaturAlgorithm(graph);
            dsa.ColorGraph();

//            int colorsUsed = dsa.GetColorCount();
//            Console.WriteLine($"Algorithm used {colorsUsed} colors for graph:\n" + $"{graph}");
            Console.WriteLine(dsa.GetDSaturOutput());
        }

        [Test]
        public void GreedyIndependentSetsTest()
        {
            string filePath = "/home/paul/coding/algorithms-data-structures/NPheuristics/GraphColoring/graph1Data.txt";

            GraphBuilder.GraphBuilder builder = new GraphBuilder.GraphBuilder();
            Graph graph = builder.BuildGraphFromFile(filePath);
            
            GIS gis = new GIS(graph);
            gis.ColorGraph();

            Console.WriteLine(gis.GetGisOutput());
        }

        [Test]
        public void GISTest2()
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
            graph.AddEdge(3,4);
            graph.AddEdge(4,5);
            
            graph.AddEdge(5,6);
            graph.AddEdge(6,7);
            graph.AddEdge(7,2);            

            GIS gis = new GIS(graph);
            gis.ColorGraph();

            Console.WriteLine(gis.GetGisOutput());   
        }
    }
}