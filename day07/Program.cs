using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day07
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = args[0];
            StreamReader inputFile = new StreamReader(inputPath);
            string line;

            Dictionary<string, BagRule> rules = new Dictionary<string, BagRule>();
            Regex bagRegex = new Regex("(.*?) bags contain (.*)");
            Regex contentsRegex = new Regex("(\\d+|no) (.*?) bag");

            while ((line = inputFile.ReadLine()) != null)
            {
                var regexMatch = bagRegex.Match(line);
                rules.Add(regexMatch.Groups[1].Value, new BagRule() {
                    ContainerColor = regexMatch.Groups[1].Value,
                    ContentRule = regexMatch.Groups[2].Value
                });
            }

            foreach (var rule in rules.Values)
            {
                var matches = contentsRegex.Matches(rule.ContentRule);

                foreach (Match m in matches)
                {
                    var quant = m.Groups[1].Value;
                    var color = m.Groups[2].Value;
                    if (quant != "no")
                    {
                        var containedColor = rules[color];
                        rule.Contains.Add(rules[color], Convert.ToInt32(quant));
                        containedColor.ContainedBy.Add(rule);
                    }
                }
            }

            int pt1 = Pt1(rules);
            Console.WriteLine($"Pt1: {pt1}");

            int pt2 = Pt2(rules);
            Console.WriteLine($"Pt2: {pt2}");
        }

        static int Pt1(Dictionary<string, BagRule> Rules)
        {
            BagRule shinyGold = Rules["shiny gold"];
            HashSet<string> candidates = new HashSet<string>();
            Stack<BagRule> bagStack = new Stack<BagRule>();
            
            bagStack.Push(shinyGold);

            while (bagStack.Count > 0)
            {
                foreach (var cont in bagStack.Pop().ContainedBy)
                {
                    candidates.Add(cont.ContainerColor);

                    if (cont.ContainedBy.Count > 0) bagStack.Push(cont);
                }
            }

            return candidates.Count;
        }

        static int Pt2(Dictionary<string, BagRule> Rules)
        {
            BagRule shinyGold = Rules["shiny gold"];
            Stack<BagRule> bagStack = new Stack<BagRule>();
            int bags = 0;

            bagStack.Push(shinyGold);

            while (bagStack.Count > 0)
            {
                var rule = bagStack.Pop();
                bags += rule.Contains.Values.Sum();

                foreach (var k in rule.Contains.Keys)
                {
                    for (var i = 0; i < rule.Contains[k]; i++) bagStack.Push(k);
                }
            }

            return bags;
        }
    }

    class BagRule
    {
        public string ContainerColor { get; set; }
        public string ContentRule { get; set; }
        public Dictionary<BagRule, int> Contains = new Dictionary<BagRule, int>();
        public List<BagRule> ContainedBy = new List<BagRule>();
    }
}
