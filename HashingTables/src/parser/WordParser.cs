using System.IO;
using System;
using System.Collections.Generic;

namespace src.parser
{
    public class WordParser
    {
        public static IList<Tuple<string, int>> ParseFromFile(string filePath, int numLines)
        {
            var list = new List<Tuple<string, int>>();
            try
            {
                using (var fileReader = new StreamReader(filePath))
                {
                    for (int i = 0; i < numLines; i++)                    
                    {
                        string[] oneLineWords = fileReader.ReadLine().Split(',');
                        var tp = new Tuple<string, int>(oneLineWords[0], Int32.Parse(oneLineWords[1]));
                        list.Add(tp);   
                    }
                }
            } catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
            return list;
        }
    }
}