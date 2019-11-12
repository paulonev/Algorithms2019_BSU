using System;

namespace graphs.Floyd
{
    public class PathFinders
    {
        public static int FloydPathfinder(Graph graph, int src, int dest)
        {
            const int INF = 10^9; //value of unexisting path
            int N = graph.Size;
            int[,] paths = new int[N,N]; //contains all paths in graph
            int[,] refMatrix = new int[N,N]; //for path reconstruction
            
            //generate modified adjacency matrix with weights on (i,j) places
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    var edge = graph.Edges[i].Find(item=>item.Dest == j);
                    if (edge != null)
                    {
                        paths[i, j] = edge.Weight;
                    }
                    else paths[i, j] = INF;
                }
            }
            
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    refMatrix[i, j] = j;
                }
            }
            
            //Floyd's pathFinder loops, O(N^3) on average
            for (int p = 0; p < N; p++)
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        if(i != p && j!= p) //isolate p-th row and column
                        {
                            if (paths[i, p] + paths[p, j] < paths[i, j])
                            {
                                paths[i, j] = paths[i, p] + paths[p, j];
                                refMatrix[i, j] = refMatrix[i, p];
                            }
                        } 
                    }   
                }
            }
            
            PathReconstructor(refMatrix, src, dest);
            return paths[--src, --dest]; //return shortest path between {src} , {dest} vertices
        }
        
        private static void PathReconstructor(int[,] refMatrix, int src, int dest)
        {
            //draws the shortest path from {src=2} to {dest=5}, like {2->3->4->1->5}    
            
        }
        
    }
}
