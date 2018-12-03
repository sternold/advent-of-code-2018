using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day.three
{
    class Claim
    {
        public int Id { get; }
        public int X { get; }
        public int Y { get; }
        public int W { get; }
        public int H { get; }
        public int X2 => X + W;
        public int Y2 => Y + H;

        public Claim(int id, int x, int y, int w, int h)
        {
            Id = id;
            X = x;
            Y = y;
            W = w;
            H = h;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            Console.WriteLine(GetClaimClashCount(input));
            Console.WriteLine(GetNonClashingId(input));
        }

        static IEnumerable<Claim> GetClaims(string[] rawClaims)
        {
            return rawClaims.Select(claim =>
            {
                var values = Regex
                    .Split(claim, @"[^\d]+")
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(int.Parse)
                    .ToList();
                return new Claim(values[0], values[1], values[2], values[3], values[4]);
            });
        }

        static int[,] GetClaimedFabric(IEnumerable<Claim> claims)
        {
            var fabric = new int[1000, 1000];
            foreach (var claim in claims)
            {
                for (int x = claim.X; x < claim.X2; x++)
                {
                    for (int y = claim.Y; y < claim.Y2; y++)
                    {
                        fabric[x, y]++;
                    }
                }
            }
            return fabric;
        }

        static int GetClaimClashCount(string input)
        {
            var rawClaims = input.Split('\n');
            var claims = GetClaims(rawClaims);
            var fabric = GetClaimedFabric(claims);
            return (from int item
                    in fabric
                where item > 1
                select item).Count();
        }

        static int GetNonClashingId(string input)
        {
            var rawClaims = input.Split('\n');
            var claims = GetClaims(rawClaims);
            var fabric = GetClaimedFabric(claims);
            foreach (var claim in claims)
            {
                var clash = false;
                for (int x = claim.X; x < claim.X2; x++)
                {
                    for (int y = claim.Y; y < claim.Y2; y++)
                    {
                        if (fabric[x, y] > 1)
                            clash = true;
                    }
                }

                if (!clash)
                    return claim.Id;
            }

            return 0;
        }
    }
}