using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Common;

namespace AdventOfCode2021.Answers.Problems
{
    public class Problem6 : Problem
    {
        public Problem6()
        {
        }

        public override byte ProblemNumber
        {
            get
            {
                return 6;
            }
        }

        public override string ProblemName
        {
            get
            {
                return "Day 3: Binary Diagnostic - Part Two";
            }
        }

        protected override string GetSolution()
        {
            List<string> binaryNumbers = Utility.SplitStringByLine(_input).ToList();



            // Calculate oxygen generator rating
            List<string> oxygenGeneratorRatingCandidates = binaryNumbers;
            for(int i = 0; oxygenGeneratorRatingCandidates.Count > 1; i++)
            {
                List<char> charactersAtTargetedIndex = oxygenGeneratorRatingCandidates.Select(n => n.ElementAt(i)).ToList();
                char mostCommonCharacter = CalculateMostCommonCharacter(charactersAtTargetedIndex);
                oxygenGeneratorRatingCandidates = oxygenGeneratorRatingCandidates.Where(n => n.ElementAt(i) == mostCommonCharacter).ToList();
            }

            // Calculate oxygen generator rating
            List<string> CO2ScrubberRatingCandidates = binaryNumbers;
            for (int i = 0; CO2ScrubberRatingCandidates.Count > 1; i++)
            {
                List<char> charactersAtTargetedIndex = CO2ScrubberRatingCandidates.Select(n => n.ElementAt(i)).ToList();
                char mostCommonCharacter = CalculateMostCommonCharacter(charactersAtTargetedIndex);
                CO2ScrubberRatingCandidates = CO2ScrubberRatingCandidates.Where(n => n.ElementAt(i) != mostCommonCharacter).ToList();
            }

            int oxygenGeneratorRating = Convert.ToInt32(oxygenGeneratorRatingCandidates.Single(), 2);
            int CO2ScrubberRating = Convert.ToInt32(CO2ScrubberRatingCandidates.Single(), 2);


            return (oxygenGeneratorRating * CO2ScrubberRating).ToString();
        }

        private char CalculateMostCommonCharacter(List<char> currentBinaryCharacter)
        {
            double numberOfOnes = currentBinaryCharacter.Count(c => c == '1');
            double halfCountOfTotalCharacters = currentBinaryCharacter.Count() / 2.0;
            if(numberOfOnes >= halfCountOfTotalCharacters)
            {
                return '1';
            }
            else
            {
                return '0';
            }
        }

    }
}
