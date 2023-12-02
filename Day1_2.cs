using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdventOfCode2023
{
    class day1_2
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("one", "1");
            dic.Add("two", "2");
            dic.Add("three", "3");
            dic.Add("four", "4");
            dic.Add("five", "5");
            dic.Add("six", "6");
            dic.Add("seven", "7");
            dic.Add("eight", "8");
            dic.Add("nine", "9");
            List<char> list = new List<char>();
            list.Add('1');
            list.Add('2');
            list.Add('3');
            list.Add('4');
            list.Add('5');
            list.Add('6');
            list.Add('7');
            list.Add('8');
            list.Add('9');
            string first = "";
            string last = "";
            string current = "";
            int total = 0;
            int count = 0;

            foreach (string line in File.ReadLines("../../../inputDay1.txt"))
            {
                int firstIndex = -1;
                int lastIndex = 0;
                int curFirstIndex = 0;
                int curLastIndex = 0;
                first = "";
                last = "";
                foreach (char c in list)
                {
                    curFirstIndex = line.IndexOf(c);
                    curLastIndex = line.LastIndexOf(c);
                    if (curFirstIndex >= 0)
                    {
                        if (firstIndex == -1 || curFirstIndex < firstIndex)
                        {
                            firstIndex = curFirstIndex;
                            first = c.ToString();
                        }
                    }
                    if (curLastIndex >= 0)
                    {
                        if (curLastIndex >= lastIndex)
                        {
                            lastIndex = curLastIndex;
                            last = c.ToString();
                        }
                    }
                }
                foreach (string number in dic.Keys)
                {
                    curFirstIndex = line.IndexOf(number);
                    curLastIndex = line.LastIndexOf(number);
                    if (curFirstIndex >= 0)
                    {
                        if (firstIndex == -1 || curFirstIndex < firstIndex)
                        {
                            firstIndex = curFirstIndex;
                            first = dic[number];
                        }
                    }
                    if (curLastIndex >= 0)
                    {
                        if (curLastIndex >= lastIndex)
                        {
                            lastIndex = curLastIndex;
                            last = dic[number];
                        }
                    }
                }
                count++;
                total += int.Parse(first + last);
            }
            Console.WriteLine("output: " + total + " count: " + count);
        }
    }
}