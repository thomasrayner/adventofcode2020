using System;
using System.Collections.Generic;
using System.IO;

namespace day01
{
    class Program
    {
        static void Main(string[] args)
        {
            // day 01 read input
            string inputPath = args[0];
            StreamReader inputFile = new StreamReader(inputPath);
            List<int> input = new List<int>();
            string line;
            bool found = false;

            while ((line = inputFile.ReadLine()) != null)
            {
                input.Add(Convert.ToInt32(line));
            }

            // pt 1

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = i + 1; j < input.Count; j++)
                {
                    if (input[i] + input[j] == 2020)
                    {
                        Console.WriteLine($"Pt 1: {input[i] * input[j]}");
                        found = true;
                        break;
                    }

                    if (found) break;
                }
            }

            // pt 2
            found = false;

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = i + 1; j < input.Count; j++)
                {
                    for (int k = j + 1; k < input.Count; k++)
                    {
                        if (input[i] + input[j] + input[k] == 2020)
                        {
                            Console.WriteLine($"Pt 2: {input[i] * input[j] * input[k]}");
                            found = true;
                            break;
                        }

                        if (found) break;
                    }

                    if (found) break;
                }
            }
        }
    }
}
