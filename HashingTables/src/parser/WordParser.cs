using System.IO;
using System;
using System.Collections.Generic;

namespace src.parser
{
    public class WordParser
    {
        public static IDictionary<string, int> ParseFromFile(string filePath)
        {
            var dict = new Dictionary<string, int>();
            try
            {
                using (var fileReader = new StreamReader(filePath))
                {
                    char[] separators = {','};
                    string line;
                    while ((line = fileReader.ReadLine()) != String.Empty)
                    {
                        string[] oneLineWords = line.Split(separators);
                        dict.Add(oneLineWords[0], Int32.Parse(oneLineWords[1]));   
                    }
                }
            } catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
            return dict;
        }
    }
}