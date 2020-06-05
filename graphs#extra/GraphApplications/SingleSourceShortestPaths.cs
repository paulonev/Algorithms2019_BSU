using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Graphs;
using GraphSearch.MinHeap;


namespace DepthFirstSearch
{
    public class SingleSourceShortestPaths
    {
        private int oneMill = 1_000_000;
        private int _source;
//        private DirectedGraph _graph;
        
        // Key-value pairs
        // With key = vertex, value = dijkstra score of vertex
        public Dictionary<int, int> VertexDictionary { get; set; }

        public SingleSourceShortestPaths()
        {}

        public void Dijkstra(DirectedGraph graph, int source)
        {
            _source = source;
            VertexDictionary = new Dictionary<int, int>();
            foreach (var vtx in graph.Edges.Keys)
            {
                VertexDictionary.Add(vtx, vtx == source ? 0 : oneMill);
            }
            
            MinHeap heap = new MinHeap(VertexDictionary);
            foreach (var vertex in VertexDictionary.Keys)
            {
                heap.InsertKey(vertex);
            }

            while (heap.HeapSize > 0)
            {
                var u = heap.ExtractMin(); //added to the 'conquered' territory

                foreach (var edge in graph.Edges[u])
                {
                    //decrease the value of path if it's possible
                    //foreach edge (u,v) where v in V-X do
                    //1.delete v from heap
                    //2.update key(v)
                    //3.insert v into heap
                    //in logarithmic time
                    int v = edge.Dest;
                    int uvWeight = edge.Weight;

                    if (heap.ScoreMap[v] > heap.ScoreMap[u] + uvWeight)
                    {
                        VertexDictionary[v] = heap.ScoreMap[u] + uvWeight;
                        heap.DecreaseKey(v, VertexDictionary[v]);
                    }
                }
            }
            
        }

        public string GetDijkstraOutput()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat($"Dijkstra Score from source {{0}}\n", _source);
            var sortedDict = VertexDictionary.ToList();
            sortedDict.Sort((v1, v2) => v1.Key.CompareTo(v2.Key));
            foreach (var vertex in sortedDict)
            {
                int dist = vertex.Value;
                sb.AppendFormat($"Vertex {vertex.Key}, Distance {dist}\n");
            }

            return sb.ToString();
        }
    }
}