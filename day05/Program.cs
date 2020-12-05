using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day05
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = args[0];
            StreamReader inputFile = new StreamReader(inputPath);
            string line;
            List<SeatAssignment> input = new List<SeatAssignment>();

            while ((line = inputFile.ReadLine()) != null)
            {
                input.Add(new SeatAssignment(line));
            }

            List<SeatAssignment> sortedAssignments = input.OrderByDescending(x => x.ID).ToList();
            int largestId = input.OrderByDescending(x => x.ID).First().ID;
            Console.WriteLine($"Pt1: {largestId}");

            for (int i = 0; i < sortedAssignments.Count; i++)
            {
                if (sortedAssignments[i].ID != sortedAssignments[i + 1].ID + 1)
                {
                    Console.WriteLine($"Pt 2: {sortedAssignments[i].ID - 1}");
                    break;
                }
            }
        }
    }

    class SeatAssignment
    {
        public int Row { get; set; }
        public int Seat { get; set; }
        public int ID { get; set; }

        public SeatAssignment(string EncodedString)
        {
            string encRow = EncodedString.Substring(0, 7);
            string encSeat = EncodedString.Substring(7, 3);

            string binRow = encRow.Replace('F', '0').Replace('B', '1');
            string binSeat = encSeat.Replace('L', '0').Replace('R', '1');

            Row = Convert.ToInt32(binRow, 2);
            Seat = Convert.ToInt32(binSeat, 2);
            ID = (Row * 8) + Seat;
        }
    }
}
