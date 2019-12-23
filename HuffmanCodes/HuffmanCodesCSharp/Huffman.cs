using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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
        ///  Makes a dictionary where {Keys}:symbols in source, {Values}:prefix code for symbol  
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
        /// For any symbol looks over the binary tree and makes a encoding - binary string representation
        /// </summary>
        /// <param name="key">HuffmanNode</param>
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

//        public static string GetBits( byte inByte )
//        {
//            // Go through each bit with a mask
//            StringBuilder builder = new StringBuilder();
//            for ( int j = 0; j < 8; j++ )
//            {
//                // Shift each bit by 1 starting at zero shift
//                byte tmp =  (byte) ( inByte >> j );
//
//                // Check byte with mask 00000001 for LSB
//                int expect1 = tmp & 0x01; 
//
//                builder.Append(expect1);
//            }
//            return builder.ToString();
//        }
        
        public static String GetByteString(byte b) {
            StringBuilder sb = new StringBuilder();
            for (int i = 7; i >= 0; --i) {
                sb.Append(b >> i & 1);
            }
            return sb.ToString();
        }
        
        //string is IEnumerable<char>
        public string GetEncodedText(IEnumerable<T> source, Dictionary<T, List<int>> huffmanDictionary)
        {
            string encodeText = "";
            foreach (T c in source)
            {
                List<int> bitString = huffmanDictionary[c];
                encodeText += string.Join("", bitString);
            }

            return encodeText;
        }

        public int WriteEncodedToFile(string path, string source)
        {
            byte[] bytes = To_Byte_Array(source);
            int numOfBytes = bytes.Length;
            using (FileStream fs = File.OpenWrite(path))
            {
                fs.Write(bytes);
                fs.Close();
            }

            return numOfBytes;
        }

        public byte[] To_Byte_Array(string source)
        {
            int numBytes = (int) Math.Ceiling(source.Length / 8m);
            var bytesAsStrings =
                Enumerable.Range(0, numBytes)
                    .Select(i => source.Substring(8 * i, Math.Min(8, source.Length - 8 * i)));

            byte[] bytes = bytesAsStrings.Select(s => Convert.ToByte(s, 2)).ToArray();
            return bytes;
        }

        public string ReadBytesFromFile(string path, int numOfBytes)
        {
            string binaryString = "";
            using (FileStream fs = File.OpenRead(path))
            {
                BinaryReader br = new BinaryReader(fs);
                byte[] outBytes = br.ReadBytes(numOfBytes);
                foreach (var b in outBytes)
                {
                    binaryString += HuffmanWrapper<char>.GetByteString(b);
                }
                fs.Close();
            }

            return binaryString;
        }

        public List<T> Decode(string binaryString)
        {
            List<T> ans = new List<T>();

            HuffmanNode<T> nodeCur = _root;
            foreach (var c in binaryString)
            {
                if (c == '0')
                    nodeCur = nodeCur.LeftSon;
                else nodeCur = nodeCur.RightSon;

                if (nodeCur.LeftSon == null && nodeCur.RightSon == null)
                {
                    ans.Add(nodeCur.Value);
                    nodeCur = _root;
                }
            }
            return ans;
        }

        public void WriteDecodedToFile(List<T> text, string pathToFile)
        {
            using (StreamWriter sw = new StreamWriter(pathToFile))
            {
                foreach (var ch in text)
                {
                    sw.Write(ch);
                }
                sw.Close();
            }
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