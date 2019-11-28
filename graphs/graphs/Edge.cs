using System.Runtime.CompilerServices;

namespace Graphs
{
//    public class Edge
//    {
//        public int Dest { get; set; }
//
//        public Edge(){}
//
//        public Edge(int dest)
//        {
//            Dest = dest;
//        }
//    }
    public class WeightedEdge
    {
        public int Src { get; set; }
        public int Dest { get; set; }
        public int Weight { get; set; }

        public WeightedEdge() {}
        public WeightedEdge(int src, int dest, int weight = 1)
        {
            Src = src;
            Dest = dest;
            Weight = weight;
        }

        public bool IsIncident(int destVert)
        {
            return Dest == destVert;
        }

    }
    

}