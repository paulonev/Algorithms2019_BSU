using System.Collections.Generic;

namespace Graphs
{
    public class DJSUtils
    {
        //a list of dis-joint sets
        private List<DJS> sets;

        public DJSUtils(List<DJS> sets)
        {
            this.sets = sets;
        }
        
        //find head of a set where node with x is stored
        public Node FIND_SET(int x)
        {
            Node tempNode = null; //create new Node not belonging to any set
            //we need to find set where node with value X is stored
            do
            {
                foreach (var set in sets)
                {
                    tempNode = set.Nodes.Find(node => node.Vtx == x);
                }
            } while (tempNode == null);

            return tempNode.Set.Head;
        }
        
        public void UNION(int x, int y)
        {
            DJS s1 = FIND_SET(x).Set;//pointers to set objects
            DJS s2 = FIND_SET(y).Set;
            foreach (var node in s2.Nodes)
            {
                node.Set = s1;
            }
            s1.Tail.NextNode = s2.Head;
            s1.Tail = s2.Tail;
            s2.DELETE_SET();
        }

    }
}