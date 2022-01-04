using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Problems.Problem7
{
    public class BingoBoard
    {
        private const byte BoardWidth = 5;
        private const byte BoardHeight = 5;
        private const byte ExpectedBoardNumberCount = BoardWidth * BoardHeight;

        public BingoBoard(List<byte> boardNumbers)
        {
            if(boardNumbers.Count != ExpectedBoardNumberCount)
            {
                throw new Exception($"Unexpected count of numbers for board. Board should have {ExpectedBoardNumberCount} numbers, but {boardNumbers.Count} was recieved.");
            }
            else
            {
                // initialize board
                foreach(var number in boardNumbers)
                {
                    bingoBoardNumbers.Add(new BingoBoardNumber(number));
                }
            }
        }

        private List<BingoBoardNumber> bingoBoardNumbers = new List<BingoBoardNumber>();

        public void MarkNumber(byte numberToMark)
        {
            var boardNumber = bingoBoardNumbers.Find(n => n.Number == numberToMark);
            if(boardNumber != null)
            {
                boardNumber.IsMarked = true;
            }
        }

        public bool HasBingo()
        {
            bool hasBingo = false;

            // check if all numbers in any row or column are marked
            for(int j = 0; j < BoardHeight; j++)
            {
                List<BingoBoardNumber> rowNumbers = bingoBoardNumbers.GetRange(j * BoardWidth, BoardWidth);
                List<BingoBoardNumber> columnNumbers = bingoBoardNumbers.Where((x, i) => i % BoardWidth == j).ToList();

                if (rowNumbers.TrueForAll(n => n.IsMarked) || columnNumbers.TrueForAll(n => n.IsMarked))
                {
                    hasBingo = true;
                    break;
                }
            }

            return hasBingo;
        }

        public int CalculateScore(byte lastCalledBingoNumber)
        {
            int sumOfUnmarkedNumbers = bingoBoardNumbers.Where(n => !n.IsMarked).Sum(n => n.Number);

            int score = sumOfUnmarkedNumbers * lastCalledBingoNumber;

            return score;
        }

        private class BingoBoardNumber
        {
            public BingoBoardNumber (byte number)
            {
                IsMarked = false;
                Number = number;
            }
            public byte Number { get; }
            public bool IsMarked { get; set; }
        }
    }
}
