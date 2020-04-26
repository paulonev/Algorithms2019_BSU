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
    };

    public class MachineRunner
    {
        //run instructions "machine" on the given input word
        public static string Run(string input, TuringMachine machine, 
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