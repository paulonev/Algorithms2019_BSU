using System.Linq;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace TuringMachines
{
    public abstract class Machine
    {
        public Dictionary<string, Dictionary<char, Move>> ShiftTable { get; set; }

        public Machine()
        {
            ShiftTable = new Dictionary<string, Dictionary<char, Move>>();
        }

        public virtual void AddOperation(string state, string character, string[] operation)
        {}
    }

    public class TuringMachine : Machine
    {
        public override void AddOperation(string state, string character, string[] operation)
        {
            var nextState = operation[0];
            var symbInstead = operation[1].ToCharArray()[0];
            int.TryParse(operation[2], out var shift);
            Move mv = new Move(nextState: nextState, symb: symbInstead, shift: shift);
            
            if(!ShiftTable.ContainsKey(state))
            {
                ShiftTable.Add(state, new Dictionary<char, Move>
                {
                    {character.ToCharArray()[0], mv}
                });
            } 
            else ShiftTable[state].Add(character.ToCharArray()[0], mv);
        }
    }

    public class MachineBuilder
    {
        public static TuringMachine BuildMachine(string filePath)
        {
            var machine = new TuringMachine();
            // using (var fileReader = new StreamReader(filePath))
                
            if(File.Exists(filePath))
            {
                var linesFiltered = from line in File.ReadLines(filePath)
                                    where !(line.StartsWith("#") || line.Length == 0) 
                                    select line;
                
                char[] separators = {',', ':', ' '};
                foreach (var line in linesFiltered)
                {
                    var elems = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    machine.AddOperation(elems[0], elems[1], elems[2..]);
                }
            }
            else
            {
                throw new FileNotFoundException($"File by filepath: {filePath} wasn't found...");
            }
            return machine;
        }
    }
}