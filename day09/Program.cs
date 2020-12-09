using System;
using System.Collections.Generic;
using System.IO;

namespace day09
{
    class Program
    {

        static void Main(string[] args)
        {
            string inputPath = args[0];
            StreamReader inputFile = new StreamReader(inputPath);
            List<long> input = new List<long>();
            string line;
            while ((line = inputFile.ReadLine()) != null)
            {
                input.Add(long.Parse(line));
            }

            long pt2Target = 0;
            for (int i = 25; i < input.Count; i++) // magic number 25 given in problem description
            {
                List<long> seen = new List<long>();
                bool good = false;
                for (int j = i - 25; j < i; j++)
                {
                    // if input[i] minus one of the prev 25 numbers equals one of the other prev 25 numbers
                    // then those two prev 25 numbers add up to input[i], which means input[i] is good
                    if (seen.Contains(input[i] - input[j]))
                    {
                        good = true;
                        break;
                    }

                    // if input[i] - input[j] doesn't equal a number in the prev 25 that we've already checked
                    // then we need to remember it, and see if input[i] - another number in the prev 25 is
                    // equal to input[j]
                    seen.Add(input[j]);
                }

                if (!good)
                {
                    Console.WriteLine($"Pt1: {input[i]}");
                    pt2Target = input[i];   // the target for pt2 is the first "corrupt" number
                    break;
                }
            }

            bool foundPt2 = false;
            for (int i = 0; i < input.Count; i++)
            {
                MapOb map = new MapOb(input[i], input[i], input[i]);

                while (!foundPt2 && map.Sum < pt2Target)
                {
                    for (int j = i + 1; j < input.Count; j++)
                    {
                        // keep adding numbers to the running sum starting at input[i] until we either
                        // hit the target we're looking for, or exceed it
                        map.Sum += input[j];
                        map.Min = Math.Min(map.Min, input[j]);
                        map.Max = Math.Max(map.Max, input[j]);

                        if (map.Sum == pt2Target)
                        {
                            Console.WriteLine($"Pt2: {map.Min + map.Max}");
                            foundPt2 = true;
                            break;
                        }
                        
                        else if (map.Sum > pt2Target) break;
                    }
                }
            }
        }
    }

    class MapOb
    {
        public long Sum { get; set; }
        public long Min { get; set; }
        public long Max { get; set; }
        public MapOb(long sum, long min, long max)
        {
            Sum = sum;
            Min = min;
            Max = max;
        }
    }
}
