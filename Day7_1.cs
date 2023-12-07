using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Schema;

namespace AdventOfCode2023
{
    class day7_1
    {
        static void Main(string[] args)
        {

            Dictionary<string, decimal> score = new Dictionary<string, decimal>();
            score.Add("A", 13);
            score.Add("K", 12);
            score.Add("Q", 11);
            score.Add("J", 10);
            score.Add("T", 9);
            score.Add("9", 8);
            score.Add("8", 7);
            score.Add("7", 6);
            score.Add("6", 5);
            score.Add("5", 4);
            score.Add("4", 3);
            score.Add("3", 2);
            score.Add("2", 1);

            Dictionary<string, int> handContent = new Dictionary<string, int>();

            Dictionary<decimal, hand> hands = new Dictionary<decimal, hand>();
            int currentHand = 0;
            int total = 0;
            foreach (string line in File.ReadLines("../../../inputDay7.txt"))
            {
                int fiveCount = 0;
                int fourCount = 0;                
                int threeCount = 0;                
                int twoCount = 0;                
                decimal singleTotal = 0;
                decimal rank = 0;
                int currentCard = 1;

                handContent.Clear();
                String[] split = line.Split(" ");

                foreach (char c in split[0])
                {
                    if (handContent.ContainsKey(c.ToString()))
                    {
                        handContent[c.ToString()]++;
                    }
                    else
                    {
                        handContent.Add(c.ToString(), 1);
                    }
                    singleTotal += score[c.ToString()] / ((decimal)MathF.Pow(100, currentCard));
                    currentCard++;
                }

                foreach (KeyValuePair<string, int> pair in handContent)
                {
                    if (pair.Value > 4)
                    {
                        fiveCount++;                        
                        rank = 10000000;
                    }
                    else if (pair.Value > 3)
                    {
                        fourCount++;                        
                        rank = 1000000;
                    }
                    else if (pair.Value > 2)
                    {
                        threeCount++;                        
                    }
                    else if (pair.Value > 1)
                    {
                        twoCount++;                        
                    }                    
                }

                if (threeCount > 0)
                {                    
                    if (twoCount > 0)
                    {                 
                        rank = 100000;
                    }
                    else
                    {                     
                        rank = 10000;
                    }
                }
                else if (twoCount > 1)
                {                    
                    rank = 1000;
                }
                else if (twoCount > 0)
                {                 
                    rank = 100;
                }
                rank += singleTotal;

                hands.Add(currentHand, new hand(split[0], rank, Convert.ToInt32(split[1])));

                currentHand++;
            }
            var ordered = hands.OrderBy(h => h.Value.rank).ToDictionary(h => h.Key, h => h.Value);

            int orderedIndex = 1;
            foreach (KeyValuePair<decimal, hand> orderedPair in ordered)
            {
                total += orderedPair.Value.bid * orderedIndex;
                orderedIndex++;
            }

            Console.WriteLine("output: " + total);
        }

        public class hand
        {
            public string cards { get; set; }

            public decimal rank { get; set; }

            public int bid { get; set; }

            public hand(string inputCards, decimal inputRank, int inputBid)
            {
                cards = inputCards;
                rank = inputRank;
                bid = inputBid;
            }
        }
    }
}