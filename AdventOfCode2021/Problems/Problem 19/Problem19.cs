using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Common;
using AdventOfCode2021.Problems.Problem19;

namespace AdventOfCode2021.Answers.Problems
{
    public class Problem19 : Problem
    {
        public Problem19()
        {
        }

        public override byte ProblemNumber
        {
            get
            {
                return 19;
            }
        }

        public override string ProblemName
        {
            get
            {
                return "Day 10: Syntax Scoring";
            }
        }

        protected override string GetSolution()
        {
            List<string> subsystemLines = Utility.SplitStringByLine(_input).ToList();

            var syntaxChecker = new SyntaxChecker();
            syntaxChecker.RegisterSubsystemLines(subsystemLines);

            var invalidCharacters = syntaxChecker.GetInvalidClosingCharacters();
            int syntaxErrorScore = 0;
            foreach(var invalidCharacter in invalidCharacters)
            {
                if(invalidCharacter == ')')
                {
                    syntaxErrorScore += 3;
                }
                else if(invalidCharacter == ']')
                {
                    syntaxErrorScore += 57;
                }
                else if(invalidCharacter == '}')
                {
                    syntaxErrorScore += 1197;
                }
                else if(invalidCharacter == '>')
                {
                    syntaxErrorScore += 25137;
                }
            }

            return syntaxErrorScore.ToString();
        }

    }
}
