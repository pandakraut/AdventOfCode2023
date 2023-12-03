using System;
using System.IO;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Reflection;
using System.Xml.Schema;

namespace AdventOfCode2022
{
    class day3_2
    {
        public static List<List<point>> mapping = new List<List<point>>();
        public static int total = 0;
        public static Dictionary<string,List<int>> gears = new Dictionary<string,List<int>>();
        public static List<point> multipliers = new List<point>();


        static void Main(string[] args)
        {
            List<point> currentLine = new List<point>();

            foreach (string line in File.ReadLines("../../../inputDay3.txt"))
            {
                currentLine = new List<point>();
                foreach (char c in line)
                {
                    currentLine.Add(new point(c.ToString(), Char.IsNumber(c)));
                }
                mapping.Add(currentLine);
            }

            point currentPoint;
            string currentValue = "";
            bool anyMultiplier = false;
            string currentMultiplierLoc = "";

            for (int row = 0; row < mapping.Count; row++)
            {
                currentLine = mapping[row];
                for (int col = 0; col < currentLine.Count; col++)
                {
                    currentPoint = currentLine[col];
                    if (currentPoint.isNumber)
                    {
                        currentValue += currentPoint.value;
                        for (int inc = -1; inc < 2; inc++)
                        {
                            checkDirection(currentPoint, col, row, inc, -1);
                            checkDirection(currentPoint, col, row, inc, 0);
                            checkDirection(currentPoint, col, row, inc, 1);
                        }

                        if (currentPoint.isAdjacent)
                        {
                            anyMultiplier = true;
                            currentMultiplierLoc = currentPoint.multiplierLoc;
                        }

                        if (currentPoint.endOfNumber)
                        {
                            if (anyMultiplier)
                            {
                                if (!gears.ContainsKey(currentMultiplierLoc))
                                {                                    
                                    gears.Add(currentMultiplierLoc, new List<int>());                                    
                                }
                                gears[currentMultiplierLoc].Add(Convert.ToInt32(currentValue));                                
                            }
                            currentValue = "";
                            anyMultiplier = false;
                        }
                    }
                }
            }

            int ratio = 1;
            foreach (KeyValuePair<string,List<int>> gear in gears)
            {
                ratio = 1;
                foreach (int part in gear.Value)
                {
                    if (gear.Value.Count > 1)
                    { 
                        ratio *= part;
                    }
                    else
                    {
                        ratio = 0;
                        break;
                    }
                }
                total += ratio;
            }            

            Console.WriteLine("output: " + total);
        }

        public static void checkDirection(point currentPoint, int col, int row, int hor, int vert)
        {
            int vertical = row + vert;
            int horizontal = col + hor;
            if ((hor == 1 && vert == 0) && (horizontal > mapping[row].Count - 1 || !mapping[vertical][horizontal].isNumber))
            {
                currentPoint.endOfNumber = true;
                if (horizontal <= mapping[row].Count - 1 && mapping[vertical][horizontal].isMultiplier)
                {
                    currentPoint.isAdjacent = true;
                    currentPoint.multiplierLoc = vertical.ToString() + "_" + horizontal.ToString();

                }
            }
            else if (horizontal < 0 || horizontal > mapping[row].Count - 1 || vertical < 0 || vertical > mapping.Count - 1)
            {
                //go next
            }
            else if (mapping[vertical][horizontal].isMultiplier)
            {
                currentPoint.isAdjacent = true;
                currentPoint.multiplierLoc = vertical.ToString() + "_" + horizontal.ToString();
            }

        }

        public class point
        {
            public int number { get; set; }

            public string value { get; set; }

            public bool isAdjacent { get; set; }

            public bool isNumber { get; set; }

            public bool isSymbol { get; set; }

            public bool endOfNumber { get; set; }

            public bool isMultiplier { get; set; }
            public string multiplierLoc { get; set; }

            public point(string input, bool isNumeric)
            {
                value = input;
                isNumber = isNumeric;
                isAdjacent = false;
                endOfNumber = false;
                isSymbol = false;
                isMultiplier = false;
                multiplierLoc = "";
                if (isNumeric)
                {
                    number = Convert.ToInt32(input);
                }
                if (!isNumeric && value != ".")
                {
                    isSymbol = true;
                    if (value == "*")
                    {
                        isMultiplier = true;
                    }
                }
            }
        }
    }
}