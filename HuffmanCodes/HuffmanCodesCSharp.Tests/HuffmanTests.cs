using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Huffman_Encoding;

namespace HuffmanCodesCSharp.Tests
{
    public class Tests
    {
        private static string Source { get; set; }
        private readonly string PathDir = "/home/paul/coding/algorithms-data-structures/HuffmanCodes/";

        /// <summary>
        /// Open streamReader and read all symbols to Source
        /// And close streamReader
        /// </summary>
        [SetUp]
        public void Setup()
        {
            try
            {
                using (StreamReader sr = new StreamReader(PathDir+"test.txt", Encoding.UTF8))
                {
                    Source = sr.ReadToEnd().ToLower();
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private readonly string PathTest =
            "/home/paul/coding/algorithms-data-structures/HuffmanCodes/geighartburgenstrauss.txt";

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
        public void HuffmanCompressTest()
        {
            var hufWrapper = new HuffmanWrapper<char>();
            hufWrapper.Huffman(Source);

            Dictionary<char, List<int>> huffmanDictionary =
                hufWrapper.Encode();
            
            foreach (char c in huffmanDictionary.Keys)
            {
                Console.Write("{0}:  ", c);
                foreach (int bit in huffmanDictionary[c])
                {
                    Console.Write("{0}", bit);
                }

                Console.WriteLine();
            }
            
            // write in file encoded text
            hufWrapper.WriteEncodedToFile(PathDir + "testEncoded", 
                hufWrapper.GetEncodedText(Source, huffmanDictionary));
        }

        [Test]
        public void HuffmanDecompressTest()
        {
            
        }
        
        
        [Test]
        public void SplitStringTest()
        {
            string input = "011101000110010101110011011101000101";
            string pathForFile =
                "/home/paul/coding/algorithms-data-structures/HuffmanCodes/encodedText";
            int numBytes = (int) Math.Ceiling(input.Length / 8m);
            var bytesAsStrings =
                Enumerable.Range(0, numBytes)
                    .Select(i => input.Substring(8 * i, Math.Min(8, input.Length - 8 * i)));

            byte[] bytes = bytesAsStrings.Select(s => Convert.ToByte(s, 2)).ToArray();
            using (FileStream fs = File.OpenWrite(pathForFile))
            {
                fs.Write(bytes);
            }
            
            foreach (var s in bytesAsStrings)
            {
                Console.WriteLine(s);
            }


        }
}
}