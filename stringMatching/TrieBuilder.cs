namespace stringMatching
{
    public class TrieBuilder
    {
        
        /// <summary>
        /// Build a trie of suffixes of Text which are |Text| in amount
        /// </summary>
        /// <param name="text">what to build a trie from</param>
        /// <returns></returns>
        public static Trie BuildSuffixTrie(string text)
        {
            text = text.Insert(text.Length, "$");
            
            var rootNode = new Node(null, " "); //root has no parent !!!
            Trie trie = new Trie(rootNode);
            
            for (int i = 0; i < text.Length; i++)
            {
                //add suffix to trie while adding new nodes
                var currentNode = trie.GetRoot();
                for (int j = i; j < text.Length; j++)
                {
                    var currentSymbol = text[j].ToString();
                    if (currentNode.Adjacent.ContainsKey(currentSymbol))
                    {
                        currentNode = currentNode.Adjacent[currentSymbol];
                    }
                    else
                    {
                        var newNode = new Node(currentNode, currentSymbol);
                        currentNode.Adjacent.Add(currentSymbol, newNode);
                        trie.Size++;
                        currentNode = newNode;
                    }

                    if (j == text.Length - 1)
                    {
                        currentNode.LeafValue = i;
                    }
                }
            }

            return trie;
        }
        
        //public static List<Node> BuildTrie(List<string> patterns)
//        {
//            int root = 0;
//            List<Node> trie = new List<Node> {new Node(root)};
//
//            int nodesAdded = 0;
//            foreach (var pattern in patterns)
//            {
//                int currentNode = root;
//                for (int i = 0; i < pattern.Length; i++)
//                {
//                    char currentSymbol = pattern[i];
//                    
//                    if (trie[currentNode].Adjacent.ContainsKey(currentSymbol))
//                    {
//                        currentNode = trie[currentNode].Adjacent[currentSymbol];
//                    }
//                    else
//                    {
//                        int newNode = ++nodesAdded;
//                        trie[currentNode].Adjacent.Add(currentSymbol, newNode);
//                        trie.Add(new Node(newNode));
//                        
//                        currentNode = newNode;
//                    }
//
//                    if(i == pattern.Length - 1)
//                        trie[currentNode] = trie[currentNode].SetEndOfPattern(true);
//                }
//            }
//
//            return trie;
//        }


        //        public static List<Dictionary<char, int>> BuildTrie(List<string> patterns)
//        {
//            var trie = new List<Dictionary<char, int>> {new Dictionary<char, int>()};
//            //write your code here    
//            int root = 0;
//            
//            int nodesAdded = 0;    
//            
//            foreach (var pattern in patterns)
//            {
//                int currentNode = root;
//                
//                for(int i=0; i < pattern.Length; i++)
//                {
//                    char currentSymbol = pattern[index: i];
//                    var adjList = trie[index: currentNode]; //neighbours of currentNode
//            
//                    if (adjList.ContainsKey(key: currentSymbol))
//                    {
//                        currentNode = adjList[key: currentSymbol];
//                    }
//                    else
//                    {
//                        int newNode = ++nodesAdded;
//                        trie[index: currentNode].Add(key: currentSymbol, value: newNode);
//                        trie.Add(item: new Dictionary<char, int>());
//                        currentNode = newNode;
//                    }
//                }
//            }
//            return trie;
//        }

        
        //        public static string TrieToString(List<Dictionary<char, int>> trie)
//        {
//            StringBuilder sb = new StringBuilder();
//            for (int i = 0; i < trie.Count; i++)
//            {
//                foreach (var edge in trie[index: i])
//                {
//                    sb.AppendFormat(format: $"{i}->{edge.Value.ToString()}:{edge.Key}\n");
//                }
//            }
//
//            return sb.ToString();
//        }
    }    
}

//int m = pat.length();
//int n = txt.length();
//int skip;
//for (int i = 0; i <= n - m; i += skip) {
//skip = 0;
//for (int j = m-1; j >= 0; j--) {
//    if (pat.charAt(j) != txt.charAt(i+j)) {
//        skip = Math.max(1, j - right[txt.charAt(i+j)]);
//        break;
//    }
//}
//if (skip == 0) return i;    // found
//}
//return n;                       // not found