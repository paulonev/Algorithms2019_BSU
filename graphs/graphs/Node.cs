namespace Graphs
{
    /// <summary>
    /// This class refers to DJS nodes(which must contain some value, point to set object and to next node in
    /// linked list of nodes)
    /// </summary>
    public class Node
    {
        //pointer to set
        private DJS set;
        //storable value
        private int vtx;

        public int Vtx => vtx;

        //pointer to next node
        private Node nextNode;

        public DJS Set
        {
            get => set;
            set => set = value;
        }

        public Node NextNode
        {
            get => nextNode;
            set => nextNode = value;
        }

        public Node(int vtx)
        {
            set = null;
            this.vtx = vtx;
            nextNode = null;
        }
        public Node(DJS set, int vtx)
        {
            this.set = set;
            this.vtx = vtx;
            nextNode = null;
        }
        
        public Node(DJS set, int vtx, Node nextNode)
        {
            this.set = set;
            this.vtx = vtx;
            this.nextNode = nextNode;
        }
    }
}