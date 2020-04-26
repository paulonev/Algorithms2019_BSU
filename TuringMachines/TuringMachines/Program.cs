using System.Collections.Generic;
using System;
using System.Text;

namespace TuringMachines
{
    static class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine($"Output {Task17()}");
                Console.WriteLine($"Output {Task28()}");
                return;
            }
            
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