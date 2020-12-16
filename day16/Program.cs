using System;
using System.Collections.Generic;

namespace day16
{
    class Field
    {
        public string Name;
        public int[] Range1;
        public int[] Range2;
        public List<int> possibleIndex = new List<int>();
        public Field(string name, int[] range1, int[] range2)
        {
            Name = name;
            Range1 = range1;
            Range2 = range2;
            for (int i = 0; i < 20; i++)
            {
                possibleIndex.Add(i);
            }
        }
    }
    public static class Day16
    {
        // static string[] input = Read.File("day16.txt");
        static string[] input = System.IO.File.ReadAllLines("input.txt");

        static HashSet<int> validNumber = new HashSet<int>();
        static List<Field> fields = new List<Field>();
        static List<int[]> tickets = new List<int[]>();
        static List<int[]> validTickets = new List<int[]>();
        public static void Main()
        {
            ParseInput();
            Console.WriteLine($"Pt1: {Part1()}");
            Console.WriteLine($"Pt2: {Part2()}");
        }

        public static void ParseInput()
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (i < 20)
                {
                    string[] fieldSplit = input[i].Split(new string[] { "-", ": ", " or " }, StringSplitOptions.RemoveEmptyEntries);
                    int range1Low = int.Parse(fieldSplit[1]), range1High = int.Parse(fieldSplit[2]);
                    int range2Low = int.Parse(fieldSplit[3]), range2High = int.Parse(fieldSplit[4]);
                    for (int x = range1Low; x <= range1High; x++)
                    {
                        if (!validNumber.Contains(x))
                            validNumber.Add(x);
                    }
                    for (int x = range2Low; x <= range2High; x++)
                    {
                        if (!validNumber.Contains(x))
                            validNumber.Add(x);
                    }
                    fields.Add(new Field(fieldSplit[0], new int[] { range1Low, range1High }, new int[] { range2Low, range2High }));
                }
                else if (i >= 25 || i == 22)
                {
                    string[] ticketNumbers = input[i].Split(',');
                    int[] ticket = new int[ticketNumbers.Length];
                    for (int t = 0; t < ticketNumbers.Length; t++)
                        ticket[t] = int.Parse(ticketNumbers[t]);
                    tickets.Add(ticket);
                }
            }
        }

        public static int Part1()
        {
            int part1 = 0;
            bool isValid;
            foreach (int[] ticket in tickets)
            {
                isValid = true;
                foreach (int n in ticket)
                {
                    if (!validNumber.Contains(n))
                    {
                        part1 += n;
                        isValid = false;
                    }
                }
                if (isValid)
                    validTickets.Add(ticket);
            }
            return part1;
        }

        public static long Part2()
        {
            long part2 = 1;
            foreach (Field f in fields)
            {
                for (int tNum = 0; tNum < validTickets[0].Length; tNum++)
                {
                    for (int ticket = 0; ticket < validTickets.Count; ticket++)
                    {
                        if ((validTickets[ticket][tNum] >= f.Range1[0] && validTickets[ticket][tNum] <= f.Range1[1]) ||
                            (validTickets[ticket][tNum] >= f.Range2[0] && validTickets[ticket][tNum] <= f.Range2[1]))
                        {
                            continue;
                        }
                        else
                        {
                            f.possibleIndex.Remove(tNum);
                            break;
                        }
                    }
                }
            }
            int fieldCound = fields.Count, count;
            do
            {
                count = 0;
                foreach (Field f in fields)
                {
                    if (f.possibleIndex.Count == 1)
                    {
                        foreach (Field fi in fields)
                        {
                            if (fi != f)
                            {
                                fi.possibleIndex.Remove(f.possibleIndex[0]);
                            }
                        }
                    }
                    count += f.possibleIndex.Count;
                }
            } while (count != fieldCound);
            foreach (Field f in fields)
            {
                if (f.Name.Contains("departure"))
                    part2 *= validTickets[0][f.possibleIndex[0]];
            }
            return part2;
        }
    }
}
