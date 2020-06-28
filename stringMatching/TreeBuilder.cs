using System;
using System.Collections.Generic;

namespace stringMatching
{
    public class TreeBuilder
    {
        private static Trie _trie;
        public static string Text { get; set; } = "";
        public static List<CompressionTask> Tasks { get; } = new List<CompressionTask>();

        public class CompressionTask
        {
            public Node FirstNode { get; set; }
            public Node LastNode { get; set; }
            public string EdgeLabel { get; set; }

            public CompressionTask(string edgeLabel, Node firstNode, Node lastNode)
            {
                FirstNode = firstNode;
                LastNode = lastNode;
                EdgeLabel = edgeLabel;
            }
        }
        
        public static Trie BuildTreeFromTrie(Trie trie)
        {
            _trie = trie;
            Node root = _trie.GetRoot();
            BuildTree(root);
            
            if(Tasks.Count > 0) DoCompressionTasks();
            return _trie;
        }

        private static void BuildTree(Node node)
        {
            Text = String.Empty;    
            if (node.Adjacent.Count == 0)
                return; 
            
            if (node.Adjacent.Count == 1)
            {
                Text += node.ParentEdge;
                Node nextNode = node;
                while (nextNode.Adjacent.Count == 1)
                {
                    nextNode = nextNode.GetFirstChild();
                    Text += nextNode.ParentEdge;
                }

                Tasks.Add(new CompressionTask(Text, node, nextNode));
                BuildTree(nextNode);
            }
            else
            {
                foreach (var childNode in node.Adjacent.Values)
                {
                    BuildTree(childNode);
                }
            }
            
        }

        
        /// <summary>
        /// Compress suffix trie using given instructions {edgeLabel, firstNode, lastNode}
        /// Where firstNode - first node to be compressed, lastNode - node where to stop compression,
        /// edgeLabel - new label to the compressed path
        /// Example:
        /// 
        /// </summary>
        /// <returns></returns>
        private static void DoCompressionTasks()
        {
            foreach (var task in Tasks)
            {
                Node firstNode = task.FirstNode;
                Node lastNode = task.LastNode;
                string edgeLabel = task.EdgeLabel;
                
                if (firstNode != null && lastNode != null)
                {
                    firstNode.Parent.Adjacent.Remove(firstNode.ParentEdge);
                    firstNode.Parent.Adjacent.Add(edgeLabel, lastNode);

                    lastNode.Parent = firstNode.Parent;
                    lastNode.ParentEdge = edgeLabel;
                }    
            }
        }
    }
}