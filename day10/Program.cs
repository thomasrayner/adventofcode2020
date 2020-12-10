using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day10
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = args[0];
            StreamReader inputFile = new StreamReader(inputPath);
            List<int> input = new List<int>();
            string line;
            while ((line = inputFile.ReadLine()) != null)
            {
                input.Add(int.Parse(line));
            }

            Dictionary<int, int> diffMap = new Dictionary<int, int>();
            input.Add(0);
            input.Sort();
            input.Add(input[^1] + 3);

            for (int i = 1; i < input.Count; i++)
            {
                int diff = input[i] - input[i - 1];
                if (diffMap.TryGetValue(diff, out int count))
                {
                    diffMap[diff]++;
                }
                else
                {
                    diffMap.Add(diff, 1);
                }
            }

            Console.WriteLine($"Pt1: {diffMap[1] * diffMap[3]}");

            Dictionary<int, long> combinations = new Dictionary<int, long> { { 0, 1 } };
            for (int i = 1; i <= input[^1]; i++)
            {
                if (input.Contains(i) || i == input[^1])
                {
                    long num = 0;
                    if (combinations.Keys.Contains(i - 3)) num += combinations[i - 3];
                    if (combinations.Keys.Contains(i - 2)) num += combinations[i - 2];
                    if (combinations.Keys.Contains(i - 1)) num += combinations[i - 1];
                    combinations.Add(i, num);
                }
            }

            Console.WriteLine($"Pt2: {combinations[input[^1]]}");
        }
    }
}
