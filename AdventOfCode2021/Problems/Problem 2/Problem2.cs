using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Common;

namespace AdventOfCode2021.Answers.Problems
{
    public class Problem2 : Problem
    {
        public Problem2()
        {
        }

        public override byte ProblemNumber
        {
            get
            {
                return 2;
            }
        }

        public override string ProblemName
        {
            get
            {
                return "Day 1: Sonar Sweep - Part Two";
            }
        }

        protected override string GetSolution()
        {
            List<int> measurements = Utility.ConvertStringsToInt(Utility.SplitStringByLine(_input));

            return CountNumberOfIncreasingSummedMeasurements(0, measurements).ToString();
        }

        /// <summary>
        /// Calculates how many adjacent sum of values in the list which increases in value.
        /// </summary>
        /// <param name="previousIncreasingMeasurements">The number of previously increasing adjacent values.</param>
        /// <param name="remainingMeasurements">The list of remaining values.</param>
        /// <returns>The number of increases in value between adjacent values within the list.</returns>
        private int CountNumberOfIncreasingSummedMeasurements(int previousIncreasingMeasurements, List<int> remainingMeasurements)
        {
            // check if base case for recursion is fulfilled
            if(remainingMeasurements.Count > 3)
            {
                // calculate the sum of the first two sequence
                var firstSequenceSum = remainingMeasurements.GetRange(0, 3).Sum();
                var secondSequenceSum = remainingMeasurements.GetRange(1, 3).Sum();

                // remove first element from list
                var currentValue = remainingMeasurements.First();
                remainingMeasurements.RemoveAt(0);

                if (firstSequenceSum < secondSequenceSum)
                {
                    return CountNumberOfIncreasingSummedMeasurements(++previousIncreasingMeasurements, remainingMeasurements);
                }
                else
                {
                    return CountNumberOfIncreasingSummedMeasurements(previousIncreasingMeasurements, remainingMeasurements);
                }
            }
            else
            {
                return previousIncreasingMeasurements;
            }
        }
    }
}
