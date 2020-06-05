using System;
using System.Collections.Generic;

namespace PackingHeuristics
{
    public class Packing
    {
        public static void ReWeight(ref float[] wes)
        {
            float max = wes[0];
            foreach (var t in wes)
            {
                if (t > max) max = t;
            }

            for (int i = 0; i < wes.Length; i++)
            {
                wes[i] /= max;
            }
        }

        /// <summary>
        /// This BinPacking into 1-volumed container approach
        /// 1) puts first elem in first container
        /// k) puts k-th elem in current container, or if it doesn't fit
        ///    creates new container(volume 1) and puts k-th elem there
        /// </summary>
        /// <param name="weights"></param>
        /// <returns>List of created containers</returns>
        public static int NextFit(float[] weights, ref List<float> packing)
        {
            packing = new List<float>{0};
            
            int j = 0;
            packing[j] += weights[0];
            for (int i = 1; i < weights.Length; i++)
            {
                if (weights[i] + packing[j] <= 1)
                {
                    packing[j] += weights[i];
                }
                else
                {
                    float newContainer = weights[i];
                    packing.Add(newContainer);
                    j++; // update current container
                }
            }    

            return packing.Count;
        }
     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="weights"></param>
        /// <returns></returns>
        public static int FirstFit(float[] weights, ref List<float> packing)
        {
            packing = new List<float> {0};
            
            packing[0] += weights[0];
            for (int i = 1; i < weights.Length; i++)
            {
                bool packed = false;
                for(int j = 0; j < packing.Count; j++)
                {
                    if (weights[i] + packing[j] <= 1)
                    {
                        packing[j] += weights[i];
                        packed = true;
                        break;
                    }
                }

                if (!packed)
                {
                    float newContainer = weights[i];
                    packing.Add(newContainer);
                }
            }    

            return packing.Count;
        }

        /// <summary>
        /// A lot like First Fit algorithm, but on k-th step it finds
        /// the BEST container for the elem, that is the smallest among those where elem can go
        /// </summary>
        /// <param name="weights"></param>
        /// <returns></returns>
        public static int BestFit(float[] weights, ref List<float> packing)
        {
            packing = new List<float> {0};

            packing[0] += weights[0];
            for (int i = 1; i < weights.Length; i++)
            {
                float min = Single.PositiveInfinity;
                var bestCont = -1;
                for (int j = 0; j < packing.Count; j++)
                {
                    if (1 - (weights[i] + packing[j]) < min && weights[i] + packing[j] <= 1)
                    {
                        bestCont = j;
                        min = 1 - (weights[i] + packing[j]);
                    }
                }

                if (bestCont != -1)
                {
                    packing[bestCont] += weights[i];
                }
                else
                {
                    float newContainer = weights[i];
                    packing.Add(newContainer);
                }
            }
            
            return packing.Count;
        }
    }

    public class ReverseComparer : IComparer<float>
    {
        public int Compare(float x, float y)
        {
            return (-x).CompareTo(-y);
        }
    }
}