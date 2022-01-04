using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode2021.Common;
using AdventOfCode2021.Problems.Problem7;

namespace AdventOfCode2021.Answers.Problems
{
    public class Problem7 : Problem
    {
        public Problem7()
        {
        }

        public override byte ProblemNumber
        {
            get
            {
                return 7;
            }
        }

        public override string ProblemName
        {
            get
            {
                return "Day 4: Giant Squid";
            }
        }

        protected override string GetSolution()
        {
            var bingoNumbers = ParseBingoNumbersFromInput();
            var bingoBoards = ParseBingoBoardsFromInput();

            int scoreOfWinningBoard = 0;

            foreach(var bingoNumber in bingoNumbers)
            {
                foreach(var bingoBoard in bingoBoards)
                {
                    bingoBoard.MarkNumber(bingoNumber);
                    if(bingoBoard.HasBingo())
                    {
                        scoreOfWinningBoard = bingoBoard.CalculateScore(bingoNumber);
                        goto boardHadBingo;
                    }
                }
            }

            boardHadBingo:
            return scoreOfWinningBoard.ToString();
        }

        private List<byte> ParseBingoNumbersFromInput()
        {
            var firstLineOfInput = _input.Split(Environment.NewLine).First();

            return firstLineOfInput.Split(',').Select(s => byte.Parse(s)).ToList();
        }

        private List<BingoBoard> ParseBingoBoardsFromInput()
        {
            var inputLines = _input.Split(Environment.NewLine);

            // create regex for getting each bingo board from input
            var bingoBoardsRegex = new Regex("((( ){0,1}([0-9]{1,2}( ){1,2}){4}[0-9]{1,2}(\n|))){5}");

            var bingoBoardMatch = bingoBoardsRegex.Match(_input);
            List<BingoBoard> bingoBoards = new List<BingoBoard>();

            do
            {
                // parse numbers on bingo board and create a new BingoBoard for each bingo board found in input
                var bingoBoardNumberStrings = bingoBoardMatch.Value.Split(new char[] { ' ', '\n' }).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                List<byte> bingoBoardNumbers = new List<byte>();
                foreach(var bingoBoardNumber in bingoBoardNumberStrings)
                {
                    bingoBoardNumbers.Add(byte.Parse(bingoBoardNumber));
                }

                bingoBoards.Add(new BingoBoard(bingoBoardNumbers));

                bingoBoardMatch = bingoBoardMatch.NextMatch();
            } while (bingoBoardMatch.Success);

            return bingoBoards;
        }
    }
}
