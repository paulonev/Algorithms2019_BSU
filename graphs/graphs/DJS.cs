using System.Collections.Generic;
using System.Security.Cryptography;

namespace Graphs
{
    /// <summary>
    /// Class refers to disjoint-set data structure
    /// And implements methods MAKE-SET(x)
    /// TODO: Try weight-union heuristics
    /// </summary>
    public class DJS
    {
        private List<Node> nodes;
        //a head(or representative) of DJS
        private Node head;
        private Node tail;
        private int size;

        public List<Node> Nodes
        {
            get => nodes;
            set => nodes = value;
        }
        public Node Head
        {
            get => head;
            set => head = value;
        }
        public Node Tail
        {
            get => tail;
            set => tail = value;
        }

        public int Size => Nodes.Count;

        public DJS()
        {
            nodes = null;
            head = null;
            tail = null;
        }
    }
}