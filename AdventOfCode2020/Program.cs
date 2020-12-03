using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Puzzle1a(); // passed
            Puzzle1b(); // passed
            Puzzle2a(); // passed
            Puzzle2b(); // passed
            Console.WriteLine("Trees(1): " + Puzzle3a(3, 1)); // passed
            Puzzle3b(); // passed


            Console.ReadKey();
        }

        public static void Puzzle1a()
        {
            List<int> nums = File.ReadAllLines("./puzzleData1/numbers.txt").ToList().Select(int.Parse).ToList();

            foreach(int num in nums)
            {
                if (nums.Contains(2020 - num))
                {
                    int result = num * (2020 - num);
                    Console.WriteLine("Result(1): " + result);
                    return;
                }
            }
        }

        public static void Puzzle1b()
        {
            List<int> nums = File.ReadAllLines("./puzzleData1/numbers.txt").ToList().Select(int.Parse).ToList();

            int tempNum;
            for (int i = 0; i < nums.Count; i++)
            {
                tempNum = 2020 - nums[i];
                for (int j = 0; j < nums.Count; j++)
                {
                    if (nums.Contains(tempNum - nums[j]) && i != j)
                    {
                        int result = nums[i] * nums[j] * (tempNum - nums[j]);
                        Console.WriteLine("Result(2): " + result);
                        return;
                    }
                }
            }
        }

        public static void Puzzle2a()
        {
            List<String> passwordPolicies = File.ReadAllLines("./puzzleData2/passwords.txt").ToList();
            int valid = 0;

            foreach (string passwordPolicy in passwordPolicies)
            {
                string[] policyParts = passwordPolicy.Split(' ');
                List<int> amounts = policyParts[0].Split('-').Select(int.Parse).ToList();
                char character = policyParts[1][0];
                string password = policyParts[2];
                int count = password.Count(c => c == character);

                if (count >= amounts[0] && count <= amounts[1]) valid += 1;
            }
            Console.WriteLine("Passwords Valid(1): " + valid);
        }

        public static void Puzzle2b()
        {
            List<String> passwordPolicies = File.ReadAllLines("./puzzleData2/passwords.txt").ToList();
            int valid = 0;

            foreach (string passwordPolicy in passwordPolicies)
            {
                string[] policyParts = passwordPolicy.Split(' ');
                List<int> positions = policyParts[0].Split('-').Select(int.Parse).ToList();
                char character = policyParts[1][0];
                string password = policyParts[2];
                int count = positions.Select(i => password[i-1]).ToList().Count(c => c == character);

                if (count == 1) valid += 1;
            }
            Console.WriteLine("Passwords Valid(2): " + valid);
        }

        public static int Puzzle3a(int stepX, int stepY)
        {
            List<String> treePatterns = File.ReadAllLines("./puzzleData3/trees.txt").ToList();
            int trees = 0;

            for (int y = 0, x = 0; y < treePatterns.Count; y += stepY, x += stepX)
            {
                if (treePatterns[y][x % treePatterns[y].Length] == '#') trees += 1;
            }
            return trees;
        }

        public static void Puzzle3b()
        {
            List<int[]> slopes = new List<int[]>()
            {
                new int[]{1, 1},
                new int[]{3, 1},
                new int[]{5, 1},
                new int[]{7, 1},
                new int[]{1, 2}
            };
            List<int> treeResults = slopes.Select(slope => Puzzle3a(slope[0], slope[1])).ToList();
            int result = treeResults.Aggregate((total, el) => total * el);
            Console.WriteLine("Trees(2): " + result);
        }
    }
}
