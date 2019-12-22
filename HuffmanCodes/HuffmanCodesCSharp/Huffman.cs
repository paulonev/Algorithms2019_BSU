using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;

namespace Huffman_Encoding
{
    public class PriorityQueue<T> where T : IComparable
    {
        protected List<T> LstHeap = new List<T>();
 
        public virtual int Count
        {
            get { return LstHeap.Count; }
        }
 
        public virtual void Add(T val)
        {
            LstHeap.Add(val);
            SetAt(LstHeap.Count - 1, val);
            UpHeap(LstHeap.Count - 1);
        }
 
        //        public T Peek()
//        {
//            if (LstHeap.Count == 0)
//            {
//                throw new IndexOutOfRangeException("Peeking at an empty priority queue");
//            }
// 
//            return LstHeap[0];
//        }
 
        public T ExtractMin()
        {
            if (LstHeap.Count == 0)
            {
                throw new IndexOutOfRangeException("Popping an empty priority queue");
            }
 
            T valRet = LstHeap[0];
 
            SetAt(0, LstHeap[LstHeap.Count - 1]);
            LstHeap.RemoveAt(LstHeap.Count - 1);
            DownHeap(0);
            return valRet;
        }
 
        protected virtual void SetAt(int i, T val)
        {
            LstHeap[i] = val;
        }
 
        protected bool RightSonExists(int i)
        {
            return RightChildIndex(i) < LstHeap.Count;
        }
 
        protected bool LeftSonExists(int i)
        {
            return LeftChildIndex(i) < LstHeap.Count;
        }
 
        protected int ParentIndex(int i)
        {
            return (i - 1) / 2;
        }
 
        protected int LeftChildIndex(int i)
        {
            return 2 * i + 1;
        }
 
        protected int RightChildIndex(int i)
        {
            return 2 * (i + 1);
        }
 
        protected T ArrayVal(int i)
        {
            return LstHeap[i];
        }
 
        protected T Parent(int i)
        {
            return LstHeap[ParentIndex(i)];
        }
 
        protected T Left(int i)
        {
            return LstHeap[LeftChildIndex(i)];
        }
 
        protected T Right(int i)
        {
            return LstHeap[RightChildIndex(i)];
        }
 
        protected void Swap(int i, int j)
        {
            T valHold = ArrayVal(i);
            SetAt(i, LstHeap[j]);
            SetAt(j, valHold);
        }
 
        protected void UpHeap(int i)
        {
            while (i > 0 && ArrayVal(i).CompareTo(Parent(i)) > 0)
            {
                Swap(i, ParentIndex(i));
                i = ParentIndex(i);
            }
        }
 
        protected void DownHeap(int i)
        {
            while (i >= 0)
            {
                int iContinue = -1;
 
                if (RightSonExists(i) && Right(i).CompareTo(ArrayVal(i)) > 0)
                {
                    iContinue = Left(i).CompareTo(Right(i)) < 0 ? RightChildIndex(i) : LeftChildIndex(i);
                }
                else if (LeftSonExists(i) && Left(i).CompareTo(ArrayVal(i)) > 0)
                {
                    iContinue = LeftChildIndex(i);
                }
 
                if (iContinue >= 0 && iContinue < LstHeap.Count)
                {
                    Swap(i, iContinue);
                }
 
                i = iContinue;
            }
        }
    }
 
    public class HuffmanWrapper<T> where T : IComparable
    {
        /// <summary>
        /// Methods that should be in Huffman Codings
        /// 1) First Method:
        ///     - accepts a set of data which have to be encoded by the algorithm
        ///     - create a dictionary, each element of which is a pair (symbol, frequency)
        ///     - initialize min heap and fill it with HuffmanNodes
        ///     - construct the binary tree of nodes
        /// </summary>
        private Dictionary<T, HuffmanNode<T>> _leafDictionary = new Dictionary<T, HuffmanNode<T>>();
        private HuffmanNode<T> _root;
 
        /// <summary>
        /// Huffman Coding - Building a binary tree that will encode symbols
        /// </summary>
        /// <param name="values">A bunch of data of type T</param>
        public void Huffman(IEnumerable<T> values)
        {
            //store symbols and their quantity in the text
            var quant_Dict = Count_Symbols(values);
            //init a min-heap that stores HuffmanNodes
            var priorityQueue = Get_Queue(quant_Dict);
 
            while (priorityQueue.Count > 1)
            {
                HuffmanNode<T> leftSon = priorityQueue.ExtractMin();
                HuffmanNode<T> rightSon = priorityQueue.ExtractMin();
                var parent = new HuffmanNode<T>(leftSon, rightSon);
                priorityQueue.Add(parent);
            }
 
            _root = priorityQueue.ExtractMin();
            _root.IsZero = false;
        }

        /// <summary>
        /// Count appearances of each unique symbol in sequence
        /// </summary>
        /// <param name="values">Sequence of symbols</param>
        /// <returns></returns>
        private Dictionary<T, int> Count_Symbols(IEnumerable<T> values)
        {
            var charsDict = new Dictionary<T, int>();
            foreach (T value in values)
            {
                if(!charsDict.ContainsKey(value))
                {
                    charsDict[value] = 0;
                }
                charsDict[value]++;
            }
            return charsDict;
        }
        
        private PriorityQueue<HuffmanNode<T>> Get_Queue(Dictionary<T,int> symbDict)
        {
            var priorityQueue = new PriorityQueue<HuffmanNode<T>>();
            foreach (T sym in symbDict.Keys)
            {
                var node = new HuffmanNode<T>(sym, symbDict[sym]);
                priorityQueue.Add(node);
                _leafDictionary[sym] = node;
            }

            return priorityQueue;
        }
        
//        public List<int> Encode(T key)
//        {
//            var returnValue = new List<int>();
//            Encode(key);
//            return returnValue;
//        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public Dictionary<T, List<int>> Encode()
        {
            var huffmanDict = new Dictionary<T, List<int>>();
            foreach (HuffmanNode<T> node in _leafDictionary.Values)
            {
                huffmanDict[node.Value] = Encode(node);
            }

            return huffmanDict;
        }


        /// <summary>
        /// For any symbol looks over the binary tree and makes a BitString - binary representation
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="ArgumentException"></exception>
        private List<int> Encode(HuffmanNode<T> key)
        {
            var encoding = new List<int>();
            var tempNode = key;
            while (!tempNode.IsRoot)
            {
                encoding.Add(tempNode.Bit);
                tempNode = tempNode.Parent;
            }

            encoding.Reverse();
            return encoding;
        }
 
 
        public T Decode(List<int> bitString, ref int position)
        {
            HuffmanNode<T> nodeCur = _root;
            while (!nodeCur.IsLeaf)
            {
                if (position > bitString.Count)
                {
                    throw new ArgumentException("Invalid bitstring in Decode");
                }
                nodeCur = bitString[position++] == 0 ? nodeCur.LeftSon : nodeCur.RightSon;
            }
            return nodeCur.Value;
        }
 
        public List<T> Decode(List<int> bitString)
        {
            int position = 0;
            var returnValue = new List<T>();
 
            while (position != bitString.Count)
            {
                returnValue.Add(Decode(bitString, ref position));
            }
            return returnValue;
        }
    }
 
}