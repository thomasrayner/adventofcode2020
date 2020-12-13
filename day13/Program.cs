using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day13
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

            int target = int.Parse(input[0]);
            List<int> busIds = new List<int>();
            int smallestDiff = int.MaxValue;
            int myBusId = 0;

            foreach (string s in input[1].Split(","))
            {
                bool parse = int.TryParse(s, out int num);
                if (parse) busIds.Add(num);
            }

            foreach (int bus in busIds)
            {
                double closest = Math.Ceiling((double)((double)target / (double)bus)) * bus;
                double diff = closest - target;

                if (diff < smallestDiff)
                {
                    smallestDiff = Convert.ToInt32(diff);
                    myBusId = bus;
                }
            }

            Console.WriteLine($"Pt1: {myBusId * smallestDiff}");
            // https://www.reddit.com/r/adventofcode/comments/kc4njx/2020_day_13_solutions/gfnljct

            string[] busIdStrings = input[1].Split(',', StringSplitOptions.RemoveEmptyEntries).ToArray();
            busIds = busIdStrings.Where(s => s != "x").Select(int.Parse).ToList();
            List<int> busIdOffsets = new(busIds.Count);

            for (int i = 0; i < busIdStrings.Length; i++)
            {
                if (busIdStrings[i] != "x")
                {
                    busIdOffsets.Add(i % busIds[busIdOffsets.Count]);
                }
            }

            long time = 0;
            long advanceBy = busIds[0];
            int correctBusIndex = 0;
            while (true)
            {
                bool found = true;
                for (int i = correctBusIndex + 1; i < busIds.Count; i++)
                {
                    int busId = busIds[i];
                    if (TimeLeft(busId, time) == busIdOffsets[i])
                    {
                        advanceBy *= busId;
                        correctBusIndex = i;
                    }
                    else
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
                time += advanceBy;
            }

            long answer = time;
            Console.WriteLine($"Pt2: {answer}");
        }

        static int TimeLeft(int busId, long time)
        {
            int timeLeft = (int)(time % busId);
            if (timeLeft > 0)
            {
                timeLeft = busId - timeLeft;
            }
            return timeLeft;
        }
    }    
}
