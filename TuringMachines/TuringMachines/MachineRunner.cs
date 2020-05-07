using System;
using System.Collections.Generic;
using System.Text;

namespace TuringMachines
{
    public class Move
    {
	    public string NextState { get; set; }
        public char ReplaceWith { get; set; }
        public int Shift { get; set; }

        public Move(string nextState, char symb, int shift)
        {
            Shift = shift;
            ReplaceWith = symb;
            NextState = nextState;
        }
    }
    
    public class MachineRunner
    {

        public static List<int> RunPatternSearch(string text, Machine machine, string pattern,
            List<string> endStates, bool log, int head = 0)
        {
            int patLen = pattern.Length;
            int pos = head;
            string state = "0";
            var matchIndexes = new List<int>();
            
            while (true)
            {
                if (pos == text.Length)
                {
                    if (state.Equals("y"))
                        matchIndexes.Add(pos - patLen);
                    return matchIndexes;
                }
                
                if (endStates.Contains(state))
                {
                    matchIndexes.Add(pos - patLen); // found a match of pattern and added index of occurence
                    state = "0";
                }
                
                char curSymbol = text[pos];
                var mv = machine.ShiftTable[state].ContainsKey(curSymbol) 
                    ? machine.ShiftTable[state][text[pos]] 
                    : machine.ShiftTable[state]['$'];
                
                if (log)
                {
                    Console.WriteLine($"--- head on: {pos} -> nextState: {mv.NextState}, move: {mv.Shift}");    
                }
                pos += mv.Shift;
                state = mv.NextState;
            }
        }
        
        
        public static string Run(string input, Machine machine, 
                        List<string> endProgramStates, bool log, int head = 0)
        {
            StringBuilder sb = new StringBuilder(input);
            Console.WriteLine($"Input word: {sb}");
            Move mv = new Move(nextState: "0", symb:'b', shift: 0);
            int pos = head; //position of head
            while(true)
            {
                string state = mv.NextState;
                if (log)
                {
                    Console.Write($"State {state} ");
                }
                if(endProgramStates.Contains(state))
                {
                    Console.WriteLine("--- Machine terminated.");
                    return sb.ToString();
                }

                mv = machine.ShiftTable[state][sb[pos]];
                if (log)
                {
                    Console.WriteLine($"--- head on: {pos}, {sb[pos]} -> {mv.ReplaceWith}, move: {mv.Shift}, nextState: {mv.NextState}");    
                }
                sb[pos]= mv.ReplaceWith;
                pos += mv.Shift;
            }
        }        

        

        /*
        prerequisites of solving problems using turing machine:
        1) finite state set ( including initial and end )
        2) finite alphabet ( literals, chars, blank symbol )
        3) instructions, using which machine produces some output tape on a given input tape

        {
            "state 1":
            {
                when read symbol "s" : (switch to state #, write symbol "w", move head(-1,0,1))
                "symbol" : "move"
                ...
            },
            ...,
            {}
        }
        */
    }
}