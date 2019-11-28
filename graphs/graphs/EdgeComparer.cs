using System;
using System.Collections.Generic;

namespace Graphs
{
    public class EdgeComparer : IComparer<WeightedEdge>
    {
        public int Compare(WeightedEdge x, WeightedEdge y)
        {
            if(x == null || y==null) throw new ArgumentNullException("Cannot compare null objects");
            if (x.Weight > y.Weight)
                return 1;
            else if (x.Weight < y.Weight)
                return -1;
            else 
                return 0;
        }
    }
}