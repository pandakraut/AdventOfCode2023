using System;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode2023
{
    class day1_1
    {
        static void Main(string[] args)
        {

            string first = "";
            string last = "";
            string current = "";
            int total = 0;
            foreach (string line in File.ReadLines("../../../inputDay1.txt"))
            {
                current = "";
                first = "";
                last = "";
                foreach (char c in line)
                {
                    if (Char.GetNumericValue(c) >= 0)
                    {
                        if (first == "")
                        {
                            first = c.ToString();
                        }
                        else
                        {
                            last = c.ToString();
                        }
                    }
                }
                if (last == "")
                {
                    last = first;
                }
                current = first + last;
                total += int.Parse(current);


            }
            Console.WriteLine("output: " + total);
        }
    }
}