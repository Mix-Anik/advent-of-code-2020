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
            Puzzle1(); // passed
            Puzzle2(); // passed
            Puzzle3(); // passed
            Puzzle4(); // passed

            Console.ReadKey();
        }

        public static void Puzzle1()
        {
            List<int> nums = File.ReadAllLines("./puzzleData1/numbers.txt").ToList().Select(int.Parse).ToList();

            foreach(int num in nums)
            {
                if (nums.Contains(2020 - num))
                {
                    Console.Write("Answer: ");
                    Console.WriteLine(num * (2020 - num));
                    return;
                }
            }
        }

        public static void Puzzle2()
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
                        Console.Write("Answer: ");
                        Console.WriteLine(nums[i] * nums[j] * (tempNum - nums[j]));
                        return;
                    }
                }
            }
        }

        public static void Puzzle3()
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
            Console.WriteLine("Answer: " + valid);
        }

        public static void Puzzle4()
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
            Console.WriteLine("Answer: " + valid);
        }
    }
}
