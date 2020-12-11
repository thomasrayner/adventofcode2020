// had my own solution, but ran out of time and borrowed/re-factored heavily based on 
// https://github.com/hlim29/AdventOfCode2020/blob/master/Days/DayEleven.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day11
{
    class Program
    {
        public static readonly List<(int x, int y)> directions = new List<(int X, int Y)> { (-1, 0), (-1, -1), (0, -1), (1, -1), (1, 0), (1, 1), (0, 1), (-1, 1) };
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

            Dictionary<(int x, int y), char> map = new Dictionary<(int x, int y), char>();

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    map.Add((j, i), input[i][j]);
                }
            }

            HashSet<(int x, int y)> keys = new HashSet<(int x, int y)>(map.Keys);

            Console.WriteLine($"Pt1: {Check(map, keys, 4, false)}");
            Console.WriteLine($"Pt2: {Check(map, keys, 5, true)}");
        }

        static int Check(Dictionary<(int x, int y), char> Map, HashSet<(int x, int y)> Keys, int Tolerance, bool Visible)
        {
            var m = new Dictionary<(int x, int y), char>(Map);
            var count = new List<int> { CountOccupied(m) };

            while (count.Count < 3 || count[^1] != count[^2])
            {
                count.Add(CountOccupied(m));
                m = Occupy(m, Tolerance, Keys, Visible);
            }

            return CountOccupied(m);
        }

        static Dictionary<(int x, int y), char> Occupy(Dictionary<(int x, int y), char> Map, int Tolerance, HashSet<(int x, int y)> Keys, bool Visible)
        {
            var newM = new Dictionary<(int x, int y), char>();

            foreach (var item in Keys)
            {
                var neighbors = Visible
                    ? GetVisibleNeighbors(item, Map, Keys)
                    : directions.Select(x => (x.x + item.x, x.y + item.y))
                        .Where(x => Map.TryGetValue((x.Item1, x.Item2), out var value))
                        .Select(x => Map[x])
                        .ToList();
                var occupiedNeighbors = neighbors.Where(x => x == '#').ToList();

                if (Map[item] == 'L' && occupiedNeighbors.Count == 0)
                {
                    newM[item] = '#';
                }
                else if (Map[item] == '#' && occupiedNeighbors.Count >= Tolerance)
                {
                    newM[item] = 'L';
                }
                else
                {
                    newM[item] = Map[item];
                }
            }

            return newM;
        }

        static int CountOccupied(Dictionary<(int x, int y), char> Map)
        {
            return Map.Values.Count(x => x == '#');
        }

        static List<char> GetVisibleNeighbors((int x, int y) Current, Dictionary<(int x, int y), char> Map, HashSet<(int x, int y)> Keys)
        {
            var neighbors = new List<char>();

            foreach (var (x, y) in directions)
            {
                int mul = 1;
                var squ = (x: x * mul + Current.x, y: y * mul + Current.y);
                
                while (Keys.Contains(squ))
                {
                    if (Map[squ] != '.')
                    {
                        neighbors.Add(Map[squ]);
                        break;
                    }
                    mul++;
                    squ = (x: x * mul + Current.x, y: y * mul + Current.y);
                }
            }

            return neighbors;
        }
    }
}
