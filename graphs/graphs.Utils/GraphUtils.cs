using System;
using System.Collections.Generic;
using System.Linq;
using Graphs;
using graphs.Searches;

namespace graphs.Utils
{
    public class GraphUtils
    {

        public GraphUtils()
        {
        }

        /// <summary>
        /// Do Depth-First Search in graph, indicating each vtx with
        /// Open/End-marks. And sort vertices in decreasing order of End-marks
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static List<int> Topological_Sort(Graph graph)
        {
            Search.DFS(graph);
            graph.TpgSortList.Reverse();
            return graph.TpgSortList;
        }

        /// <summary>
        /// Constructor Euler path in graph if it's Euler graph(deg v = 2*k)
        /// Otherwise throw an Exception
        /// TODO: Implement
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public void Euler_Cycle(Graph graph)
        {
            for (int i = 0; i < graph.Size; i++)
            {
                if (graph.GetDegree(i) % 2 == 1)
                {
                    throw new ArgumentException("The graph is not Euler");
                }
            }

        }


        /// <summary>
        /// Define connected components of the given graph
        /// And print vertices of each components per line
        /// </summary>
        /// <param name="g">given graph</param>
        public static void GetConnectedComponents(Graph g)
        {
            Dictionary<int, List<int>> componentsMap = new Dictionary<int, List<int>>();
            List<int> vertices = new List<int>(g.Size);
            for (int i = 0; i < g.Size; i++) vertices.Add(i);

            int temp = 1;
            while (vertices.Count > 0)
            {
                List<int> verticesInComponent = Search.BFS(g, vertices);
                componentsMap[temp] = verticesInComponent;
                vertices = vertices.Except(verticesInComponent).ToList();
                temp++;
            }

            PrintComponents(componentsMap);
        }

        private static void PrintComponents(Dictionary<int, List<int>> map)
        {
            foreach (var pair in map)
            {
                Console.Write("Component #{0}: ", pair.Key);
                foreach (int vtx in pair.Value)
                {
                    Console.Write((vtx+1) + " ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// To find out which notUsed vertex add to the spanning tree
        /// </summary>
        /// <param name="notUsedV">set of notUsed vertices</param>
        /// <param name="weights">distances from vtx to spanTree</param>
        /// <returns></returns>
        public int GetClosestVtx(List<int> notUsedV, int[] weights)
        {
            int min = notUsedV[0];
            foreach (int vtx in notUsedV)
            {
                if (weights[vtx] < weights[min]) min = vtx;
            }
            return min;
        }
    }
}
