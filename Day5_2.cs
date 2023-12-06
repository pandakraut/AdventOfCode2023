using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.Text;
using System.Collections;

namespace AdventOfCode2023
{
    class day5_2
    {
        public static List<seed> seedList = new List<seed>();


        static void Main(string[] args)
        {

            string currentType = "";
            long lowestLoc = 0;
            List<map> seed2soil = new List<map>();
            List<map> soil2fert = new List<map>();
            List<map> fert2water = new List<map>();
            List<map> water2light = new List<map>();
            List<map> light2temp = new List<map>();
            List<map> temp2humid = new List<map>();
            List<map> humid2loc = new List<map>();

            List<long> tempList = new List<long>();
            Dictionary<long, map> tempDict = new Dictionary<long, map>();

            foreach (string line in File.ReadLines("../../../inputDay5.txt"))
            {
                String[] splitType = line.Split(':');
                if (line.Contains(":"))
                {
                    currentType = splitType[0];                    
                    {
                        String[] splitValues = splitType[1].Split(" ");
                        string start = "";                        
                        foreach (String value in splitValues)
                        {
                            if (value != "")
                            {
                                if (start == "")
                                {
                                    start = value;
                                }
                                else
                                {
                                    seed tempSeed = new seed((long)Convert.ToDecimal(start), (long)Convert.ToDecimal(start), (long)Convert.ToDecimal(value) -1);  
                                    seedList.Add(tempSeed);                                                                        
                                    start = "";                                 
                                }                                
                            }
                        }
                    }
                }
                else if (line.Trim().Length > 0)
                {
                    String[] splitValues = splitType[0].Split(" ");
                    long tempValue = (long)Convert.ToDecimal(splitValues[1]);
                    map tempMap = new map(currentType, tempValue, (long)Convert.ToDecimal(splitValues[0]), (long)Convert.ToDecimal(splitValues[2]) -1);
                    tempList.Add(tempValue);
                    tempDict.Add(tempValue, tempMap);                   
                }
                else
                {
                    //end section
                    tempList.Sort();
                    foreach (long i in tempList)
                    {
                        switch (currentType)
                        {
                            case "seed-to-soil map":
                                seed2soil.Add(new map(currentType, i, tempDict[i].target, tempDict[i].range));
                                break;
                            case "soil-to-fertilizer map":                                
                                soil2fert.Add(new map(currentType, i, tempDict[i].target, tempDict[i].range));
                                break;
                            case "fertilizer-to-water map":                                
                                fert2water.Add(new map(currentType, i, tempDict[i].target, tempDict[i].range));
                                break;
                            case "water-to-light map":                                
                                water2light.Add(new map(currentType, i, tempDict[i].target, tempDict[i].range));
                                break;
                            case "light-to-temperature map":                                
                                light2temp.Add(new map(currentType, i, tempDict[i].target, tempDict[i].range));
                                break;
                            case "temperature-to-humidity map":                                
                                temp2humid.Add(new map(currentType, i, tempDict[i].target, tempDict[i].range));
                                break;
                            case "humidity-to-location map":                                
                                humid2loc.Add(new map(currentType, i, tempDict[i].target, tempDict[i].range));
                                break;
                            default: break;
                        }
                    }
                    tempList.Clear();
                    tempDict.Clear();
                    currentType = "";
                }

            }            
            for (int currentSeed = 0; currentSeed < seedList.Count(); currentSeed++)            
            {
                checkMap(currentSeed, seedList[currentSeed], seed2soil);
            }
            for (int currentSeed = 0; currentSeed < seedList.Count(); currentSeed++)
            {               
                checkMap(currentSeed, seedList[currentSeed], soil2fert);             
            }
            for (int currentSeed = 0; currentSeed < seedList.Count(); currentSeed++)
            {                
                checkMap(currentSeed, seedList[currentSeed], fert2water);                                
            }
            for (int currentSeed = 0; currentSeed < seedList.Count(); currentSeed++)
            {                
                checkMap(currentSeed, seedList[currentSeed], water2light);               
            }
            for (int currentSeed = 0; currentSeed < seedList.Count(); currentSeed++)
            {                
                checkMap(currentSeed, seedList[currentSeed], light2temp);             
            }
            for (int currentSeed = 0; currentSeed < seedList.Count(); currentSeed++)
            {                
                checkMap(currentSeed, seedList[currentSeed], temp2humid);             
            }
            for (int currentSeed = 0; currentSeed < seedList.Count(); currentSeed++)
            {             
                checkMap(currentSeed, seedList[currentSeed], humid2loc);
            }

            for (int currentSeed = 0; currentSeed < seedList.Count(); currentSeed++)
            {
                if (lowestLoc == 0 || seedList[currentSeed].start < lowestLoc)
                {
                    lowestLoc = seedList[currentSeed].start;
                }
                
            }

                Console.WriteLine("output: " + lowestLoc);
        }

        public static void checkMap(int originalSeed, seed seed, List<map> currentMap)
        {
            bool mapped = false;
            for (int i = 0; i < currentMap.Count; i++)
            {
                if (seed.start >= currentMap[i].source)
                {
                    //start seed is after start of current range
                    if (seed.start <= currentMap[i].source + currentMap[i].range)
                    {
                        //start seed not outside of current range
                        if (seed.start + seed.range <= currentMap[i].source + currentMap[i].range)
                        {
                            //end seed not outside of current range
                            mapped = true;
                            seedList[originalSeed].start = currentMap[i].target + (seed.start - currentMap[i].source);
                        }
                        else
                        {
                            //split into two seeds
                            //shorten original seeds range, create new seed from where the original now leaves off
                            mapped = true;
                            long newRange = (seed.start + seed.range) - (currentMap[i].source + currentMap[i].range);
                            seed tempSeed = new seed(seedList[originalSeed].origin, seedList[originalSeed].start + (seedList[originalSeed].range - newRange) + 1, newRange);
                            seedList[originalSeed].range = (seedList[originalSeed].range - newRange);
                            seedList[originalSeed].start = currentMap[i].target + (seed.start - currentMap[i].source);
                            seedList.Add(tempSeed);
                        }
                    }
                    if (mapped)
                    {
                        break;
                    }
                }
            }
        }

        public class map
        {
            public string type { get; set; }

            public long source { get; set; }

            public long target { get; set; }

            public long range { get; set; }

            public map(string inputType, long inputSource, long inputTarget, long inputRange)
            {
                type = inputType;
                source = inputSource;
                target = inputTarget;
                range = inputRange;
            }
        }

        public class seed
        {                       
            public long start { get; set; }

            public long range { get; set; }

            public long origin { get; set; }

            public seed(long inputOrigin, long inputStart, long inputRange)
            {                                
                start = inputStart;
                range = inputRange;
                origin = inputOrigin;
            }
        }
    }
}