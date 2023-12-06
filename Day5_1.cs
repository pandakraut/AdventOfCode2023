using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace AdventOfCode2023
{
    class day5_1
    {
        public static Dictionary<string, long> seedList = new Dictionary<string, long>();


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

            foreach (string line in File.ReadLines("../../../inputDay5.txt"))
            {
                String[] splitType = line.Split(':');
                if (line.Contains(":"))
                {
                    currentType = splitType[0];
                    {
                        String[] splitValues = splitType[1].Split(" ");
                        foreach (String value in splitValues)
                        {
                            if (value != "")
                            {
                                seedList.Add(value, (long)Convert.ToDecimal(value));
                            }
                        }
                    }
                }
                else if (line.Trim().Length > 0)
                {
                    String[] splitValues = splitType[0].Split(" ");
                    switch (currentType)
                    {
                        case "seed-to-soil map":
                            seed2soil.Add(new map(currentType, (long)Convert.ToDecimal(splitValues[1]), (long)Convert.ToDecimal(splitValues[0]), (long)Convert.ToDecimal(splitValues[2])));
                            break;
                        case "soil-to-fertilizer map":
                            soil2fert.Add(new map(currentType, (long)Convert.ToDecimal(splitValues[1]), (long)Convert.ToDecimal(splitValues[0]), (long)Convert.ToDecimal(splitValues[2])));
                            break;
                        case "fertilizer-to-water map":
                            fert2water.Add(new map(currentType, (long)Convert.ToDecimal(splitValues[1]), (long)Convert.ToDecimal(splitValues[0]), (long)Convert.ToDecimal(splitValues[2])));
                            break;
                        case "water-to-light map":
                            water2light.Add(new map(currentType, (long)Convert.ToDecimal(splitValues[1]), (long)Convert.ToDecimal(splitValues[0]), (long)Convert.ToDecimal(splitValues[2])));
                            break;
                        case "light-to-temperature map":
                            light2temp.Add(new map(currentType, (long)Convert.ToDecimal(splitValues[1]), (long)Convert.ToDecimal(splitValues[0]), (long)Convert.ToDecimal(splitValues[2])));
                            break;
                        case "temperature-to-humidity map":
                            temp2humid.Add(new map(currentType, (long)Convert.ToDecimal(splitValues[1]), (long)Convert.ToDecimal(splitValues[0]), (long)Convert.ToDecimal(splitValues[2])));
                            break;
                        case "humidity-to-location map":
                            humid2loc.Add(new map(currentType, (long)Convert.ToDecimal(splitValues[1]), (long)Convert.ToDecimal(splitValues[0]), (long)Convert.ToDecimal(splitValues[2])));
                            break;
                        default: break;
                    }

                }
                else
                {
                    //end section
                    currentType = "";
                }
            }
            foreach (KeyValuePair<string, long> seed in seedList)
            {
                checkMap(seed.Key, seedList[seed.Key], seed2soil);
                checkMap(seed.Key, seedList[seed.Key], soil2fert);
                checkMap(seed.Key, seedList[seed.Key], fert2water);
                checkMap(seed.Key, seedList[seed.Key], water2light);
                checkMap(seed.Key, seedList[seed.Key], light2temp);
                checkMap(seed.Key, seedList[seed.Key], temp2humid);
                checkMap(seed.Key, seedList[seed.Key], humid2loc);

                if (seedList[seed.Key] < lowestLoc || lowestLoc == 0)
                {
                    lowestLoc = seedList[seed.Key];
                }

            }

            Console.WriteLine("output: " + lowestLoc);
        }

        public static void checkMap(string seed, long seedValue, List<map> currentMap)
        {
            bool mapped = false;
            foreach (map conversion in currentMap)
            {
                if (seedValue >= conversion.source && seedValue <= conversion.source + conversion.range)
                {
                    mapped = true;
                    seedList[seed] = conversion.target + (seedValue - conversion.source);
                }
                if (mapped)
                {
                    break;
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
    }
}