using System;
using System.Collections.Generic;
using System.Text;

namespace GraphColoring
{
    public class Graph
    {
        public int Size { get; set; }
        public List<Vertex> Vertices { get; private set; }

        public Graph()
        {
            Vertices = new List<Vertex>();
        }

        public Graph(int size)
        {
            Size = size;
            Vertices = new List<Vertex>();
            //initialize list of vertices and give them values
            for (int i = 1; i <= size; i++)
            {
                Vertices.Add(new Vertex(i));
            }
        }
        
        public bool AreValid(int v1, int v2)
        {
            return !(v1 < 1 || v1 > Size) || (v2 < 1 || v2 > Size);
        }

        public virtual void AddEdge(int src, int dest)
        {

//            if (src < 1 || src > Size)
//            {
//                throw new ArgumentException($"Can't add this edge // Wrong Source Vertex - {src}");
//            }
//            if (dest < 1 || dest > Size)
//            {
//                throw new ArgumentException("Can't add this edge // Wrong Destination Vertex!");
//            }

            Vertex srcVtx = FindVertex(src);
            Vertex destVtx = FindVertex(dest);
            
            if (!srcVtx.AdjVertices.Exists(v=>v.Value == dest))
            {
                srcVtx.AdjVertices.Add(destVtx);
                destVtx.AdjVertices.Add(srcVtx);
                Console.WriteLine($"Add edge [{src},{dest}] to graph");
            }
        }

        /// <summary>
        /// Given vertex value search for Vertex in graph
        /// If it's not in graph, create new vertex and add to graph
        /// </summary>
        /// <param name="val">search parameter</param>
        /// <returns>vertex, whether found or created</returns>
        private Vertex FindVertex(int val)
        {
            if (!Vertices.Exists(v => v.Value == val))
            {
                Vertex vertex = new Vertex(val);
                Vertices.Add(vertex);
                return vertex;
            }
            return Vertices.Find(v => v.Value == val);
        }
        
        /// <summary>
        /// Attempts to remove edge from list of vertices of graph
        /// Otherwise throw an ArgumentException
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If invalid argument</exception>
        public virtual void RemoveEdge(int src, int dest)
        {
            if (src < 1 || src > Size)
            {
                throw new ArgumentException("Can't remove this edge // Wrong Source Vertex!");
            }
            if (dest < 1 || dest > Size)
            {
                throw new ArgumentException("Can't remove this edge // Wrong Destination Vertex!");
            }

            Vertex srcVtx = Vertices.Find(v => v.Value == src);
            Vertex destVtx = Vertices.Find(v => v.Value == dest);
            if (srcVtx.AdjVertices.Exists(v=>v.Value == destVtx.Value))
            {
                srcVtx.AdjVertices.Remove(destVtx);
                destVtx.AdjVertices.Remove(srcVtx);
                Console.WriteLine($"Remove edge [{src},{dest}] from graph");
            }
            else Console.WriteLine("Can't remove edge // 404 - Edge not found!");
        }
        
        /// <summary>
        /// Outputs graph info to the console
        /// </summary>
        public void PrintGraph()
        {
            foreach (var src in Vertices)
            {
                foreach (var dest in src.AdjVertices)
                {
                    Console.Write($"({src.Value},{dest.Value}) ");
                }
                Console.WriteLine();
            }
        }

        public string GetGraph()
        {
            StringBuilder str = new StringBuilder();
            foreach (var src in Vertices)
            {
                foreach (var dest in src.AdjVertices)
                {
                    str.Append(String.Format($"({src.Value},{dest.Value})\n"));
                }

                str.Append("------\n");
            }
            return str.ToString();
        }

        public override string ToString()
        {
            return GetGraph();
        }
    }

//    public class GraphBuilder
//    {
//        public GraphBuilder()
//        {}
//        
//        public Graph BuildGraphFromFile(string filePath)
//        {
//            Graph graph = new Graph();
//            try
//            {
//                using var fileReader = new StreamReader(filePath);
//                string line;
//                while ((line = fileReader.ReadLine()) != null)
//                {
//                    // each line: i<space>j, where i and j are series of digits, representing label of a vertex
//                    string[] oneLineNumbers = line.Split(' ');
//                    int src = Int32.Parse(oneLineNumbers[0]);
//                    int dest = Int32.Parse(oneLineNumbers[1]);
//                    graph.AddEdge(src, dest);
//                }
//            }
//            catch (NullReferenceException e)
//            {
//                Console.WriteLine(e.Message);
//                throw e;
//            }
//
//            //File.ReadAllLines()
//            return graph;
//        }
//    }
}