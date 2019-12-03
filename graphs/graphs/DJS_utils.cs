using System;
using System.Collections.Generic;

namespace Graphs
{
    public class DJSUtils
    {
        //return 1 if s1.Size > s2.Size, -1 if s1.Size < s2.Size, 0 if they're equally sized
        public delegate int SmallerSet(DJS s1, DJS s2);

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
        /// <param name="s1">first set</param>
        /// <param name="s2">second set</param>
        /// <param name="func">delegate which defines which set is Larger</param>
        public void UNION(DJS s1, DJS s2, SmallerSet func)
        {
            DJS lg = new DJS();
            DJS sm = new DJS();
            if (func(s1,s2) >= 0)
            {
                lg = s1;
                sm = s2;
            }
            else if (func(s1,s2) < 0)
            {
                lg = s2;
                sm = s1;
            }
            
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