using System;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode2023
{
    class day2_2
    {
        static void Main(string[] args)
        {

            int minBlue = 0;
            int minRed = 0;
            int minGreen = 0;
            int total = 0;            
            foreach (string line in File.ReadLines("../../../inputDay2.txt"))
            {
                minBlue = 0;
                minRed = 0;
                minGreen = 0;
                String[] splitId = line.Split(':');                
                string[] splitGames = splitId[1].Split(";");
                foreach (string game in splitGames)
                {
                    
                    foreach (string pull in game.Split(","))
                    {                        
                        int count = Convert.ToInt32(pull.TrimStart().Substring(0, pull.TrimStart().IndexOf(" ")));
                        if (pull.Contains("blue") && (count > minBlue || minBlue == 0))
                        {                           
                            minBlue = count;
                        }
                        if (pull.Contains("green") && (count > minGreen || minGreen == 0))
                        {
                            minGreen = count; 
                        }
                        if (pull.Contains("red") && (count > minRed || minRed == 0))
                        {                   
                            minRed = count;
                        }
                    }
                }
                total += minBlue * minGreen * minRed;
            }
            Console.WriteLine("output: " + total);
        }
    }
}