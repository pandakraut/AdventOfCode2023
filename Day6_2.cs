using System;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode2023
{
    class day6_2
    {
        static void Main(string[] args)
        {
            
            long total = 1;
            string id = "";
            List<long> times = new List<long>();
            List<long> distances = new List<long>();
            foreach (string line in File.ReadLines("../../../inputDay6.txt"))
            {
                String[] splitId = line.Replace(" ", "").Split(':');
                id = splitId[0];
                if (splitId[1].Trim() != "")
                {
                    if (id == "Time")
                    {
                        times.Add((long)Convert.ToDecimal(splitId[1]));
                    }
                    else
                    {
                        distances.Add((long)Convert.ToDecimal(splitId[1]));
                    }
                }                                                       
            }
            for (int i = 0; i < times.Count; i++)
            {
                total *= checkWins(i, times[i], distances[i]);
            }
            Console.WriteLine("output: " + total);
        }

        public static int checkWins(int index, long time, long distance)
        {
            int wins = 0;
            for (long i = 1; i < time; i++)
            {
                if (((time - i) * i) > distance)
                {
                    wins++;
                }
            }
            return wins;
        }
    }
}