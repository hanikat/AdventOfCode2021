using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode2021.Common;
using AdventOfCode2021.Problems.Problem7;
using AdventOfCode2021.Problems.Problem9;

namespace AdventOfCode2021.Answers.Problems
{
    public class Problem10 : Problem
    {
        public Problem10()
        {
        }

        public override byte ProblemNumber
        {
            get
            {
                return 10;
            }
        }

        public override string ProblemName
        {
            get
            {
                return "Day 5: Hydrothermal Venture - Part Two";
            }
        }

        protected override string GetSolution()
        {
            var lines = ParseLinesFromInput();
            int maxWidthCoordinate = Math.Max(lines.Max(l => l.StartPoint.Item1), lines.Max(l => l.EndPoint.Item1));
            int maxHeightCoordinate = Math.Max(lines.Max(l => l.StartPoint.Item2), lines.Max(l => l.EndPoint.Item2));

            var grid = new Grid(maxHeightCoordinate, maxHeightCoordinate, true);
            foreach(var line in lines)
            {
                grid.AddLineToGrid(line);
            }

            return grid.GetNumberOfOverlaps().ToString();
        }

        private List<Line> ParseLinesFromInput()
        {
            List<Line> lines = new();

            var trimmedInputLines = _input.Replace(" ", "")
                .Replace("->", ",")
                .Split(Environment.NewLine);

            foreach(var inputLine in trimmedInputLines)
            {
                var coordintaes = inputLine.Split(",");
                if(coordintaes.Length == 4)
                {
                    int xStartPoint = 0;
                    int yStartPoint = 0;
                    int xEndPoint = 0;
                    int yEndPoint = 0;

                    if(int.TryParse(coordintaes[0], out xStartPoint) &&
                       int.TryParse(coordintaes[1], out yStartPoint) &&
                       int.TryParse(coordintaes[2], out xEndPoint) &&
                       int.TryParse(coordintaes[3], out yEndPoint))
                    {
                        Tuple<int, int> startPoint = Tuple.Create(xStartPoint, yStartPoint);
                        Tuple<int, int> endPoint = Tuple.Create(xEndPoint, yEndPoint);

                        lines.Add(new Line(startPoint, endPoint));
                    }
                    else
                    {
                        throw new Exception("Could not parse coordinates from input line.");
                    }
                }
                else
                {
                    throw new Exception("Could not parse input. Unexpected number of coordinates on same line.");
                }
            }


            return lines;
        }
    }
}
