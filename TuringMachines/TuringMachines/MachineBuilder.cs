using System.Linq;
using System.IO;
using System;
using System.Collections.Generic;

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
        
        public virtual void AddOperation(int[] prefixTable)
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

    public class KmpAutomate : Machine
    {
        /// <summary>
        /// Matching pattern
        /// </summary>
        public string Pattern { get; }

        public KmpAutomate(string pattern)
        {
            Pattern = pattern;
        }
        
        public override void AddOperation(int[] prefixTable)
        {
            // Add for first symbol in pattern
            ShiftTable.Add("0", new Dictionary<char, Move>
            {
                {Pattern[0], new Move(nextState:"1", symb:Pattern[0], shift:1)}
            });
            ShiftTable["0"].Add('$', new Move(nextState:"0", symb:'$', shift:1));

            
            for (int i = 1; i < prefixTable.Length; i++)
            {
                string state = i.ToString();
                var patChar = Pattern[i];
                if (!ShiftTable.ContainsKey(state))
                {
                    if (i == prefixTable.Length - 1)
                    {
                        ShiftTable.Add(state, new Dictionary<char, Move>
                        {
                            {patChar, new Move(nextState:"y", symb:patChar, shift:1)}
                        });     
                    }
                    else
                    {
                        ShiftTable.Add(state, new Dictionary<char, Move>
                        {
                            {patChar, new Move(nextState:(i+1).ToString(), symb:patChar, shift:1)}
                        });    
                    }
                }
                ShiftTable[state].Add('$', new Move(nextState:prefixTable[i-1].ToString(), symb:'$', shift:0));
            }   
            
            
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