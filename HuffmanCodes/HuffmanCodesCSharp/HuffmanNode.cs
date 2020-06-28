using System;
using System.Collections.Generic;
using Huffman_Encoding;

public class HuffmanNode<T> : IComparable
{
    /// <summary>
    ///  Data stored in Node
    /// </summary>
    public T Value { get; set; }
        
    /// <summary>
    ///  Amount of presence in text or any other string
    /// </summary>
    public int Freq { get; set; }
        
    /// <summary>
    /// Reference to left child in binary tree
    /// </summary>
    public HuffmanNode<T> LeftSon { get; set; }
        
    /// <summary>
    /// Reference to right child in binary tree
    /// </summary>
    public HuffmanNode<T> RightSon { get; set; }
        
    /// <summary>
    /// Reference to parent for Node in binary tree
    /// </summary>
    public HuffmanNode<T> Parent { get; set; }
        
    /// <summary>
    /// To denote whether a Node is leaf
    /// </summary>
    public bool IsLeaf { get; set; }
        
    /// <summary>
    /// Whether an element is a left child of any parent
    /// </summary>
    public bool IsZero { get; set; }

  
    /// <summary>
    ///  
    /// </summary>
    public int Bit => IsZero ? 0 : 1;
    public bool IsRoot => Parent == null;

    public HuffmanNode(T value, int freq)
    {
        Value = value;
        Freq = freq;
        LeftSon = RightSon = Parent = null;
        IsLeaf = true;
//        BitString = new List<char>();
    }
 
    public HuffmanNode(HuffmanNode<T> leftSon, HuffmanNode<T> rightSon)
    {
        LeftSon = leftSon;
        RightSon = rightSon;
        Freq = leftSon.Freq + rightSon.Freq;
        leftSon.IsZero = true;
        rightSon.IsZero = false;
        leftSon.Parent = rightSon.Parent = this;
        IsLeaf = false;
    }
 
    public int CompareTo(object obj)
    {
        return -Freq.CompareTo(((HuffmanNode<T>) obj).Freq);
    }
}