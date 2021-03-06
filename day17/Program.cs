﻿// I got way too lost thinking about recursion, and today turned into a learning
// experience once I looked at other solutions: https://github.com/viceroypenguin/adventofcode/blob/master/2020/day17.original.cs

using System;
using System.Collections.Generic;
using System.Linq;

namespace day17
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputStrings = System.IO.File.ReadAllLines(args[0]);

            Console.WriteLine($"Pt1: {Part1(inputStrings)}");
            Console.WriteLine($"Pt2: {Part2(inputStrings)}");
        }

        static int Part1(string[] Input)
        {
            var state = new Dictionary<(int x, int y, int z), bool>(1024);
            int _x = 0, _y = -1;

            foreach (var l in Input)
            {
                _x = 0;
                _y++;

                for (int i = 0; i < l.Length; i++) state[(_x++, _y, 0)] = l[i] == '#';
            }

            var count = new Dictionary<(int x, int y, int z), int>(1024);
            var dirs = Enumerable.Range(-1, 3)
                .SelectMany(x => Enumerable.Range(-1, 3)
                    .SelectMany(y => Enumerable.Range(-1, 3)
                        .Select(z => (x, y, z))))
                .Where(d => d != (0, 0, 0))
                .ToArray();

            for (int i = 0; i < 6; i++)
            {
                count.Clear();

                foreach (var p in state.Keys) count[p] = 0;

                foreach (var ((x, y, z), alive) in state.Where(kvp => kvp.Value))
                {
                    foreach (var (dx, dy, dz) in dirs)
                    {
                        count[(x + dx, y + dy, z + dz)] = count.GetValueOrDefault((x + dx, y + dy, z + dz)) + 1;
                    }
                }

                foreach (var (p, c) in count)
                {
                    state[p] = (state.GetValueOrDefault(p), c) switch
                    {
                        (true, >= 2 and <= 3) => true,
                        (false, 3) => true,
                        _ => false,
                    };
                }
            }

            return state.Where(k => k.Value).Count();
        }

        static int Part2(string[] Input)
        {
            var state = new Dictionary<(int x, int y, int z, int w), bool>(8192);
            int _x = 0, _y = -1;

            foreach (var l in Input)
            {
                _x = 0;
                _y++;

                for (int i = 0; i < l.Length; i++) state[(_x++, _y, 0, 0)] = l[i] == '#';
            }

            var count = new Dictionary<(int x, int y, int z, int w), int>(8192);
            var dirs = Enumerable.Range(-1, 3)
                .SelectMany(x => Enumerable.Range(-1, 3)
                    .SelectMany(y => Enumerable.Range(-1, 3)
                        .SelectMany(z => Enumerable.Range(-1, 3)
                            .Select(w => (x, y, z, w)))))
                .Where(d => d != (0, 0, 0, 0))
                .ToArray();

            for (int i = 0; i < 6; i++)
            {
                count.Clear();

                foreach (var p in state.Keys) count[p] = 0;

                foreach (var ((x, y, z, w), alive) in state.Where(kvp => kvp.Value))
                {
                    foreach (var (dx, dy, dz, dw) in dirs)
                    {
                        count[(x + dx, y + dy, z + dz, w + dw)] = count.GetValueOrDefault((x + dx, y + dy, z + dz, w + dw)) + 1;
                    }
                }

                foreach (var (p, c) in count)
                {
                    state[p] = (state.GetValueOrDefault(p), c) switch
                    {
                        (true, >= 2 and <= 3) => true,
                        (false, 3) => true,
                        _ => false,
                    };
                }
            }

            return state.Where(k => k.Value).Count();
        }
    }
}
