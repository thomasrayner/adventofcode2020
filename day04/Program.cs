using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace day04
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = args[0];
            StreamReader inputFile = new StreamReader(inputPath);
            List<string> lines = new List<string>();
            List<Passport> passports1 = new List<Passport>();
            List<Passport> passports2 = new List<Passport>();

            while (true)
            {
                string line = inputFile.ReadLine();

                if (string.IsNullOrEmpty(line))
                {
                    if (lines.Count > 0)
                    {
                        Passport pass = new Passport(lines);
                        if (pass.ValidatePassport1()) passports1.Add(pass);
                        if (pass.ValidatePassport2()) passports2.Add(pass);
                        lines.Clear();
                        continue;
                    }

                    break;
                }

                lines.Add(line);
            }

            Console.WriteLine($"Pt1: {passports1.Count}");
            Console.WriteLine($"Pt2: {passports2.Count}");
        }
    }

    class Passport
    {
        public int byr { get; set; } // birth year
        public int iyr { get; set; } // issue year
        public int eyr { get; set; } // expiration year
        public string hgt { get; set; } // height
        public string hcl { get; set; } // hair color
        public string ecl { get; set; } // eye color
        public string pid { get; set; } // passport id
        public string cid { get; set; } // country id

        private readonly string[] _validEcl = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

        public Passport(List<string> RawStrings)
        {
            List<string> elements = new List<string>();
            foreach (string s in RawStrings)
            {
                foreach (string i in s.Split(" "))
                {
                    elements.Add(i);
                }
            }

            foreach (string e in elements)
            {
                string[] kvp = e.Split(":");
                switch (kvp[0])
                {
                    case "byr":
                        byr = Convert.ToInt32(kvp[1]);
                        break;
                    case "iyr":
                        iyr = Convert.ToInt32(kvp[1]);
                        break;
                    case "eyr":
                        eyr = Convert.ToInt32(kvp[1]);
                        break;
                    case "hgt":
                        hgt = kvp[1];
                        break;
                    case "hcl":
                        hcl = kvp[1];
                        break;
                    case "ecl":
                        ecl = kvp[1];
                        break;
                    case "pid":
                        pid = kvp[1];
                        break;
                    case "cid":
                        cid = kvp[1];
                        break;
                    default:
                        throw new ArgumentException($"KVP {e} was invalid somehow");
                }
            }
        }
        public bool ValidatePassport1()
        {
            foreach (PropertyInfo propertyInfo in this.GetType().GetProperties())
            {
                if (propertyInfo.Name == "cid") continue;

                // pt 1 just wants them all to have a value except cid
                if (propertyInfo.PropertyType == typeof(string) && propertyInfo.GetValue(this) == null) return false;
                if (propertyInfo.PropertyType == typeof(int) && (int)propertyInfo.GetValue(this) == 0) return false;
            }

            return true;
        }

        public bool ValidatePassport2()
        {
            foreach (PropertyInfo propertyInfo in this.GetType().GetProperties())
            {
                if (propertyInfo.Name == "cid") continue;

                var val = propertyInfo.GetValue(this);

                // pt 1 just wants them all to have a value except cid
                if (propertyInfo.PropertyType == typeof(string) && val == null) return false;
                if (propertyInfo.PropertyType == typeof(int) && (int)val == 0) return false;

                // pt 2 has stricter rules
                switch (propertyInfo.Name)
                {
                    case "byr":
                    // birth year must be 4 digits, between 1920 and 2002
                        if (Regex.Match(val.ToString(), @"\d{4}").Success &&
                            (int)val >= 1920 &&
                            (int)val <= 2002)
                        {
                            break;
                        }
                        return false;
                    case "iyr":
                    // issued year must be 4 digits, between 2010 and 2020
                        if (Regex.Match(val.ToString(), @"\d{4}").Success &&
                            (int)val >= 2010 &&
                            (int)val <= 2020)
                        {
                            break;
                        }
                        return false;
                    case "eyr":
                        // expiry year must be 4 digits, between 2020 and 2030
                        if (Regex.Match(val.ToString(), @"\d{4}").Success &&
                            (int)val >= 2020 &&
                            (int)val <= 2030)
                        {
                            break;
                        }
                        return false;
                    case "hgt":
                    // height must be a number followed by "cm" or "in"
                    // if "in", must be between 59 and 76
                    // if "cm", must be between 150 and 193
                        if (Regex.Match(val.ToString(), @"\d+(cm|in)").Success)
                        {
                            string measure = val.ToString()[^2..];
                            int[] limits = new int[2];
                            int h = Convert.ToInt32(val.ToString()[0..^2]);

                            if (measure == "cm") limits = new int[] { 150, 193 };
                            if (measure == "in") limits = new int[] { 59, 76 };
                            if (h <= limits[1] && h >= limits[0]) break;
                        }
                        return false;
                    case "hcl":
                    // hair color must start with "#" and then be "6 numbers or letters a-f"
                        if (Regex.Match(val.ToString(), @"#[\da-fA-F]{6}").Success) break;
                        return false;
                    case "ecl":
                    // eye color must be one of several valid values
                        if (_validEcl.Contains(val.ToString())) break;
                        return false;
                    case "pid":
                    // passport ID must be exactly 9 digits
                        if (Regex.Match(val.ToString(), @"\d{9}").Success &&
                            val.ToString().Length == 9) 
                            {
                                break;
                            }
                        return false;
                    case "cid":
                    // country ID purposefully ignored
                        break;
                }
            }

            return true;
        }
    }
}
