using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Common;
using AdventOfCode2021.Problems.Problem20;

namespace AdventOfCode2021.Answers.Problems
{
    public class Problem20 : Problem
    {
        public Problem20()
        {
        }

        public override byte ProblemNumber
        {
            get
            {
                return 20;
            }
        }

        public override string ProblemName
        {
            get
            {
                return "Day 10: Syntax Scoring - Part Two";
            }
        }

        protected override string GetSolution()
        {
            List<string> subsystemLines = Utility.SplitStringByLine(_input).ToList();

            var syntaxChecker = new SyntaxChecker();
            syntaxChecker.RegisterSubsystemLines(subsystemLines);

            var missingClosingCharactersForLines = syntaxChecker.GetMissingClosingCharactersForLines();

            List<long> syntaxScores = new();
            foreach(var missingCharacters in missingClosingCharactersForLines)
            {
                long syntaxScore = 0;
                foreach (var missingCharacter in missingCharacters)
                {
                    syntaxScore *= 5;
                    if (missingCharacter == ')')
                    {
                        syntaxScore += 1;
                    }
                    else if (missingCharacter == ']')
                    {
                        syntaxScore += 2;
                    }
                    else if (missingCharacter == '}')
                    {
                        syntaxScore += 3;
                    }
                    else if (missingCharacter == '>')
                    {
                        syntaxScore += 4;
                    }
                }
                syntaxScores.Add(syntaxScore);
            }
            syntaxScores.Sort();

            return syntaxScores[syntaxScores.Count / 2].ToString();
        }

    }
}
