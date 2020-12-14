// Was looking up the bitwise operations, and this ended up being almost exactly what I was
// going to do... but this code is largely from https://github.com/DanaL/AdventOfCode/blob/master/2020/Day14.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day14
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = args[0];
            StreamReader inputFile = new StreamReader(inputPath);
            string line;

            Dictionary<long, long> memory = new Dictionary<long, long>();
            (long Or, long And) mask = (0, 0);

            while ((line = inputFile.ReadLine()) != null)
            {
                if (line.StartsWith("mask"))
                {
                    string m = line.Split(" = ")[1];
                    mask = (
                        Or: Convert.ToInt64(m.Replace("X", "0"), 2),
                        And: Convert.ToInt64(m.Replace("X", "1"), 2)
                    );
                }
                else
                {
                    var pieces = Regex.Match(line, @"mem\[(?<p0>\d+)\] = (?<p1>\d+)");
                    var a = (Loc: long.Parse(pieces.Groups["p0"].Value),
                        Val: long.Parse(pieces.Groups["p1"].Value));
                    a.Val &= mask.And;
                    a.Val |= mask.Or;
                    memory[a.Loc] = a.Val;
                }
            }

            Console.WriteLine($"Pt1: {memory.Values.Sum()}");

            inputFile.Close();
            inputFile = new StreamReader(inputPath);
            memory = new Dictionary<long, long> ();
            string pt2Mask = "";

            while ((line = inputFile.ReadLine()) != null)
            {
                if (line.StartsWith("mask"))
                {
                    pt2Mask = line.Split(" = ")[1];
                }
                else
                {
                    var pieces = Regex.Match(line, @"mem\[(?<p0>\d+)\] = (?<p1>\d+)");
                    var a = (Loc: long.Parse(pieces.Groups["p0"].Value),
                        Val: long.Parse(pieces.Groups["p1"].Value));

                    long or = Convert.ToInt64(pt2Mask.Replace('X', '0'), 2);
                    a.Loc |= or;

                    var loc = Convert.ToString(a.Loc, 2).PadLeft(36, '0').ToCharArray();
                    var revMask = string.Concat(pt2Mask.ToCharArray().Reverse());

                    for (int j = 0; j < revMask.Length; j++)
                    {
                        if (revMask[j] == 'X') loc[^(j + 1)] = 'X';
                    }

                    writeToMem(string.Concat(loc), a.Val, memory);
                }
            }

            Console.WriteLine($"Pt2: {memory.Values.Sum()}");
        }

        static void writeToMem(string loc, long val, Dictionary<long, long> mem)
        {
            var xes = loc.Select((c, i) => c == 'X' ? i : -1).Where(n => n > -1).ToArray();

            if (xes.Length == 0)
            {
                mem[Convert.ToInt64(string.Concat(loc), 2)] = val;
            }
            else
            {
                var floating = loc.ToCharArray();
                floating[xes[0]] = '1';
                writeToMem(string.Concat(floating), val, mem);
                floating[xes[0]] = '0';
                writeToMem(string.Concat(floating), val, mem);
            }
        }
    }
}
