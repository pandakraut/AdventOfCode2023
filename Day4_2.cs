using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Schema;

namespace AdventOfCode2023
{
    class day4_2
    {        
        public static Dictionary<int, int> processLine = new Dictionary<int, int>();
        public static HashSet<string> winners = new HashSet<string>();
        public static Dictionary<int, int> winLog = new Dictionary<int, int>();
        public static int total = 0;
        static void Main(string[] args)
        {      
            int id = 0;
           
            foreach (string line in File.ReadLines("../../../inputDay4.txt"))
            {
                String[] splitId = line.Split(':');
                id = Convert.ToInt32(splitId[0].Replace("Card", "").Trim());
 
                if (processLine.ContainsKey(id))
                {
                    processLine[id]++;
                    total++;
                    for (int i = 0; i < processLine[id]; i++)
                    {
                        if (winLog.ContainsKey(id))
                        {
                            addLines(id, winLog[id]);                            
                        }                    
                        else
                        {
                            checkWinners(splitId, id);
                        }
                    }
                }
                else
                {
                    processLine.Add(id, 1);
                    total++;
                    checkWinners(splitId, id);                    
                }                
            }
            Console.WriteLine("output: " + total);
        }

        public static void addLines(int id, int curTotal)
        {
            int nextId = id;
            nextId++;
            
            for (int newId = nextId; newId <= id + curTotal; newId++)
            {
                if (processLine.ContainsKey(newId))
                {
                    processLine[newId]++;
                }
                else
                {
                    processLine.Add(newId, 1);
                }
                total++;
            }
        }

        public static void checkWinners(string[] splitId, int id)
        {
            int curTotal = 0;
            winners.Clear();
            string[] splitSide = splitId[1].Split("|");
            foreach (string value in splitSide[0].Split(" "))
            {
                if (value != "" && !winners.Contains(value))
                {
                    winners.Add(value);
                }
            }
            
            foreach (string heldValue in splitSide[1].Split(" "))
            {
                if (winners.Contains(heldValue))
                {
                    curTotal++;
                }
            }
            winLog.Add(id, curTotal);            
            addLines(id, curTotal);
            
        }
    }
}