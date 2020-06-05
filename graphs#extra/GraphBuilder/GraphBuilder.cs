using System;
using System.IO;
using Graphs;

namespace GraphsBuilder
{
    public class GraphBuilder
    {
        public GraphBuilder()
        {}

        public DirectedGraph BuildFromFileForDijkstra(string filePath)
        {
            DirectedGraph graph = new DirectedGraph();
            try
            {
                using var fileReader = new StreamReader(filePath);
                string line;
                char[] charSeparators = {' ', ',', '\t'};
                while ((line = fileReader.ReadLine()) != null)
                {
                    // each row contains: #src   dest1, w1   dest2,w2   dest3,w3  ... destn,wn
                    string[] oneLineNumbers = line.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                    var src = UInt16.Parse(oneLineNumbers[0]); // src of string
                    for (int i = 1; i < oneLineNumbers.Length; i += 2)
                    {
                        var dest = UInt16.Parse(oneLineNumbers[i]);
                        var weight = UInt16.Parse(oneLineNumbers[i+1]);
                        graph.AddEdge(src, dest, weight, true);
                    }
                }

                return graph;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        
        public Graph BuildFromFileForSCC(string filePath)
        {
            Graph graph = new DirectedGraph();
            try
            {
                using var fileReader = new StreamReader(filePath);
                string line;
                while ((line = fileReader.ReadLine()) != null)
                {
                    // each line: i<space>j, where i and j are series of digits, representing label of a vertex
                    string[] oneLineNumbers = line.Split(' ');
                    int src = Int32.Parse(oneLineNumbers[0]);
                    int dest = Int32.Parse(oneLineNumbers[1]);
                    graph.AddEdge(src, dest, true);
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }

            //File.ReadAllLines()
            return graph;
        }
    }
}
