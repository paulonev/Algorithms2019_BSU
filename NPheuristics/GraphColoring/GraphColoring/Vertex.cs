using System;
using System.Collections.Generic;

namespace GraphColoring
{
    /// <summary>
    /// Implemented comparer for ordering colorless vertices in DSatur algorithm
    /// </summary>
    public class VertexComparer : IComparer<Vertex>
    {
        public int Compare(Vertex x, Vertex y)
        {
            if (x == null || y == null)
            {
                throw new NullReferenceException("Instance is null");
            }

            int satComparing = x.SatDegree.CompareTo(y.SatDegree);
            if (satComparing == 0)
            {
                return x.AdjDegree.CompareTo(y.AdjDegree);
            }
            return satComparing;
        }
    }
    
    public class Vertex
    {
        private int _satDegree;
        private int _adjDegree;

        public int Value { get; set; }
        
        //Current color set by DSatur for vertex
        public int Color { get; set; }
        
        //Storage for different colors of adjacents of vertex
        public List<int> AdjColors { get; set; }
        public List<Vertex> AdjVertices { get; set; }
        
        public int SatDegree
        {
            get => AdjColors.Count; 
            set => _satDegree = value;
        }
        public int AdjDegree  
        {
            get => AdjVertices.Count;
            set => _adjDegree = value;
        }
        
        public Vertex(int val)
        {
            Value = val;
            Color = -1;
            SatDegree = 0;
            AdjDegree = 0;
            AdjColors = new List<int>();
            AdjVertices = new List<Vertex>();
        }
        
    }
    
}