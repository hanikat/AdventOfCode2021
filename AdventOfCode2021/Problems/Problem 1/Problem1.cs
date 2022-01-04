using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Common;

namespace AdventOfCode2021.Answers.Problems
{
    public class Problem1 : Problem
    {
        public Problem1()
        {
        }

        public override byte ProblemNumber
        {
            get
            {
                return 1;
            }
        }

        public override string ProblemName
        {
            get
            {
                return "Day 1: Sonar Sweep";
            }
        }

        protected override string GetSolution()
        {
            List<int> measurements = Utility.ConvertStringsToInt(Utility.SplitStringByLine(_input));

            return CountNumberOfIncreasingMeasurements(0, measurements).ToString();
        }

        /// <summary>
        /// Calculates how many adjacent values in the list increases in value.
        /// </summary>
        /// <param name="previousIncreasingMeasurements">The number of previously increasing adjacent values.</param>
        /// <param name="remainingMeasurements">The list of remaining values.</param>
        /// <returns>The number of increases in value between adjacent values within the list.</returns>
        private int CountNumberOfIncreasingMeasurements(int previousIncreasingMeasurements, List<int> remainingMeasurements)
        {
            // check if base case for recursion is fulfilled
            if(remainingMeasurements.Count > 1)
            {
                // remove first element from list
                var currentValue = remainingMeasurements.First();
                remainingMeasurements.RemoveAt(0);

                if (currentValue < remainingMeasurements.First())
                {
                    return CountNumberOfIncreasingMeasurements(++previousIncreasingMeasurements, remainingMeasurements);
                }
                else
                {
                    return CountNumberOfIncreasingMeasurements(previousIncreasingMeasurements, remainingMeasurements);
                }
            }
            else
            {
                return previousIncreasingMeasurements;
            }
        }
    }
}
