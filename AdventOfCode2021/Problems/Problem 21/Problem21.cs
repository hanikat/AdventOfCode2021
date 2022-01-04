using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Common;
using AdventOfCode2021.Problems.Problem21;

namespace AdventOfCode2021.Answers.Problems
{
    public class Problem21 : Problem
    {
        private const int _numberOfStepsToSimulate = 100;
        public Problem21()
        {
        }

        public override byte ProblemNumber
        {
            get
            {
                return 21;
            }
        }

        public override string ProblemName
        {
            get
            {
                return "Day 11: Dumbo Octopus";
            }
        }

        protected override string GetSolution()
        {

            List<int> octupusEnergyLevels = new();
            var inputLines = Utility.SplitStringByLine(_input);

            foreach (var inputLine in inputLines)
            {
                var charsFromInputLine = inputLine.ToCharArray();
                foreach (var ch in charsFromInputLine)
                {
                    if (int.TryParse(ch.ToString(), out int parsedHeight))
                    {
                        octupusEnergyLevels.Add(parsedHeight);
                    }
                    else
                    {
                        throw new Exception($"Failed to parse height from character: {ch}");
                    }
                }
            }

            DumboOctupusSchool octupusSchool = new(octupusEnergyLevels, inputLines.First().Length);

            return octupusSchool.CountFlashesAfterSteps(_numberOfStepsToSimulate).ToString();
        }

    }
}
