using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace GraphColoring
{
    public class Graph
    {
        public int Size { get; set; }
        public List<Vertex> Vertices { get; private set; }

        public Dictionary<int,List<int>> ConnectedComponentsDict { get; set; }
        //new edges will be added in the form of
        //graph.AddEdge(src,dest)
        public Graph()
        {
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
            if (src < 1 || src > Size)
            {
                throw new ArgumentException("Can't add this edge // Wrong Source Vertex!");
            }
            if (dest < 1 || dest > Size)
            {
                throw new ArgumentException("Can't add this edge // Wrong Destination Vertex!");
            }

            Vertex srcVtx = Vertices.Find(v => v.Value == src);
            Vertex destVtx = Vertices.Find(v => v.Value == dest);
            if (!srcVtx.AdjVertices.Exists(v=>v.Value == destVtx.Value))
            {
                srcVtx.AdjVertices.Add(destVtx);
                destVtx.AdjVertices.Add(srcVtx);
                Console.WriteLine($"Add edge [{src},{dest}] to graph");
            }
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
}