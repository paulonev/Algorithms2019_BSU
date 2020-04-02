using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Graphs;

namespace DepthFirstSearch
{
    public class Compute
    {
        public static void StronglyConnectedComponents(Graph graph)
        {
            //Kosaraju's two-pass algorithm  
            //First pass - compute "magic" numbers for each node of REVERSED graph
            //Second pass - find SCC by applying DFS on nodes with decreasing "magic" numbers
            //2*O(n+m) efficient
            DepthSearchApplications scc = new DepthSearchApplications(graph.GetReverseGraph());

            scc.DfsTool();
            scc.CurrentPass++;
            scc.AppGraph = graph; //set forward graph for SCC's second pass
            scc.DfsTool();

            int counter = 1;
            //sort SCC by value, that is by their sizes
            var SCC_s = scc.Leader.ToList();
            SCC_s.Sort((x,y) => x.Value.CompareTo(y.Value));
            foreach (var pair in SCC_s)
            {
                Console.WriteLine($"SCC #{counter++}: Leader {pair.Key}, Size {pair.Value}");
            }
            //SortedList - a collection of Key-Value pairs, sorted by key 
        }
    }

    public class DepthSearchApplications
    {
        public Graph AppGraph { get; set; }
        
        private int s; // 0, by default
        private int timer; //0 , by default

        public int CurrentPass { get; set; } //holds 1 - if algorithm is doing 1 pass, 2 - if 2 pass
        public Dictionary<int, int> FTime { get; set; }
        
        /// <summary>
        /// Holds 3 states of Node
        /// 0 - if node hasn't been explored
        /// 1 - if node has been explored but not fully(there's been at least 1 adjacent non-explored vtx)
        /// 2 - if node has been finished in exploration
        /// </summary>
        public Dictionary<int, int> Explored { get; set; }
        
        public Dictionary<int, int> Leader { get; set; }

        public List<int> Nodes      // represent the list of all nodes of graph
        {
            get
            {
                var tempList = FTime.ToList();
                FTime.Clear();
                //first sort by decreasing order of nodes
                //second sort by decreasing order of Ftimes of nodes
                if(CurrentPass == 1) tempList.Sort((n1,n2) => n2.Key.CompareTo(n1.Key));
                else tempList.Sort((n1,n2) => n2.Value.CompareTo(n1.Value));
                foreach (var pair in tempList)
                {
                    FTime.Add(pair.Key, pair.Value);
                }

                return FTime.Keys.ToList();
            }
        }        
        
        public DepthSearchApplications(Graph graph)
        {
            AppGraph = graph;
            FTime = new Dictionary<int, int>();
            Leader = new Dictionary<int, int>();
            
            foreach (var node in graph.Adjacents.Keys)
            {
                FTime.Add(node, 0);
            }
            
            CurrentPass = 1;
        }

        public void DfsTool()
        {
            Explored = new Dictionary<int, int>();
            foreach (var key in Nodes)
            {
                Explored.Add(key, 0);
            }

            foreach (var node in Nodes)
            {
                if (Explored[node] == 0)
                {
                    s = node;
                    //DFS(AppGraph, node);
                    DFS_iter(AppGraph, node);
                }
            }
        }

        private void DFS_iter(Graph graph, int node)
        {
            Stack<int> st = new Stack<int>();
            st.Push(node);

            while (st.Count != 0)
            {
                int v = st.Pop();
                if (Explored[v] != 2)
                {
                    st.Push(v); // ??
                    if (Explored[v] == 0)
                    {
                        Explored[v] = 1;
                    }
                    
                    bool allAdjExplored = true;
                    foreach (int w in graph.Adjacents[v])
                    {
                        if (Explored[w] == 0)
                        {
                            allAdjExplored = false;
                            st.Push(w);
                            break;
                        }
                    }

                    if (allAdjExplored)
                    {
                        Explored[v] = 2; // v becomes finished, there are no outgoing arcs to unexplored

                        if (CurrentPass == 1)
                        {
                            timer++;
                            FTime[v] = timer;
                        }

                        else if (CurrentPass == 2)
                        {
                            if (!Leader.ContainsKey(s))
                            {
                                Leader.Add(s, 1);
                            }
                            else Leader[s]++;    
                        }
                        st.Pop();
                    }
                }
                
            }
        }
        
        // Recursive approach - Caused StackOverflow on 875714 nodes from Stanford Course
        
//        private void DFS(Graph graph, int node)
//        {
//            try
//            {
//                if (CurrentPass == 2)
//                {
//                    if (SccDict.ContainsKey(s)) SccDict[s]++;
//                    else
//                    {
//                        SccDict.Add(s,1);
//                    }
//                }
//            
//                // All the way UP!
//                Explored[node] = true; //mark i-th vtx as explored
//                foreach (int j in graph.Adjacents[node])
//                {
//                    if (!Explored[j])
//                    {
//                        DFS(graph,j); //recursion depth is heavy - Stack overflow
//                    }
//                }
//
//                if (CurrentPass == 1)
//                {
//                    timer++;
//                    FTime[node] = timer;
//                    FTimeNodes.Add(node);   
//                }
//            }
//            catch (StackOverflowException e)
//            {
//                Console.WriteLine("On node " + node);
//                throw new StackOverflowException("Something went wrong on DFS recursion\n" + e.Message);
//            }
//            
//        }
//        
    }
}
