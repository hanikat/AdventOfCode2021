using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Common;
using AdventOfCode2021.Problems.Problem18;

namespace AdventOfCode2021.Answers.Problems
{
    public class Problem18 : Problem
    {
        public Problem18()
        {
        }

        public override byte ProblemNumber
        {
            get
            {
                return 18;
            }
        }

        public override string ProblemName
        {
            get
            {
                return "Day 9: Smoke Basin - Part Two";
            }
        }

        protected override string GetSolution()
        {
            List<int> heightMapPoints = new();
            var inputLines = Utility.SplitStringByLine(_input);

            foreach(var inputLine in inputLines)
            {
                var charsFromInputLine = inputLine.ToCharArray();
                foreach(var ch in charsFromInputLine)
                {
                    if(int.TryParse(ch.ToString(), out int parsedHeight))
                    {
                        heightMapPoints.Add(parsedHeight);
                    }
                    else
                    {
                        throw new Exception($"Failed to parse height from character: {ch}");
                    }
                }
            }

            HeightMap heightMap = new(heightMapPoints, inputLines.First().Length);

            return heightMap.CalculateScoreOfLargestBasins().ToString();
        }

    }
}
