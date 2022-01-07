using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Problems.Problem26
{
    public class Paper
    {
        Point[,] _paperPoints;

        public Paper(List<Tuple<int, int>> dotCoordinates)
        {
            int maxWidth = dotCoordinates.Max(c => c.Item1) + 1;
            int maxHeight = dotCoordinates.Max(c => c.Item2) + 1;

            _paperPoints = new Point[maxHeight, maxWidth];

            for(int w = 0; w < maxWidth; w++)
            {
                for(int h = 0; h < maxHeight; h++)
                {
                    _paperPoints[h, w] = new Point(
                        dotCoordinates.Any(c => c.Item1 == w && c.Item2 == h)
                    );
                }
            }
        }

        public int CountDots { 
            get
            {
                int sum = 0;
                foreach(var point in _paperPoints)
                {
                    if(point.HasDot)
                    {
                        sum++;
                    }
                }
                return sum;
            }
        }

        public string PrintDots()
        {
            string line = Environment.NewLine;
            for (int h = 0; h < _paperPoints.GetLength(0); h++)
            {
                
                for (int w = 0; w < _paperPoints.GetLength(1); w++)
                {
                    if (_paperPoints[h, w].HasDot) {
                        line += "#";
                    } 
                    else
                    {
                        line += ".";
                    }
                }
                line += Environment.NewLine;
            }
            return line;
        }

        public void PerformFoldInstruction(FoldInstruction foldInstruction)
        {
            // calculate new height and width of paper
            int newHeight = foldInstruction.Instruction.Equals(FoldInstructions.FoldAlongY) ? foldInstruction.LineNumber : _paperPoints.GetLength(0);
            int newWidth = foldInstruction.Instruction.Equals(FoldInstructions.FoldAlongX) ? foldInstruction.LineNumber : _paperPoints.GetLength(1);

            Point[,] newPaperPoints = new Point[newHeight, newWidth];

            // recreate the old paper which is not folded
            for (int w = 0; w < newWidth; w++)
            {
                for (int h = 0; h < newHeight; h++)
                {
                    newPaperPoints[h, w] = _paperPoints[h, w];
                }
            }


            if(foldInstruction.Instruction.Equals(FoldInstructions.FoldAlongY))
            {
                int startIndex = newHeight >= _paperPoints.GetLength(0) - newHeight ? _paperPoints.GetLength(0) - 1 : 2 * newHeight;
                for (int w = 0; w < newWidth; w++)
                {
                    for (int h = startIndex, i = 0; h > foldInstruction.LineNumber; h--, i++)
                    {
                        if (_paperPoints[h, w].HasDot)
                        {
                            newPaperPoints[i, w].HasDot = true;
                        }
                    }
                }

                _paperPoints = newPaperPoints;
            }
            else if(foldInstruction.Instruction.Equals(FoldInstructions.FoldAlongX))
            {
                int startIndex = newWidth >= _paperPoints.GetLength(1) - newWidth ? _paperPoints.GetLength(1) - 1: 2 * newWidth;
                for (int w = startIndex, i = 0; w > foldInstruction.LineNumber; w--, i++)
                {
                    for (int h = 0; h < newHeight; h++)
                    {
                        if (_paperPoints[h, w].HasDot)
                        {
                            newPaperPoints[h, i].HasDot = true;
                        }
                    }
                }

                _paperPoints = newPaperPoints;
            }
        }

        private class Point {

            public Point(bool hasDot)
            {
                HasDot = hasDot;
            }

            public bool HasDot { get; set; }
        }
    }
}
