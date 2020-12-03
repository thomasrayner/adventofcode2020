using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day03
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

            // pt1

            int width = input[0].Length;
            int height = input.Count;
            int[] slope = new int[] {3, 1}; // from question
            int x = 0;
            int treeCount = 0;

            for (int y = 0; y < height; y += slope[1])
            {
                if (input[y][x] == '#') treeCount++;
                x = (x + slope[0]) % width;
            }

            Console.WriteLine($"Pt1: {treeCount}");

            // pt2

            List<int[]> slopes = new List<int[]>() {
                new int[] {1, 1},
                slope,
                new int[] {5, 1},
                new int[] {7, 1},
                new int[] {1, 2}};
            List<Int64> treeCounts = new List<Int64>();

            foreach (int[] s in slopes)
            {
                int tempCount = 0;
                x = 0;
                for (int y = 0; y < height; y += s[1])
                {
                    if (input[y][x] == '#') tempCount++;
                    x = (x + s[0]) % width;
                }
                treeCounts.Add(tempCount);
            }

            Console.WriteLine($"Pt2: {(Int64)(treeCounts.Aggregate((a, x) => a * x))}");

            // ran out of time to optimize
        }
    }
}
