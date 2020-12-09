using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Puzzle1a();
            Puzzle1b();
            Puzzle2a();
            Puzzle2b();
            Console.WriteLine("Trees(1): " + Puzzle3a(3, 1));
            Puzzle3b();
            Puzzle4a();
            Puzzle4b();
            Console.WriteLine("Boardpasses(1): " + Puzzle5a().Max());
            Puzzle5b();
            Puzzle6a();
            Puzzle6b();
            Puzzle7a();
            Puzzle7b();
            Puzzle8a();
            Puzzle8b();
            Puzzle9a();
            Puzzle9b();

            Console.ReadKey();
        }

        public static void Puzzle1a()
        {
            List<int> nums = File.ReadAllLines("./puzzleData/numbers.txt").ToList().Select(int.Parse).ToList();

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
            List<int> nums = File.ReadAllLines("./puzzleData/numbers.txt").ToList().Select(int.Parse).ToList();

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
            List<String> passwordPolicies = File.ReadAllLines("./puzzleData/passwords.txt").ToList();
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
            List<String> passwordPolicies = File.ReadAllLines("./puzzleData/passwords.txt").ToList();
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
            List<String> treePatterns = File.ReadAllLines("./puzzleData/trees.txt").ToList();
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

        public static void Puzzle4a()
        {
            string inputFile = File.ReadAllText("./puzzleData/passports.txt");
            List<String> requiredFields = new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            int valid = 0;
            List<String> passports = inputFile.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<String[]> passportFieldsData = passports.Select(p => {
                string[] kvString = p.Replace('\n', ' ').Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                return kvString.Select(field => field.Split(':')[0]).ToArray();
            }).ToList();

            foreach (string[] passport in passportFieldsData)
            {
                if (requiredFields.Intersect(passport).Count() == requiredFields.Count) valid++;
            }
            Console.WriteLine("Passports(1): " + valid);
        }

        public static void Puzzle4b()
        {
            string inputFile = File.ReadAllText("./puzzleData/passports.txt");
            List<String> requiredFields = new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            int valid = 0;
            List<String> passports = inputFile.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<Dictionary<string, string>> passportDataSets = passports.Select(p => {
                Dictionary<string, string> passportKeyValues = new Dictionary<string, string>();
                List<String> kvString = p.Replace('\n', ' ').Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                kvString.ForEach(field => passportKeyValues[field.Split(':')[0]] = field.Split(':')[1]);
                return passportKeyValues;
            }).ToList();

            foreach (Dictionary<string, string> passport in passportDataSets)
            {
                if (requiredFields.Intersect(passport.Keys).Count() != requiredFields.Count) continue;
                if (passport["byr"].Length != 4 || passport["iyr"].Length != 4 || passport["eyr"].Length != 4 ||
                    passport["pid"].Length != 9 || passport["hcl"].Length != 7 || passport["hgt"].Length < 4) continue;

                string[] eyeColors = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                string hclCode = passport["hcl"].Substring(1, passport["hcl"].Length-1);
                string hgtUnit = passport["hgt"].Substring(passport["hgt"].Length-2, 2);
                int hgtValue = int.Parse(passport["hgt"].Substring(0, passport["hgt"].Length-2));
                
                if (int.Parse(passport["byr"]) < 1920 || int.Parse(passport["byr"]) > 2002) continue;
                if (int.Parse(passport["iyr"]) < 2010 || int.Parse(passport["iyr"]) > 2020) continue;
                if (int.Parse(passport["eyr"]) < 2020 || int.Parse(passport["eyr"]) > 2030) continue;
                if (!eyeColors.Contains(passport["ecl"])) continue;
                if (passport["hcl"][0] != '#' || !hclCode.All(c => "0123456789abcdef".Contains(c))) continue;
                if (!(hgtUnit == "in" && hgtValue >= 59 && hgtValue <= 76) && !(hgtUnit == "cm" && hgtValue >= 150 && hgtValue <= 193)) continue;

                valid++;
            }
            Console.WriteLine("Passports(2): " + valid);
        }

        public static List<int> Puzzle5a()
        {
            List<String> boardpasses = File.ReadAllLines("./puzzleData/boardpasses.txt").ToList();
            List<int> seatIds = new List<int>();

            foreach (string bp in boardpasses)
            {
                int[] row = new int[] { 0, 128 };
                int[] col = new int[] { 0, 8 };

                bp.ToList().ForEach(c => {
                    int rowHalf = (row[1] - row[0]) / 2;
                    int colHalf = (col[1] - col[0]) / 2;
                    
                    switch (c)
                    {
                        case 'B':
                            row[0] += rowHalf;
                            break;
                        case 'F':
                            row[1] -= rowHalf;
                            break;
                        case 'R':
                            col[0] += colHalf;
                            break;
                        case 'L':
                            col[1] -= colHalf;
                            break;
                        default:
                            break;
                    }
                });
                seatIds.Add(col[0] + row[0] * 8);
            }
            return seatIds;
        }

        public static void Puzzle5b()
        {
            List<int> seatIds = Puzzle5a();

            seatIds.Sort();
            seatIds.Aggregate((prev, cur) => {
                if (cur - prev > 1) Console.WriteLine("Boardpasses(2): " + (prev+1));
                return cur;
            });
        }

        public static void Puzzle6a()
        {
            string formsData = File.ReadAllText("./puzzleData/forms.txt");
            List<String> formGroups = formsData.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<int> formGroupAnswers = formGroups.Select(fg => fg.Replace("\n", string.Empty).Distinct().Count()).ToList();

            Console.WriteLine("Forms(1): " + formGroupAnswers.Sum());
        }

        public static void Puzzle6b()
        {
            string formsData = File.ReadAllText("./puzzleData/forms.txt");
            List<String> formGroups = formsData.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<int> uniqueGroupAnswers = formGroups.Select(fg => {
                List<String> groupAnswers = fg.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                return groupAnswers.Aggregate((a, b) => new string(a.Intersect(b).ToArray())).Count();
            }).ToList();

            Console.WriteLine("Forms(2): " + uniqueGroupAnswers.Sum());
        }

        public static void Puzzle7a()
        {
            List<String> bagRulesData = File.ReadAllLines("./puzzleData/bags.txt").ToList();
            Dictionary<String, List<String>> bagRelations = new Dictionary<string, List<string>>();
            Regex pattern = new Regex(@"\b(?!bags?|contain|,|\d)\b(\S+ \S+)");
            bagRulesData.ForEach(rd => {
                List<String> matches = pattern.Matches(rd).Cast<Match>().Select(m => m.Value).ToList();
                bagRelations[matches[0]] = matches.Skip(1).ToList();
            });
            int count = 0;

            foreach (var contains in bagRelations)
            {
                if (ContainsBag(bagRelations, contains.Value, "shiny gold")) count++;
            }
            Console.WriteLine("Bags(1): " + count);
        }

        public static void Puzzle7b()
        {
            List<String> bagRulesData = File.ReadAllLines("./puzzleData/bags.txt").ToList();
            Dictionary<String, List<String[]>> bagRelations = new Dictionary<String, List<String[]>>();
            Regex pattern = new Regex(@"\b(?!bags?|contain|,)\b((\d )?\S+ \S+)");
            bagRulesData.ForEach(rd => {
                List<String> matches = pattern.Matches(rd).Cast<Match>().Select(m => m.Value).ToList();
                bagRelations[matches[0]] = matches.Skip(1).Select(m => m.Split(new[] {' '}, 2)).ToList();
            });

            Console.WriteLine("Bags(2): " + CountInnerBags(bagRelations, "shiny gold"));
        }

        public static void Puzzle8a()
        {
            List<String> operationsData = File.ReadAllLines("./puzzleData/operations.txt").ToList();
            List<int> usedOpIndexes = new List<int>();
            int acc = 0;
            int opIndex = 0;

            while(!usedOpIndexes.Contains(opIndex) && opIndex >= 0 && opIndex < operationsData.Count - 1)
            {
                string[] opParts = operationsData[opIndex].Split(' ');
                usedOpIndexes.Add(opIndex);

                switch (opParts[0])
                {
                    case "nop":
                        opIndex += 1;
                        break;
                    case "acc":
                        acc += int.Parse(opParts[1]);
                        opIndex += 1;
                        break;
                    case "jmp":
                        opIndex += int.Parse(opParts[1]);
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine("Operations(1): " + acc);
        }

        public static void Puzzle8b()
        {
            List<String> operationsData = File.ReadAllLines("./puzzleData/operations.txt").ToList();
            List<int> usedOpIndexes = new List<int>();
            List<int> triedModifications = new List<int>();
            int acc = 0;
            int opIndex = 0;
            bool canChange = true;

            while (opIndex >= 0 && opIndex < operationsData.Count-1)
            {
                if (usedOpIndexes.Contains(opIndex))
                {
                    acc = 0;
                    opIndex = 0;
                    usedOpIndexes = new List<int>();
                    canChange = true;
                }

                string[] opParts = operationsData[opIndex].Split(' ');
                usedOpIndexes.Add(opIndex);

                if ((opParts[0] == "nop" || opParts[0] == "jmp") && !triedModifications.Contains(opIndex) && canChange)
                {
                    opParts[0] = (opParts[0] == "nop") ? "jmp" : "nop";
                    triedModifications.Add(opIndex);
                    canChange = false;
                }

                switch (opParts[0])
                {
                    case "nop":
                        opIndex += 1;
                        break;
                    case "acc":
                        acc += int.Parse(opParts[1]);
                        opIndex += 1;
                        break;
                    case "jmp":
                        opIndex += int.Parse(opParts[1]);
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine("Operations(2): " + acc);
        }

        public static void Puzzle9a()
        {
            List<long> nums = File.ReadAllLines("./puzzleData/xmas.txt").ToList().Select(long.Parse).ToList();
            int preamble = 25;

            for (int i = preamble; i < nums.Count; i++)
            {
                List<long> preambleNums = nums.GetRange(i-preamble, preamble);
                bool found = false;

                for (int j = 0; j < preambleNums.Count; j++)
                {
                    for (int k = 0; k < preambleNums.Count; k++)
                    {
                        if (preambleNums[j] + preambleNums[k] == nums[i])
                        {
                            found = true;
                        }
                    }
                }
                if(!found) Console.WriteLine("XMAS(1): " + nums[i]);
            }
        }

        public static void Puzzle9b()
        {
            List<long> nums = File.ReadAllLines("./puzzleData/xmas.txt").ToList().Select(long.Parse).ToList();
            int invalidNum = 31161678;

            for (int i = 2; i < nums.Count; i++)
            {
                for (int j = 0; j < nums.Count-i; j++)
                {
                    List<long> contiguousSet = nums.GetRange(j, i);
                    long sum = contiguousSet.Sum();
                    long weakness = contiguousSet.Min() + contiguousSet.Max();
                    if (sum == invalidNum) Console.WriteLine("XMAS(2): " + weakness);
                }
            }
        }



        // Helper Functions
        public static bool ContainsBag(Dictionary<String, List<String>> relations, List<String> bagBags, string bagName)
        {
            if (bagBags.Contains(bagName)) return true;
            if (bagBags.Count == 1 && bagBags[0] == "no other") return false;

            return bagBags.Select(bag => ContainsBag(relations, relations[bag], bagName)).Any(x => x == true);
        }

        public static int CountInnerBags(Dictionary<String, List<String[]>> relations, string bagName)
        {
            if (relations[bagName].Count == 1 && string.Join(" ", relations[bagName][0]) == "no other") return 0;

            return relations[bagName].Select(bag => int.Parse(bag[0]) + int.Parse(bag[0]) * CountInnerBags(relations, bag[1])).Sum();
        }
    }
}
