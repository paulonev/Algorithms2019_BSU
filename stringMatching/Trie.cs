using System;
using System.Collections.Generic;
using System.Linq;

namespace stringMatching
{
    public class Node
    {
        public Node Parent { get; set; }
        public Dictionary<string, Node> Adjacent { get; set; }
        public string ParentEdge { get; set; } // portion of suffix with which parentNode got to currentNode
        public int? LeafValue { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent">could be null, if add root</param>
        /// <param name="parentSymb">symbol which lead parent to current</param>
        public Node(Node parent, string parentSymb)
        {
            Parent = parent;
            ParentEdge = parentSymb;
            Adjacent = new Dictionary<string, Node>();
        }
        public Node GetFirstChild() => Adjacent.Values.First();
    }
    
    public class Trie
    {
        private Node _root;

        public int Size { get; set; }
        public Trie(Node root)
        {
            _root = root;
            Size = 1;
        }

        public void SetRoot(Node root)
        {
            _root = root;
        }
        public Node GetRoot() => _root;

        /// <summary>
        /// Returns true if any pattern matches prefix of text
        /// false - otherwise
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
//        private bool PrefixTrieMatching(int start, string text)
//        {
//            int root = 0; //root of _trie
//            var node = _trie[root];
//            
//            for (int j = start; j <= text.Length; ) //start of matching
//            {
//                char symb = ' ';
//                if(j < text.Length)
//                    symb = text[j];
//                    
//                
//                if (node.Adjacent.ContainsKey(symb))
//                {
//                    node = _trie[node.Adjacent[symb]];
//                    j++;
//                }
//                    
//                else if (node.GetEndOfPattern()/*node is a leaf or end of pattern but not leaf of trie*/)
//                    return true;
//                
//                else break; //no pattern match
//            }
//            
//            return false;
//        }

        /// <summary>
        /// Produces the array of matching indexes of patterns to text
        /// </summary>
        /// <param name="text">text for prefix matching</param>
        /// <returns></returns>
//        public List<int> TrieMatching(string text)
//        {
//            List<int> indexes = new List<int>();
//            for (int i = 0; i < text.Length; i++)
//            {
//                if (PrefixTrieMatching(i, text))
//                {
//                    indexes.Add(i);
//                }
//            }
//            return indexes;
//        }

        public void TraverseTrie()
        {
            if (_root == null)
                Console.WriteLine("Trie has no nodes...");
            else TraverseTrie(_root);
        }
        
        
        /// <summary>
        /// Outputs trie in the following form:
        /// {srcNode} -> {edgeSymbol} : {destNode}
        /// </summary>
        /// <returns></returns>
        private void TraverseTrie(Node node)
        {
            if (node.Adjacent.Count == 0)
                return;

            foreach (var nextNode in node.Adjacent.Values)
            {
                Console.WriteLine(nextNode.ParentEdge);
                TraverseTrie(nextNode);
            }
        }
    }
}