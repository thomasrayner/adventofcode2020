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

            Console.WriteLine($"Pt1: {RunGame(startingNums, 2020)}");
            Console.WriteLine($"Pt2: {RunGame(startingNums, 30000000)}"); 
        }

        static void AddToGameMap(Dictionary<int, List<int>> Map, int Key, int Value)
        {
            if (Map.TryGetValue(Key, out _))
            {
                Map[Key].Add(Value);
            }
            else
            {
                Map.Add(Key, new List<int>());
                Map[Key].Add(Value);
            }
        }

        static int RunGame(List<int> StartingNumbers, int Turns)
        {
            Dictionary<int, List<int>> gameMap = new Dictionary<int, List<int>>();

            for (int i = 0; i < StartingNumbers.Count; i++)
            {
                AddToGameMap(gameMap, StartingNumbers[i], i);
            }

            int lastSpoken = StartingNumbers[^1];
            int add = 0;

            for (int i = StartingNumbers.Count; i < Turns; i++)
            {
                if (gameMap.ContainsKey(lastSpoken) && gameMap[lastSpoken].Count > 1)
                {
                    add = gameMap[lastSpoken][^1] - gameMap[lastSpoken][^2];
                }
                else
                {
                    add = 0;
                }

                AddToGameMap(gameMap, add, i);
                lastSpoken = add;
            }

            return add;
        }
    }
}
