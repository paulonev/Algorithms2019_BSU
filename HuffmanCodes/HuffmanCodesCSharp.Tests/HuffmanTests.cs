using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Huffman_Encoding;
using Microsoft.VisualBasic.CompilerServices;

namespace HuffmanCodesCSharp.Tests
{
    public class Tests
    {
        public int BytesCount { get; set; }
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
                using (StreamReader sr = new StreamReader(PathDir + "WarAndPeace1.txt", Encoding.UTF8))
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

        private readonly string PathTestDir =
            "/home/paul/coding/algorithms-data-structures/HuffmanCodes/";

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
        public void HuffmanCompressDecompressTest()
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
            BytesCount = hufWrapper.WriteEncodedToFile(PathDir + "WarAndPeace1_encoded",
                hufWrapper.GetEncodedText(Source, huffmanDictionary));
            
            //Decompress
            string binaryString = hufWrapper.ReadBytesFromFile(PathDir + "WarAndPeace1_encoded", BytesCount);
            List<char> decodedList = hufWrapper.Decode(binaryString);

            hufWrapper.WriteDecodedToFile(decodedList, PathDir + "WarAndPeace1_decoded.txt");
            
        }

        [Test]
        public void Can_Read_Bytes_From_File()
        {
            //1)read bytes from file into GLOBAL string in the form of zeros and ones
            //2)loop over this string(IEnumerable<char>) and when finding prefix code
            //delete it from GLOBAL and append symbol(which has that prefix code) to DECODED_TEXT string
            //3)write DECODED_TEXT into file with name <previous_Name>_decoded.txt
            string input = "01110100011001010111001101110100";
            string pathForFile = PathTestDir + "geig_encoded";
            
            int numBytes = (int) Math.Ceiling(input.Length / 8m);
            var bytesAsStrings =
                Enumerable.Range(0, numBytes)
                    .Select(i => input.Substring(8 * i, Math.Min(8, input.Length - 8 * i)));

            byte[] bytes = bytesAsStrings.Select(s => Convert.ToByte(s, 2)).ToArray();
            using (FileStream fs = File.OpenWrite(pathForFile))
            {
                fs.Write(bytes);
                fs.Close();
            }

            using (FileStream fs = File.OpenRead(pathForFile))
            {
                BinaryReader br = new BinaryReader(fs);
                byte[] outBytes= br.ReadBytes(numBytes);
                string bRepresent = "";
                foreach (var b in outBytes)
                {
                    bRepresent += HuffmanWrapper<char>.GetByteString(b);
                }
                
                Assert.AreEqual(input, bRepresent);
            }
            
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


        [TestCase(6,6)]
        [TestCase(3,4)]
        [TestCase(50,51)]
        [TestCase(15,15)]
        public void Can_Count_Paths_To(int n, int m)
        {
            long[,] matrix = new long[n,m];
            
            long result = CountPathsProblem.GetAmountOfWaysTo(n, m, matrix);
            
            if(n == 3 && m == 4)
                Assert.AreEqual(10, result);
            
            
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write(matrix[i,j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}