using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using Huffman_Encoding;

namespace HuffmanCodesCSharp.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        private readonly string Path = "/home/paul/coding/algorithms-data-structures/HuffmanCodesCSharp/test.txt";
        
        
        [TestCase("/home/paul/coding/algorithms-data-structures/HuffmanCodesCSharp/test.txt")]
        public void ReadFromFileTest(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
                {
                    Console.WriteLine(sr.ReadToEnd());
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [Test]
        public void HuffmanEncodingTest()
        {
            string source;
            try
            {
                using (StreamReader sr = new StreamReader(Path, Encoding.UTF8))
                {
                    source = sr.ReadToEnd().ToLower();
                    sr.Close();
                }
                
                var hufWrapper = new HuffmanWrapper<char>();
                hufWrapper.Huffman(source);

                Dictionary<char, List<int>> huffmanDictionary =
                    hufWrapper.Encode();
                
//            List<char> decoding = huffman.Decode(encoding);
//            var outString = new string(decoding.ToArray());
//            Console.WriteLine(outString == source ? "Encoding/decoding worked" : "Encoding/Decoding failed");
 
                foreach (char c in huffmanDictionary.Keys)
                { 
                    Console.Write("{0}:  ", c);
                    foreach (int bit in huffmanDictionary[c])
                    {
                        Console.Write("{0}", bit);
                    }
                    Console.WriteLine();
                }
            
//            using (StreamReader sr = new StreamReader(path))
//            {
//                source = sr.ReadToEnd();
//            }

//            Huffman.Build(source);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}