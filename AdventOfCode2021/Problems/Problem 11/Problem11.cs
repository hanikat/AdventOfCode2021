using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode2021.Common;
using AdventOfCode2021.Problems.Problem11;

namespace AdventOfCode2021.Answers.Problems
{
    public class Problem11 : Problem
    {
        private int _daysToSimulate = 80;
        public Problem11()
        {
        }

        public override byte ProblemNumber
        {
            get
            {
                return 11;
            }
        }

        public override string ProblemName
        {
            get
            {
                return "Day 6: Lanternfish";
            }
        }

        protected override string GetSolution()
        {
            List<int> initialDaysToSpawn = ParseInitialDaysToSpawnFromInput();
            LanternfishSchool lanternfishSchool = new(initialDaysToSpawn);

            for(int i = 0; i < _daysToSimulate; i++)
            {
                lanternfishSchool.IncrementByOneDay();
            }

            return lanternfishSchool.NumberOfLanternfish.ToString();
        }

        private List<int> ParseInitialDaysToSpawnFromInput()
        {
            List<int> initialDaysToSpawn = new();

            var listOfInitialDaysAsStrings = _input.Split(",");

            int numberParsed;
            foreach(var numberAsString in listOfInitialDaysAsStrings)
            {
                if(int.TryParse(numberAsString, out numberParsed))
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
