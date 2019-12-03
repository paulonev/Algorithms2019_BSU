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
        
        /// <summary>
        /// Using weight-union heuristics append smaller set to larger
        /// </summary>
        /// <param name="lg">larger set</param>
        /// <param name="sm">smaller set</param>
        public void UNION(DJS lg, DJS sm)
        {
            foreach (var node in sm.Nodes)
            {
                lg.Nodes.Add(node);
                node.Set = lg;
            }
            lg.Tail.NextNode = sm.Head;
            lg.Tail = sm.Tail;
            
            sets.Remove(sm);
        }

    }
}