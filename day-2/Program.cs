using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day.two
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText(@"input.txt");
            Console.WriteLine(GetChecksum(input));
            Console.WriteLine(GetCommonCharacters(input));
        }
        
        static int GetChecksum(string input)
        {
            var ids = input.Split('\n');
            var two = 0;
            var three = 0;
            foreach (var id in ids)
            {
                var hasTwo = false;
                var hasThree = false;
            
                var uniques = new HashSet<char>();
                foreach (var c in id)
                {
                    uniques.Add(c);
                }
                foreach (var u in uniques)
                {
                    var count = id.Count(c => u == c);
                    if (count == 2)
                        hasTwo = true;
                    else if (count == 3)
                        hasThree = true;    
                }

                if (hasTwo) two++;
                if (hasThree) three++;
            }
            return two * three;
        }

        static string GetCommonCharacters(string input)
        {
            var ids = input.Split('\n');
            for (int i = 0; i < ids.Length; i++)
            {
                for (int j = i + 1; j < ids.Length; j++)
                {
                    var left = ids[i];
                    var right = ids[j];

                    var result = left
                        .Where((c, k) => c == right[k])
                        .Aggregate("", (current, t) => current + t);

                    if (result.Length == left.Length - 1)
                    {
                        return result;
                    }
                }
            }
            return "";
        }
    }
}