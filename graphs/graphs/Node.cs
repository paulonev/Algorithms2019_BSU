namespace Graphs
{
    /// <summary>
    /// This class refers to DJS nodes(which must contain some value, point to set object and to next node in
    /// linked list of nodes)
    /// </summary>
    public class Node
    {
        //pointer to set
        public DJS Set { get; set; }
        
        //storable value
        public int Vtx { get; }

        //pointer to next node
        public Node NextNode { get; set; }

        public Node(int vtx)
        {
            Set = null;
            Vtx = vtx;
            NextNode = null;
        }
 
        public Node(DJS set, int vtx)
        {
            Set = set;
            Vtx = vtx;
            NextNode = null;
        }
        
        public Node(DJS set, int vtx, Node nextNode)
        {
            Set = set;
            Vtx = vtx;
            NextNode = nextNode;
        }
    }
}