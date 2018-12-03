using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day.one
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            Console.WriteLine(GetFrequency(input));
            Console.WriteLine(GetRepeatFrequency(input));
        }
        
        static int GetFrequency(string input)
        {
            var numbers = input.Split('\n');
            return numbers.Aggregate(0, (current, number) => (number[0] == '-'
                ? current - Convert.ToInt32(number.Substring(1))
                : current + Convert.ToInt32(number.Substring(1))));
        }

        static int GetRepeatFrequency(string input)
        {
            var numbers = input.Split('\n');
            var count = 0;
            var frequencies = new List<int>();
            var found = false;
            while (!found)
            {
                foreach (var number in numbers)
                {
                    frequencies.Add(count);
                    count = number[0] == '-'
                        ? count - Convert.ToInt32(number.Substring(1))
                        : count + Convert.ToInt32(number.Substring(1));
                    if (!frequencies.Contains(count)) continue;
                    
                    found = true;
                    break;
                }
            }
            return count;
        }
    }
}