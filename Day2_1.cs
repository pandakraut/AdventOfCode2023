using System;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode2023
{
    class day2_1
    {
        static void Main(string[] args)
        {

            int maxBlue = 14;
            int maxRed = 12;
            int maxGreen = 13;
            int total = 0;
            int id = 0;
            bool valid = true;
            foreach (string line in File.ReadLines("../../../inputDay2.txt"))
            {
                valid = true;
                String[] splitId = line.Split(':');
                id = Convert.ToInt32(splitId[0].Split(' ')[1]);
                string[] splitGames = splitId[1].Split(";");
                foreach (string game in splitGames)
                {
                    foreach (string pull in game.Split(","))
                    {
                        int count = Convert.ToInt32(pull.TrimStart().Substring(0, pull.TrimStart().IndexOf(" ")));
                        if (pull.Contains("blue") && count > maxBlue)
                        {
                            valid = false;
                            break;
                        }
                        if (pull.Contains("green") && count > maxGreen)
                        {
                            valid = false;
                            break;
                        }
                        if (pull.Contains("red") && count > maxRed)
                        {
                            valid = false;
                            break;
                        }
                    }
                }
                if (valid)
                {
                    total += id;
                }
            }
            Console.WriteLine("output: " + total);
        }
    }
}