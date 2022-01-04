using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Common;
using AdventOfCode2021.Problems.Problem15;

namespace AdventOfCode2021.Answers.Problems
{
    public class Problem15 : Problem
    {
        List<int> _digitsToCountOccurancesFor = new List<int>()
            {
                1,4,7,8
            };

        public Problem15()
        {
        }

        public override byte ProblemNumber
        {
            get
            {
                return 15;
            }
        }

        public override string ProblemName
        {
            get
            {
                return "Day 8: Seven Segment Search";
            }
        }

        protected override string GetSolution()
        {
            var segmentDisplay = new SevenSegmentDisplay();
            var listOfOutputValuesByLine = ParseValuesByLine(ParseOutputValuesFromInput());
            foreach(var outputLine in listOfOutputValuesByLine)
            {
                foreach(var outputSegment in outputLine)
                {
                    segmentDisplay.RegisterDigitToDisplay(outputSegment);
                }
            }

            

            var totalDigitOccurances = _digitsToCountOccurancesFor.Sum(d => segmentDisplay.PossibleOccurrancesOfNumberOnDisplay(d));

            return totalDigitOccurances.ToString();
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
