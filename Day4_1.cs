using System;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode2023
{
    class day4_1
    {
        static void Main(string[] args)
        {

            int curTotal = 0;
            int total = 0;
            int id = 0;
            HashSet<string> winners = new HashSet<string>();
            foreach (string line in File.ReadLines("../../../inputDay4.txt"))
            {
                winners.Clear();
                String[] splitId = line.Split(':');
                //id = Convert.ToInt32(splitId[0].Split(' ')[1]);
                string[] splitSide = splitId[1].Split("|");
                foreach (string value in splitSide[0].Split(" "))
                {
                    if (value != "" && !winners.Contains(value))
                    {
                        winners.Add(value);
                    }
                }

                curTotal = 0;
                foreach (string heldValue in splitSide[1].Split(" "))
                {
                    if (winners.Contains(heldValue))
                    {
                        if (curTotal == 0)
                        {
                            curTotal = 1;
                        }
                        else
                        {
                            curTotal *= 2;
                        }
                    }
                }
                total += curTotal;
            }
            Console.WriteLine("output: " + total);
        }
    }
}