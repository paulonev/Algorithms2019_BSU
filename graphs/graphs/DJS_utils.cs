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
        
        /// <summary>
        /// Creates new DJS and adds it to List<DJS> 
        /// </summary>
        /// <param name="x">Element to add to DJS</param>
        public void MAKE_SET(int x)
        { 
            DJS djs = new DJS();
            djs.Nodes = new List<Node> {new Node(djs,x)}; //initialization list
            djs.Head = djs.Nodes[0];
            djs.Tail = djs.Head;
            
            sets.Add(djs);
        }
        
        //find head of a set where node with x is stored
        public Node FIND_SET(int x)
        {
            Node? tempNode = null; //create new Node not belonging to any set
            //we need to find set where node with value X is stored
            foreach (var set in sets) 
            { 
                tempNode = set.Nodes?.Find(node => node.Vtx == x);
                if (tempNode != null) break;
            }

            if (tempNode != null) return tempNode.Set.Head;
            return tempNode;
        }
        
        public void UNION(int x, int y)
        {
            DJS s1 = FIND_SET(x).Set;//pointers to set objects
            DJS s2 = FIND_SET(y).Set;
            foreach (var node in s2.Nodes)
            {
                s1.Nodes.Add(node);
                node.Set = s1;
            }
            s1.Tail.NextNode = s2.Head;
            s1.Tail = s2.Tail;
            
            sets.Remove(s2);
        }

    }
}