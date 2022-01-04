using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Common;
using AdventOfCode2021.Problems.Problem16;

namespace AdventOfCode2021.Answers.Problems
{
    public class Problem16 : Problem
    {
        public Problem16()
        {
        }

        public override byte ProblemNumber
        {
            get
            {
                return 16;
            }
        }

        public override string ProblemName
        {
            get
            {
                return "Day 8: Seven Segment Search - Part Two";
            }
        }

        protected override string GetSolution()
        {
            var listOfOutputValuesByLine = ParseValuesByLine(ParseOutputValuesFromInput());
            var listOfSignalPatternValues = ParseValuesByLine(ParseUniqueSignalPatternsFromInput());

            int outputValueSum = 0;

            for (int i = 0; i < listOfOutputValuesByLine.Count; i++)
            {
                var segmentDisplay = new SevenSegmentDisplay();
                segmentDisplay.SolveDisplayNumberSegmentMappings(listOfSignalPatternValues[i]);
                outputValueSum += segmentDisplay.GetDisplayOutput(listOfOutputValuesByLine[i]);
            }

            return outputValueSum.ToString();
        }

        private List<string> ParseUniqueSignalPatternsFromInput()
        {
            return _input.Split(Environment.NewLine).Select(s => s.Split('|')[0].Trim()).ToList();
        }

        private List<string> ParseOutputValuesFromInput()
        {
            return _input.Split(Environment.NewLine).Select(s => s.Split('|')[1].Trim()).ToList();
        }

        private List<List<string>> ParseValuesByLine(List<string> lines)
        {
            List<List<string>> outputValuesByLine = new();
            foreach(var line in lines)
            {
                outputValuesByLine.Add(line.Split(" ").ToList());
            }

            return outputValuesByLine;
        }
    }
}
