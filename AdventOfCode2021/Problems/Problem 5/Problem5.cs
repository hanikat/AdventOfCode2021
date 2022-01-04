using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Common;

namespace AdventOfCode2021.Answers.Problems
{
    public class Problem5 : Problem
    {
        public Problem5()
        {
        }

        public override byte ProblemNumber
        {
            get
            {
                return 5;
            }
        }

        public override string ProblemName
        {
            get
            {
                return "Day 3: Binary Diagnostic";
            }
        }

        protected override string GetSolution()
        {
            string[] binaryNumbers = Utility.SplitStringByLine(_input);
            List<int> binaryOnesAtPosition = SumBinaryOnesAtPosition(binaryNumbers);
            string binaryString = GenerateBinaryStringFromNumberOfOccurances(binaryOnesAtPosition, binaryNumbers.Length);

            int gamma = Convert.ToInt32(binaryString, 2);
            int epsilon = Convert.ToInt32(binaryString.Replace("1", "TMP").Replace("0", "1").Replace("TMP", "0"), 2);

            return (gamma * epsilon).ToString();
        }

        private List<int> SumBinaryOnesAtPosition(string[] binaryNumbers)
        {
            List<int> occurances = new();
            for(int i = 0; i < binaryNumbers.First().Length; i++)
            {
                occurances.Add(0);
            }

            foreach(var binaryNumber in binaryNumbers)
            {
                int index = 0;
                foreach(var number in binaryNumber)
                {
                    if(number == '1')
                    {
                        occurances[index] = occurances.ElementAt(index) + 1;
                    }
                    index++;
                }
            }

            return occurances;
        }

        private string GenerateBinaryStringFromNumberOfOccurances(List<int> occurrances, int numberOfEntries)
        {
            string binaryString = "";
            foreach(var occurance in occurrances)
            {
                if(occurance > numberOfEntries / 2)
                {
                    binaryString += "1";
                }
                else
                {
                    binaryString += "0";
                }
            }

            return binaryString;
        }

    }
}
