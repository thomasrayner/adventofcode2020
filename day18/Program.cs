using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day18
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = args[0];
            
            Console.WriteLine($"Pt1: {Part1(inputPath)}");
            Console.WriteLine($"Pt2: {Part2(inputPath)}");
        }
        
        static long Part1(string InputPath)
        {
            StreamReader inputFile = new(InputPath);
            string line;
            var sum = 0L;

            while ((line = inputFile.ReadLine()) != null)
            {
                // https://en.wikipedia.org/wiki/Shunting-yard_algorithm

                var opsStack = new Stack<char>();
                var valStack = new Stack<long>();

                void evalUntil(string Ops)
                {
                    while (!Ops.Contains(opsStack.Peek()))
                    {
                        if (opsStack.Pop() == '+')
                            valStack.Push(valStack.Pop() + valStack.Pop());
                        else
                            valStack.Push(valStack.Pop() * valStack.Pop());
                    }
                }

                opsStack.Push('(');

                foreach (var c in line)
                {
                    switch (c)
                    {
                        case ' ':
                            break;
                        case '*':
                            evalUntil("(");
                            opsStack.Push('*');
                            break;
                        case '+':
                            evalUntil("(");
                            opsStack.Push('+');
                            break;
                        case '(':
                            opsStack.Push('(');
                            break;
                        case ')':
                            evalUntil("(");
                            opsStack.Pop();
                            break;
                        default:
                            valStack.Push(long.Parse(c.ToString()));
                            break;
                    }
                }

                evalUntil("(");

                sum += valStack.Single();
            }

            return sum;
        }

        static long Part2(string InputPath)
        {
            StreamReader inputFile = new(InputPath);
            string line;
            var sum = 0L;

            while ((line = inputFile.ReadLine()) != null)
            {
                // https://en.wikipedia.org/wiki/Shunting-yard_algorithm

                var opsStack = new Stack<char>();
                var valStack = new Stack<long>();

                void evalUntil(string Ops)
                {
                    while (!Ops.Contains(opsStack.Peek()))
                    {
                        if (opsStack.Pop() == '+')
                            valStack.Push(valStack.Pop() + valStack.Pop());
                        else
                            valStack.Push(valStack.Pop() * valStack.Pop());
                    }
                }

                opsStack.Push('(');

                foreach (var c in line)
                {
                    switch (c)
                    {
                        case ' ':
                            break;
                        case '*':
                            evalUntil("(");
                            opsStack.Push('*');
                            break;
                        case '+':
                            evalUntil("(*");
                            opsStack.Push('+');
                            break;
                        case '(':
                            opsStack.Push('(');
                            break;
                        case ')':
                            evalUntil("(");
                            opsStack.Pop();
                            break;
                        default:
                            valStack.Push(long.Parse(c.ToString()));
                            break;
                    }
                }

                evalUntil("(");

                sum += valStack.Single();
            }

            return sum;
        }
    }
}
