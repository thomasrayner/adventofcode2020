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

            int[] slope = new int[] { 3, 1 }; // from question
            long treeCount = Ski(input, slope);

            Console.WriteLine($"Pt1: {treeCount}");

            // pt2

            List<int[]> slopes = new List<int[]>() {
                new int[] {1, 1},
                slope,
                new int[] {5, 1},
                new int[] {7, 1},
                new int[] {1, 2}}; // from question
            List<long> treeCounts = new List<long>();

            slopes.ForEach(i => treeCounts.Add(Ski(input, i)));
            long treeCount2 = treeCounts.Aggregate((a, x) => a * x);
            Console.WriteLine($"Pt2: {treeCount2}");
        }

        static long Ski(List<string> Course, int[] slope)
        {
            int width = Course[0].Length;
            int height = Course.Count;
            int x = 0;
            int treeCount = 0;

            for (int y = 0; y < height; y += slope[1])
            {
                if (Course[y][x] == '#') treeCount++;
                x = (x + slope[0]) % width;
            }

            return treeCount;
        }
    }
}
