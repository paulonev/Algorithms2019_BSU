using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Graphs
{
    /// Graph component
    /// Represents any edge of graph (tail, head, weight)
    /// where tail - start of edge, head - end of edge, weight - integral characteristic of edge
    public struct GraphEdge
    {
        public ushort Source { get; set; }
        public ushort Dest { get; set; }
        public ushort Weight { get; set; }
        
        public GraphEdge(ushort src, ushort dest, ushort w)
        {
            Source = src;
            Dest = dest;
            Weight = w;
        }
        
    }
    
    public class DirectedGraph : Graph
    {
        public Dictionary<int, List<GraphEdge>> Edges { get; set; }

        public DirectedGraph() : base()
        {
            Edges = new Dictionary<int, List<GraphEdge>>();
        }

        public override void AddEdge(ushort src, ushort dest, ushort weight, bool readFromFile = false)
        {
            if (!readFromFile)
            {
                if (src < 1 || src > Size)
                {
                    throw new ArgumentException("Can't add this edge // Wrong Source Vertex!");
                }
                if (dest < 1 || dest > Size)
                {
                    throw new ArgumentException("Can't add this edge // Wrong Destination Vertex!");
                }    
            }

            bool containsSrc = Edges.ContainsKey(src);
            bool containsDest = Edges.ContainsKey(dest);

            if(!containsDest)
                Edges.Add(dest,new List<GraphEdge>());
            if (!containsSrc)
                Edges.Add(src, new List<GraphEdge>{new GraphEdge(src, dest, weight)});
            else
                Edges[src].Add(new GraphEdge(src, dest, weight));
            //Console.WriteLine($"Add directed edge [{src},{dest}] to graph");
        }
        public override void AddEdge(int src, int dest, bool readFromFile = false)
        {
            if (!readFromFile)
            {
                if (src < 1 || src > Size)
                {
                    throw new ArgumentException("Can't add this edge // Wrong Source Vertex!");
                }
                if (dest < 1 || dest > Size)
                {
                    throw new ArgumentException("Can't add this edge // Wrong Destination Vertex!");
                }    
            }

            bool containsSrc = Adjacents.ContainsKey(src);
            bool containsDest = Adjacents.ContainsKey(dest);
            
            if (!containsDest) Adjacents.Add(dest, new List<int>());
            
            if (!containsSrc)
            {
                Adjacents.Add(src, new List<int>{dest});
            } else if (!Adjacents[src].Contains(dest))
            {
                Adjacents[src].Add(dest);
            }
            //Console.WriteLine($"Add directed edge [{src},{dest}] to graph");
        }

        public override void RemoveEdge(int src, int dest)
        {
            if (Adjacents[src].Contains(dest))
            {
                Adjacents[src].Remove(dest);
                Console.WriteLine($"Remove directed edge [{src},{dest}] from graph");
            }
            else Console.WriteLine("Can't remove directed edge // 404 - Edge not found!");
        }
    }

    public class Graph
    {
        public int Size => Adjacents.Keys.Count;
        public Dictionary<int, List<int>> Adjacents { get; private set; }

        public Dictionary<int, List<int>> ConnectedComponentsDict { get; set; }
        //new edges will be added in the form of
        //graph.AddEdge(src,dest)
        
        public Graph()
        {
            Adjacents = new Dictionary<int, List<int>>();
        }
        
        public Graph GetReverseGraph()
        {
            Graph g = new Graph();
            foreach (var pair in this.Adjacents)
            {
                foreach (var dest in pair.Value)
                {
                    int src = pair.Key;
                    if(!g.Adjacents.ContainsKey(dest)) g.Adjacents.Add(dest, new List<int>());
                    if(!g.Adjacents.ContainsKey(src)) g.Adjacents.Add(src, new List<int>()); 
                    
                    g.Adjacents[dest].Add(src); //pair.Key was src but became dest
                }
            }
            //O(n+m)
            
            return g;
        }

        public bool AreValid(int v1, int v2)
        {
            return !(v1 < 1 || v1 > Size) || (v2 < 1 || v2 > Size);
        }
        
        public virtual void AddEdge(int src, int dest, bool readFromFile = false)
        {
            if(!readFromFile)
            {
                if (src < 1 || src > Size)
                {
                    throw new ArgumentException("Can't add this edge // Wrong Source Vertex!");
                }
                if (dest < 1 || dest > Size)
                {
                    throw new ArgumentException("Can't add this edge // Wrong Destination Vertex!");
                }    
            }

            if (!Adjacents[src].Contains(dest))
            {
                Adjacents[src].Add(dest);
                Adjacents[dest].Add(src);
                Console.WriteLine($"Add edge [{src},{dest}] to graph");
            }
        }
        
        public virtual void AddEdge(ushort src, ushort dest, ushort weight, bool readFromFile = false)
        {
            if(!readFromFile)
            {
                if (src < 1 || src > Size)
                {
                    throw new ArgumentException("Can't add this edge // Wrong Source Vertex!");
                }
                if (dest < 1 || dest > Size)
                {
                    throw new ArgumentException("Can't add this edge // Wrong Destination Vertex!");
                }    
            }

            if (!Adjacents[src].Contains(dest))
            {
                Adjacents[src].Add(dest);
                Adjacents[dest].Add(src);
                Console.WriteLine($"Add edge [{src},{dest}] to graph");
            }
        }
        
        /// <summary>
        /// Attempts to remove edge from adjacents lists of graph
        /// Returns 0 - if removal succeeded, -1 - if edge wasn't found
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If invalid argument</exception>
        public virtual void RemoveEdge(int src, int dest)
        {
            if (src < 1 || src > Size)
            {
                throw new ArgumentException("Can't remove this edge // Wrong Source Vertex!");
            }
            if (dest < 1 || dest > Size)
            {
                throw new ArgumentException("Can't remove this edge // Wrong Destination Vertex!");
            }

            if (Adjacents[src].Contains(dest))
            {
                Adjacents[src].Remove(dest);
                Adjacents[dest].Remove(src);
                Console.WriteLine($"Remove edge [{src},{dest}] from graph");
            }
            else Console.WriteLine("Can't remove edge // 404 - Edge not found!");
        }

        /// <summary>
        /// Returns len of path from s to d, or -1 - if d - not reachable from s
        /// Than they belong to different Connected Components
        /// <seealso cref="ConnectedComponents"/>
        /// </summary>
        /// <param name="s"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public int ShortestPaths(int s, int d)
        {
            // indexes     0  1  2  3  ... s ...
            // vertices   {1  2  3  4  ... s+1 ...}
            // explored   [0  0  0  0  ... 1 ...]
            // dists      [f  f  f  f  ... 0 ...]
            //where f is INF
            if (AreValid(s, d))
            {
                Queue<int> Q = new Queue<int>();
                bool[] explored = new bool[Size];
                int[] dists = new int[Size];
                for (int i = 0; i < Size; i++)
                {
                    if (i == s - 1)
                    {
                        explored[i] = true;
                        dists[i] = 0;
                    }
                    else
                    {
                        dists[i] = -1; //indicates not source vertices
                    }
                }

                Q.Enqueue(s);
                Console.WriteLine($"Enqueue {s}");
                while (Q.Count != 0)
                {
                    //take out first vertex named V
                    int v = Q.Dequeue();
                    Console.WriteLine($"Dequeue {v}");
                    foreach (var w in Adjacents[v])
                    {
                        //consider edges (v,w) for any w in adjacent to v
                        if (!explored[w - 1])
                        {
                            Console.WriteLine($"Find unexplored {w}");
                            explored[w - 1] = true;
                            dists[w - 1] = dists[v - 1] + 1;
                            Q.Enqueue(w);
                            Console.WriteLine($"Enqueue {w}");
                        }

                        else Console.WriteLine($"{w} been explored already");
                    }
                }

                return dists[d - 1];
            }
            else return -1;
        }

        public void Compute_UCC()
        {
            int numCC = 0;
            ConnectedComponentsDict = 
                new Dictionary<int, List<int>>(); //because any graph has at least 1 CC
            bool[] explored = new bool[Size];
            
            foreach (var vtx in Adjacents.Keys)
            {
                if (!explored[vtx - 1])
                {
                    explored[vtx - 1] = true;
                    numCC++;
                    Console.WriteLine($"Initialize {numCC} connected component");
                    ConnectedComponentsDict.Add(numCC, new List<int>{vtx});
                    BFS(this, vtx, ref explored);
                    PrintDict(ConnectedComponentsDict);
                }
            }
        }

        /// <summary>
        /// Makes changes to ConnectedComponentsDict of graph
        /// </summary>
        /// <param name="g"></param>
        /// <param name="v"></param>
        /// <param name="exp"></param>
        private void BFS(Graph g, int v, ref bool[] exp)
        {
            Queue<int> Q = new Queue<int>();
            Q.Enqueue(v);
            while (Q.Count != 0)
            {
                int u = Q.Dequeue();
                foreach (int w in Adjacents[u])
                {
                    if (!exp[w - 1])
                    {
                        Q.Enqueue(w);
                        exp[w - 1] = true;
                        g.ConnectedComponentsDict[g.ConnectedComponentsDict.Keys.Last()]
                            .Add(w);
                    }
                }    
            }
            
        }
        
        public void PrintGraph()
        {
            foreach (var src in Adjacents.Keys)
            {
                foreach (var dest in Adjacents[src])
                {
                    Console.Write($"({src},{dest}) ");
                }
                Console.WriteLine();
            }
        }

        private void PrintDict(Dictionary<int,List<int>> dictionary)
        {
            Console.WriteLine("Current dict ->");
            foreach (var cc in dictionary.Keys)
            {
                Console.Write($"Component #{cc}: [");
                foreach (var vtx in dictionary[cc])
                {
                    Console.Write(" " + vtx);
                }
                Console.WriteLine(" ]");
            }
        }
    }
}
