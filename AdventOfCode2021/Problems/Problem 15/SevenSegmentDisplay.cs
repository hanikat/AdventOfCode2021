using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Problems.Problem15
{
    public class SevenSegmentDisplay
    {
        private static Dictionary<int, int> _numberOfSegmentsForDigits = new Dictionary<int, int>()
        {
            {0,6},
            {1,2},
            {2,5},
            {3,5},
            {4,4},
            {5,5},
            {6,6},
            {7,3},
            {8,7},
            {9,6},
        };

        private Dictionary<int, int> _numberOfPossibleDigits = new();

        public SevenSegmentDisplay()
        {
            for(int i = 0; i < 10; i++)
            {
                _numberOfPossibleDigits.Add(i, 0);
            }
        }

        public void RegisterDigitToDisplay(string segmentsToDisplay)
        {
            var possibleDigits = _numberOfSegmentsForDigits.Where(n => n.Value == segmentsToDisplay.Length).ToList();


            foreach(var possibleDigit in possibleDigits)
            {
                _numberOfPossibleDigits[possibleDigit.Key] = _numberOfPossibleDigits[possibleDigit.Key] + 1;
            }
        }

        public int PossibleOccurrancesOfNumberOnDisplay(int number)
        {
            return _numberOfPossibleDigits[number];
        }
    }
}
