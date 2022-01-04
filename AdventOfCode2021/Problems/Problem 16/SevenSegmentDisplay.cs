using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Problems.Problem16
{
    public class SevenSegmentDisplay
    {

        

        private bool _allDisplaySegmentMappingsSolved = false;
        private Dictionary<int,DisplayNumber> _displayNumbers = new();

        public SevenSegmentDisplay()
        {
        }

        /// <summary>
        /// Converts a series of active output segments to an integer value.
        /// </summary>
        /// <param name="activeOutputSegments">A list of active segments for each number on the display.</param>
        /// <returns>The summed value of the numbers shown on the display.</returns>
        public int GetDisplayOutput(List<string> activeOutputSegments)
        {
            activeOutputSegments.Reverse();
            int sum = 0, power = 0;

            foreach (var activeSegments in activeOutputSegments)
            {
                sum += _displayNumbers.Single(n => n.Value.SegmentsCorrespondsToNumber(activeSegments)).Key * (int)Math.Pow(10, power);
                power++;
            }

            return sum;
            
        }

        /// <summary>
        /// Attempts to solve the mappings between display segments and characters in output strings.
        /// </summary>
        /// <param name="listOfOutputSegments">The characters of the active segments.</param>
        /// <returns>true if mappings for all numbers have been solved, otherwise false</returns>
        public bool SolveDisplayNumberSegmentMappings(List<string> listOfOutputSegments)
        {
            if(!_allDisplaySegmentMappingsSolved)
            {
                for(int i = 0; i < 3; i++)
                {
                    foreach (var outputSegment in listOfOutputSegments)
                    {
                        UpdateSegmentToNumberMappings(outputSegment);
                        if (AllNumbersAreSolved())
                        {
                            _allDisplaySegmentMappingsSolved = true;
                            break;
                        }
                    }
                }
            }

            return _allDisplaySegmentMappingsSolved;
        }

        /// <summary>
        /// Check if all numbers have been mapped to a set of segments.
        /// </summary>
        /// <returns>true if all numbers have been mapped</returns>
        private bool AllNumbersAreSolved()
        {
            bool allNumbersSolved = true;
            for(int i = 0; i <= 9; i++)
            {
                if(!_displayNumbers.ContainsKey(i))
                {
                    allNumbersSolved = false;
                    break;
                }
            }

            return allNumbersSolved;
        }


        // Display segments by displayed numbers:
        // 0: 6
        // 1: 2
        // 2: 5
        // 3: 5
        // 4: 4
        // 5: 5
        // 6: 6
        // 7: 3
        // 8: 7
        // 9: 6

        /// <summary>
        /// Uses the length of activeSegments string and known relations between segments to map sets of segments to specific numbers.
        /// </summary>
        /// <param name="activeSegments">The currently active segments</param>
        private void UpdateSegmentToNumberMappings(string activeSegments)
        {
            switch(activeSegments.Length)
            {
                case 2:
                    _displayNumbers.TryAdd(1,new DisplayNumber(activeSegments));
                    break;

                case 3:
                    _displayNumbers.TryAdd(7,new DisplayNumber(activeSegments));
                    break;

                case 4:
                    _displayNumbers.TryAdd(4,new DisplayNumber(activeSegments));
                    break;

                case 5:
                    // check if number 3 (2 segments in common with number 1)
                    if(_displayNumbers.ContainsKey(1))
                    {
                        if(_displayNumbers[1].SegmentsInCommon(activeSegments) == 2)
                        {
                            _displayNumbers.TryAdd(3, new DisplayNumber(activeSegments));
                        }
                        else if (_displayNumbers.ContainsKey(4))
                        {
                            // check if number 2 (2 segments in common with number 4)
                            if (_displayNumbers[4].SegmentsInCommon(activeSegments) == 2)
                            {
                                _displayNumbers.TryAdd(2, new DisplayNumber(activeSegments));
                            }
                            // check if number 5 (3 segments in common with number 4)
                            else if (_displayNumbers[4].SegmentsInCommon(activeSegments) == 3)
                            {
                                _displayNumbers.TryAdd(5, new DisplayNumber(activeSegments));
                            }
                        }
                    }
                    break;

                case 6:
                    if(_displayNumbers.ContainsKey(7))
                    {
                        // check if number 6 (2 segments in common with number 7)
                        if(_displayNumbers[7].SegmentsInCommon(activeSegments) == 2)
                        {
                            _displayNumbers.TryAdd(6, new DisplayNumber(activeSegments));
                        }
                        else if(_displayNumbers.ContainsKey(4))
                        {
                            // check if number 0 (3 segments in common with number 7 and 3 segments in common with number 4)
                            if (_displayNumbers[7].SegmentsInCommon(activeSegments) == 3 && _displayNumbers[4].SegmentsInCommon(activeSegments) == 3)
                            {
                                _displayNumbers.TryAdd(0, new DisplayNumber(activeSegments));
                            }
                            // check if number 9 (3 segments in common with number 7 and 4 segments in common with number 4)
                            else if (_displayNumbers[7].SegmentsInCommon(activeSegments) == 3 && _displayNumbers[4].SegmentsInCommon(activeSegments) == 4)
                            {
                                _displayNumbers.TryAdd(9, new DisplayNumber(activeSegments));
                            }
                        }
                    }
                    break;

                case 7:
                    _displayNumbers.TryAdd(8,new DisplayNumber(activeSegments));
                    break;
            }
        }

        private class DisplayNumber
        {
            public DisplayNumber(string segments)
            {
                Segments = segments;
            }

            public string Segments { get; private set; }

            public int SegmentsInCommon(string comparedSegments)
            {
                var charsInSegments = Segments.ToList();
                List<char> commonSegments = comparedSegments.ToList().Where(c => charsInSegments.Contains(c)).ToList();
                return commonSegments.Count;
            }

            public bool SegmentsCorrespondsToNumber(string comparedSegments)
            {
                if(Segments.Length == comparedSegments.Length &&
                   SegmentsInCommon(comparedSegments) == Segments.Length)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        } 

    }
}
