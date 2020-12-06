using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day06
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = args[0];
            StreamReader inputFile = new StreamReader(inputPath);
            List<string> lines = new List<string>();
            int pt1 = 0;
            int pt2 = 0;

            while (true)
            {
                string line = inputFile.ReadLine();

                if (string.IsNullOrEmpty(line))
                {
                    if (lines.Count > 0)
                    {
                        pt1 += CountAnswer1(lines);
                        pt2 += CountAnswer2(lines);
                        lines.Clear();
                        continue;
                    }

                    break;
                }

                lines.Add(line);
            }

            Console.WriteLine($"Pt1: {pt1}");
            Console.WriteLine($"Pt2: {pt2}");
        }

        public static int CountAnswer1(List<string> RawAnswers)
        {
            HashSet<char> answers = new HashSet<char>();

            foreach (string s in RawAnswers)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    answers.Add(s[i]);
                }
            }

            return answers.Count;
        }

        public static int CountAnswer2(List<string> RawAnswers)
        {
            Dictionary<char, int> answers = new Dictionary<char, int>();
            int groupMembers = RawAnswers.Count;

            foreach (string s in RawAnswers)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (answers.TryGetValue(s[i], out int count))
                    {
                        answers[s[i]] = count + 1;
                    }
                    else
                    {
                        {
                            answers.Add(s[i], 1);
                        }
                    }
                }
            }

            return answers.Where(x => x.Value == groupMembers).Count();
        }
    }
}
