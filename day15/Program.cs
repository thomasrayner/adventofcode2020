using System;
using System.Collections.Generic;
using System.Linq;

namespace day15
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = args[0];
            List<int> startingNums = input.Split(",").Select(int.Parse).ToList();
            Dictionary<int, List<int>> gameMap = new Dictionary<int, List<int>>();

            for (int i = 0; i < startingNums.Count; i++)
            {
                if (gameMap.TryGetValue(startingNums[i], out List<int> count))
                {
                    gameMap[startingNums[i]].Add(i);
                }
                else
                {
                    gameMap.Add(startingNums[i], new List<int>());
                    gameMap[startingNums[i]].Add(i);
                }
            }

            int lastSpoken = startingNums[^1];
            int add = 0;

            for (int i = startingNums.Count; i < 2020; i++)
            {
                if (gameMap.ContainsKey(lastSpoken) && gameMap[lastSpoken].Count > 1)
                {
                    add = gameMap[lastSpoken][^1] - gameMap[lastSpoken][^2];
                }
                else
                {
                    add = 0;
                }

                if (gameMap.TryGetValue(add, out List<int> count))
                {
                    gameMap[add].Add(i);
                }
                else
                {
                    gameMap.Add(add, new List<int>());
                    gameMap[add].Add(i);
                }

                lastSpoken = add;
            }

            Console.WriteLine($"Pt1: {add}");

            // pt2 is the same, just do it more times
            // just resetting and going again, so I can be done with today's puzzle before scrum

            lastSpoken = startingNums[^1];
            add = 0;
            gameMap = new Dictionary<int, List<int>>();

            for (int i = 0; i < startingNums.Count; i++)
            {
                if (gameMap.TryGetValue(startingNums[i], out List<int> count))
                {
                    gameMap[startingNums[i]].Add(i);
                }
                else
                {
                    gameMap.Add(startingNums[i], new List<int>());
                    gameMap[startingNums[i]].Add(i);
                }
            }

            for (int i = startingNums.Count; i < 30000000; i++)
            {
                if (gameMap.ContainsKey(lastSpoken) && gameMap[lastSpoken].Count > 1)
                {
                    add = gameMap[lastSpoken][^1] - gameMap[lastSpoken][^2];
                }
                else
                {
                    add = 0;
                }

                if (gameMap.TryGetValue(add, out List<int> count))
                {
                    gameMap[add].Add(i);
                }
                else
                {
                    gameMap.Add(add, new List<int>());
                    gameMap[add].Add(i);
                }

                lastSpoken = add;
            }

            Console.WriteLine($"Pt2: {add}"); 
        }
    }
}
