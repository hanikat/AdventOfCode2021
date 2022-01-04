using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode2021.Common;
using AdventOfCode2021.Problems.Problem12;

namespace AdventOfCode2021.Answers.Problems
{
    public class Problem12 : Problem
    {
        private int _daysToSimulate = 256;
        public Problem12()
        {
        }

        public override byte ProblemNumber
        {
            get
            {
                return 12;
            }
        }

        public override string ProblemName
        {
            get
            {
                return "Day 6: Lanternfish - Part Two";
            }
        }

        protected override string GetSolution()
        {
            List<ulong> initialDaysToSpawn = ParseInitialDaysToSpawnFromInput();
            LanternfishSchool lanternfishSchool = new(initialDaysToSpawn);

            for(int i = 0; i < _daysToSimulate; i++)
            {
                lanternfishSchool.IncrementByOneDay();
            }

            return lanternfishSchool.NumberOfLanternfish.ToString();
        }

        private List<ulong> ParseInitialDaysToSpawnFromInput()
        {
            List<ulong> initialDaysToSpawn = new();

            var listOfInitialDaysAsStrings = _input.Split(",");

            ulong numberParsed;
            foreach(var numberAsString in listOfInitialDaysAsStrings)
            {
                if(ulong.TryParse(numberAsString, out numberParsed))
                {
                    initialDaysToSpawn.Add(numberParsed);
                }
                else
                {
                    throw new Exception($"Unable to parse number from input string. Attempted to parse: {numberAsString}");
                }
            }

            return initialDaysToSpawn;
        }

    }
}
