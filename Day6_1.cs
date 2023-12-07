//using System;
//using System.IO;
//using System.Collections.Generic;

//namespace AdventOfCode2023
//{
//    class day6_1
//    {
//        static void Main(string[] args)
//        {
            
//            int total = 1;
//            string id = "";
//            List<int> times = new List<int>();
//            List<int> distances = new List<int>();
//            foreach (string line in File.ReadLines("../../../inputDay6.txt"))
//            {                
//                String[] splitId = line.Split(':'); 
//                id = splitId[0];
//                string[] splitRace = splitId[1].Split(" ");
//                foreach (string value in splitRace)
//                {
//                    if (value.Trim() != "")
//                    {
//                        if (id == "Time")
//                        {
//                            times.Add(Convert.ToInt32(value));
//                        }
//                        else
//                        {
//                            distances.Add(Convert.ToInt32(value));
//                        }                        
//                    }
//                }                                               
//            }
//            for (int i = 0; i < times.Count; i++)
//            {
//                total *= checkWins(i, times[i], distances[i]);
//            }
//            Console.WriteLine("output: " + total);
//        }

//        public static int checkWins(int index, int time, int distance)
//        {
//            int wins = 0;
//            for (int i = 1; i < time; i++)
//            {
//                if (((time - i) * i) > distance)
//                {
//                    wins++;
//                }
//            }
//            return wins;
//        }
//    }
//}