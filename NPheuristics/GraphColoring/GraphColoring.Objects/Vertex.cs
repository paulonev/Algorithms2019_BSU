using System;
using System.Collections.Generic;

namespace GraphColoring
{
    /// <summary>
    /// Implemented comparer for ordering colorless vertices in DSatur algorithm
    /// </summary>
    public class SaturationComparer : IComparer<Vertex>
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

    /// <summary>
    /// Comparer for GIS 
    /// </summary>
    public class AdjacencyComparer : IComparer<Vertex>
    {
        public int Compare(Vertex x, Vertex y)
        {
            if (x == null || y == null)
            {
                throw new NullReferenceException("Instance is null");
            }

            int adjComparing = x.AdjDegree.CompareTo(y.AdjDegree);
            if (adjComparing == 0)
            {
                return x.Value.CompareTo(y.Value);
            }
            return adjComparing;
        }
    }
    
    public class Vertex
    {
        private int _satDegree;
        private int _adjDegree;

        public int Value { get; set; }
        public int Color { get; set; } //color of vertex
        public SortedSet<int> AdjColors { get; set; }
        public List<Vertex> AdjVertices { get; set; }
        
        public int SatDegree
        {
            get => AdjColors.Count; 
            private set => _satDegree = value;
        }
        public int AdjDegree  
        {
            get => AdjVertices.Count;
            private set => _adjDegree = value;
        }

//        public void UpdateAdjColors(int color)
//        {
//            bool addColor = true;
//            foreach (var vtxColor in AdjColors)
//            {  
//            }
//            if(addColor) AdjColors.Add(color);
//        }

        public Vertex(int val)
        {
            Value = val;
            Color = -1;
            SatDegree = 0;
            AdjDegree = 0;
            AdjColors = new SortedSet<int>();
            AdjVertices = new List<Vertex>();
        }
        
    }
    
}