using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

namespace TuringMachines
{
    static class Program
    {
        public static void Main(string[] args)
        {
//            for running Task17() and Task28() - "old" Turing Automates
//            while (true)
//            {
//                Console.WriteLine($"Output {Task17()}");
//                Console.WriteLine($"Output {Task28()}");
//                return;
//            }

//            string text = "adsgwadsxdsgwadsgz";
//            string pattern = "music";
            
            using StreamReader sr =
                new StreamReader("/home/paul/coding/algorithms-data-structures/TuringMachines/TuringMachines/data/worldandpeace_oneline.txt");
            string text = sr.ReadLine()?.Trim();

            var machine = new KmpAutomate(pattern: args[0]);
            machine.AddOperation(prefixTable: machine.Pattern.PrefixSuffix());
            List<string> endStates = new List<string>{"y", "n"};
            
            var answer = MachineRunner.RunPatternSearch(
                text: text, 
                machine: machine, 
                pattern: machine.Pattern,
                endStates: endStates,
                log: true);

            Console.WriteLine($"{machine.Pattern} found in {answer.Count} places");
            Console.Write($"Indexes of occurence: ");
            foreach (var item in answer)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        public static string Task17()
        {
            Console.WriteLine("Task 1.7, alphabet {a,b,c,#} - In odd word delete all symbols except for the middle");
            
            string input = Console.ReadLine();
            int headPos = GetHeadPos(ref input);
            
            var machine =
                MachineBuilder.BuildMachine(
                    "/home/paul/coding/algorithms-data-structures/TuringMachines/TuringMachines/17.txt");
            return MachineRunner.Run(input, machine,
                endProgramStates: new List<string> {"e"}, head: headPos, log: true);
        }

        public static string Task28()
        {
            Console.WriteLine("Task 2.8, alphabet {a,b,c,#} - Insert symbol {c} after the first symbol of input");
            
            string input = Console.ReadLine();
            int headPos = GetHeadPos(ref input);
            
            var machine =
                MachineBuilder.BuildMachine(
                    "/home/paul/coding/algorithms-data-structures/TuringMachines/TuringMachines/28.txt");

            return MachineRunner.Run(input, machine,
                endProgramStates: new List<string> {"e"}, head: headPos, log: true);
        }
        private static int GetHeadPos(ref string input)
        {
            char temp = input[0];
            StringBuilder sb = new StringBuilder(input);
            input = sb.Insert(sb.Length, "##").Insert(0, "##").ToString();
            return input.IndexOf(temp);
        }
    }
}
