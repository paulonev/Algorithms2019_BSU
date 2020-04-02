using System.Collections.Generic;
using System.Linq;

namespace GraphColoring.DSatur
{
    public class DSaturAlgorithm
    {
        private List<int> colors;
        private List<Vertex> colorlessVertices;

        public DSaturAlgorithm(Graph graph)
        {
            colors = new List<int>{1};
            colorlessVertices = new List<Vertex>(graph.Vertices);
        }

        public int GetColorCount() => colors.Count;
        
        /// <summary>
        /// (c)Brelaz - DSatur coloring graph algorithm. O(n^2) worst case
        /// Is optimal for several types of topologies, like wheel-graph. 
        /// Produces a certain order of coloring, firstly take nodes with greatest "degree of saturation"
        /// If they are equal between 2 or more nodes, then take such v that has the biggest N(v)
        /// Gives the smallest possible color to the node.
        /// If {1,3,4} - available colors for node V, then color(V) = 1
        /// </summary>
        public void ColorGraph()
        {
            while (colorlessVertices.Count > 0)
            {
                colorlessVertices.Sort(new VertexComparer());
                Vertex colorNode = colorlessVertices[^1]; //extract (1 from the end) node
                SortedSet<int> tempColors = new SortedSet<int>(colors);
                tempColors.ExceptWith(colorNode.AdjColors); //!!!

                int colorForNode;
                if (tempColors.Count > 0)
                {
                    colorForNode = tempColors.Min;
                }
                else
                {
                    colorForNode = colors.Count + 1;
                    colors.Add(colorForNode);
                }

                colorNode.Color = colorForNode;
                
                //update adjColors & satDegree for every neighbour of the selected vertex
                foreach(var node in colorNode.AdjVertices)
                {
                    node.AdjColors.Add(colorNode.Color);
                    node.AdjColors = node.AdjColors.Distinct().ToList();
                }

                colorlessVertices.Remove(colorNode);
            }
        }
        
    }
}
