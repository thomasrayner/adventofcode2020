using System;
using System.Collections.Generic;
using System.IO;

namespace day12
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = args[0];
            StreamReader inputFile = new StreamReader(inputPath);
            string line;

            ShipNav ship = new ShipNav();
            WaypointNav way = new WaypointNav();

            while ((line = inputFile.ReadLine()) != null)
            {
                char c = line[0];
                int d = int.Parse(line[1..]);

                switch (c)
                {
                    case 'L':
                    case 'R':
                        ship.Turn(c, d);
                        way.Turn(c, d);
                        break;
                    case 'F':
                        ship.Forward(d);
                        way.Forward(d);
                        break;
                    case 'N':
                        ship.North += d;
                        way.WayNorth += d;
                        break;
                    case 'S':
                        ship.South += d;
                        way.WayNorth -= d;
                        break;
                    case 'E':
                        ship.East += d;
                        way.WayEast += d;
                        break;
                    case 'W':
                        ship.West += d;
                        way.WayEast -=d ;
                        break;
                }
            }

            Console.WriteLine($"Pt1: {ship.ManhattanDistance()}");
            Console.WriteLine($"Pt2: {way.ManhattanDistance()}");
        }

        class ShipNav
        {
            public int North { get; set; }
            public int South { get; set; }
            public int East { get; set; }
            public int West { get; set; }
            public string Facing = "East";

            public void Turn(char Direction, int Degrees)
            {
                int turns = Degrees / 90;

                for (int i = 0; i < turns; i++)
                {
                    if (Direction == 'L')
                    {
                        switch (Facing)
                        {
                            case "East":
                                Facing = "North";
                                break;
                            case "North":
                                Facing = "West";
                                break;
                            case "West":
                                Facing = "South";
                                break;
                            case "South":
                                Facing = "East";
                                break;
                        }
                    }

                    else if (Direction == 'R')
                    {
                        switch (Facing)
                        {
                            case "East":
                                Facing = "South";
                                break;
                            case "North":
                                Facing = "East";
                                break;
                            case "West":
                                Facing = "North";
                                break;
                            case "South":
                                Facing = "West";
                                break;
                        }
                    }
                }
            }

            public int ManhattanDistance()
            {
                return Math.Abs(East - West) + Math.Abs(North - South);
            }

            public void Forward(int Count)
            {
                switch (Facing)
                {
                    case "East":
                        East += Count;
                        break;
                    case "West":
                        West += Count;
                        break;
                    case "North":
                        North += Count;
                        break;
                    case "South":
                        South += Count;
                        break;
                }
            }
        }
    
        class WaypointNav
        {
            public int ShipNorth { get; set; }
            public int ShipEast { get; set; }
            public int WayNorth = 1;
            public int WayEast = 10;

            public void Turn(char Direction, int Degrees)
            {
                int turns = Degrees / 90;

                for (int i = 0; i < turns; i++)
                {
                    int curNorth = WayNorth;
                    int curEast = WayEast;

                    if (Direction == 'L')
                    {
                        WayEast = -curNorth;
                        WayNorth = curEast;
                    }

                    else if (Direction == 'R')
                    {
                        WayEast = curNorth;
                        WayNorth = -curEast;
                    }
                }
            }

            public int ManhattanDistance()
            {
                return Math.Abs(ShipEast) + Math.Abs(ShipNorth);
            }

            public void Forward(int Count)
            {
                ShipNorth += WayNorth * Count;
                ShipEast += WayEast * Count;
            }
        }
    }
}
