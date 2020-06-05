using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphColoring.DSatur
{
    public class GIS
    {
        private Graph _graph;
        
        /// <summary>
        /// List for storing uncolored vertices
        /// </summary>
        private List<Vertex> _colorlessVertices;

        /// <summary>
        /// List of vertices that can be colored with C
        /// </summary>
        private List<Vertex> ColorableVertices { get; set; }

        public GIS(Graph graph)
        {
            _graph = graph;
            _colorlessVertices = new List<Vertex>(_graph.Vertices);
            ColorableVertices = new List<Vertex>();
        }

        /// <summary>
        /// Algorithm
        /// </summary>
        public void ColorGraph()
        {
            int C = 1;  
            while (_colorlessVertices.Count > 0)
            {
                ColorableVertices = new List<Vertex>(_colorlessVertices);
                while (ColorableVertices.Any())
                {
                    //sort according to the degree of adjacency with uncolored vertices
                    ColorableVertices.Sort(new AdjacencyComparer());
                    Vertex colorNode = ColorableVertices.First();
                    
                    colorNode.Color = C;
                    
                    foreach (var adjacent in colorNode.AdjVertices) //remove elems adjacent to node colored
                    {
                        ColorableVertices.Remove(adjacent);
                        adjacent.AdjVertices.Remove(colorNode);
                    }

                    _colorlessVertices.Remove(colorNode);
                    ColorableVertices.Remove(colorNode);
                }

                C += 1;
            }
        }
        
        public string GetGisOutput()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("GIS coloring: \n");
            sb.Append("Vtx    | Color\n");
            foreach (var vertex in _graph.Vertices)
            {
                sb.AppendFormat($"Vtx {vertex.Value} - [{vertex.Color}]\n");
            }

            return sb.ToString();
        }
    }
}