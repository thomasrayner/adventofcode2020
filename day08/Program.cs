using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace day08
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = args[0];
            StreamReader inputFile = new StreamReader(inputPath);
            List<string> input = new List<string>();
            string line;
            while ((line = inputFile.ReadLine()) != null)
            {
                input.Add(line);
            }

            AdventProgram adv = new AdventProgram();
            adv.LoadProgram(input);

            HashSet<int> SeenInstructions = new HashSet<int>();
            while (true)
            {
                int acc = adv.Accumulator;
                bool notYetSeen = SeenInstructions.Add(adv.ExecuteOne().ID);

                if (!notYetSeen)
                {
                    Console.WriteLine($"Pt1: {acc}");
                    break;
                }
            }


            // pt 2
            // create permutations of input data
            List<List<string>> possibleInstructionChanges = new List<List<string>>();
            for (int i = 0; i < input.Count; i++)
            {
                List<string> tmp = new List<string>(input);
                switch (input[i].Substring(0, 3))
                {
                    case "nop":
                        possibleInstructionChanges.Add(tmp);
                        possibleInstructionChanges[^1][i] = tmp[i].Replace("nop", "jmp");
                        break;
                    case "jmp":
                        possibleInstructionChanges.Add(tmp);
                        possibleInstructionChanges[^1][i] = tmp[i].Replace("jmp", "nop");
                        break;
                    default:
                        break;
                }
            }

            // see which permutation ends the program
            for (int i = 0; i < possibleInstructionChanges.Count; i++)
            {
                List<string> pos = possibleInstructionChanges[i];
                SeenInstructions.Clear();
                AdventProgram a = new AdventProgram();
                a.LoadProgram(pos);
                while (true)
                {
                    Instruction ex = a.ExecuteOne();
                    if (a.Terminated)
                    {
                        Console.WriteLine($"Pt2: {a.Accumulator}");
                        break;
                    }

                    bool notYetSeen = SeenInstructions.Add(ex.ID);

                    if (!notYetSeen)
                    {
                        break;
                    }
                }
            }

            
        }
    }

    enum Operation
    {
        acc,    // increase program-wide accumulator var by arg amount
        jmp,    // jump by arg amount steps relative to position
        nop     // no operation, skip to next operation
    }

    class Instruction
    {
        public Operation Op { get; set; }
        public int Argument { get; set; }
        public int ID { get; set; }

        public Instruction(Operation Operation, int Arg, int Identifier)
        {
            Op = Operation;
            Argument = Arg;
            ID = Identifier;
        }
    }

    class AdventProgram
    {
        public int Accumulator = 0;
        public List<Instruction> Instructions = new List<Instruction>();
        private int Index = 0;
        public bool Terminated = false;

        public void LoadProgram(List<string> ProgramInput)
        {
            foreach (string line in ProgramInput)
            {
                Operation o = (Operation)Enum.Parse(typeof(Operation), line.Substring(0, 3), true);
                int a = int.Parse(line[4..], NumberStyles.AllowLeadingSign);
                Instructions.Add(new Instruction(o, a, Instructions.Count));
            }
        }

        public Instruction ExecuteOne()
        {
            if (Index >= Instructions.Count)
            {
                Terminated = true;
                return null;
            }

            Instruction ret = Instructions[Index];

            switch (Instructions[Index].Op)
            {
                case Operation.acc:
                    Accumulator += Instructions[Index].Argument;
                    Index++;
                    break;
                case Operation.jmp:
                    Index += Instructions[Index].Argument;
                    break;
                default:
                    Index++;
                    break;
            }

            return ret;
        }
    }
}
