using System;
using System.IO;

namespace GraphColoring.GraphBuilder
{
    public class GraphBuilder
    {
        public Graph BuildGraphFromFile(string filePath)
        {
            Graph graph = new Graph();
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
                    graph.AddEdge(src, dest);
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
