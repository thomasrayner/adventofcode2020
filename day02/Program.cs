using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day02
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = args[0];
            StreamReader inputFile = new StreamReader(inputPath);
            List<Password> input = new List<Password>();
            string line;
            while ((line = inputFile.ReadLine()) != null)
            {
                input.Add(new Password(line));
            }

            int validCount1 = input.Where(x => x.Part1() == true).Count();
            Console.WriteLine($"Pt 1: {validCount1}");

            int validCount2 = input.Where(x => x.Part2() == true).Count();
            Console.WriteLine($"Pt 2: {validCount2}");
        }
    }

    class Password
    {
        public Password(string RawData)
        {
            string[] elements = Regex.Split(RawData, @"\W");
            Min = Convert.ToInt32(elements[0]);
            Max = Convert.ToInt32(elements[1]);
            Character = elements[2].ToCharArray()[0];
            PasswordValue = elements[4];
        }

        public int Min { get; set; }
        public int Max { get; set; }
        public char Character { get; set; }
        public string PasswordValue { get; set; }

        public bool Part1()
        {
            int count = 0;
            for (int i = 0; i < PasswordValue.Length; i++)
            {
                if (PasswordValue[i] == Character) count++;
            }

            return count >= Min && count <= Max;
        }

        public bool Part2()
        {
            return (PasswordValue[Min - 1] == Character) ^ (PasswordValue[Max - 1] == Character);
        }
    }
}
