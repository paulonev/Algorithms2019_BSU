using System.Collections.Generic;
using System.Security.Cryptography;

namespace Graphs
{
    /// <summary>
    /// Class refers to disjoint-set data structure
    /// And implements methods MAKE-SET(x) , FIND-SET(x), UNION(x,y)
    /// </summary>
    public class DJS
    {
         
        private List<Node> nodes;
        //a head(or representative) of DJS
        private Node head;
        private Node tail;

        public List<Node> Nodes => nodes;
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


        public DJS()
        {
            nodes = null;
            head = null;
            tail = null;
        }
        
        //Creates new DJS set which initially stores only one object
        public DJS MAKE_SET(int x)
        { 
            Node newNode = new Node(this, x);
            nodes = new List<Node> {newNode};
            head = newNode;
            tail = head;
            return this;
        }

        public void DELETE_SET()
        {
            head = null;
            nodes= null;
            tail = null;
        }
    }
}