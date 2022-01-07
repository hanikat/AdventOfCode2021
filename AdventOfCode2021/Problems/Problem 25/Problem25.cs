using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdventOfCode2021.Common;
using AdventOfCode2021.Problems.Problem25;

namespace AdventOfCode2021.Answers.Problems25
{
    public class Problem25 : Problem
    {
        public Problem25()
        {
        }

        public override byte ProblemNumber
        {
            get
            {
                return 25;
            }
        }

        public override string ProblemName
        {
            get
            {
                return "Day 13: Transparent Origami";
            }
        }

        private List<string> _inputLines;

        protected override string GetSolution()
        {
            List<Tuple<int, int>> dotCoordinates = ParseDotCoordinatesFromInput();
            List<FoldInstruction> foldInstructions = ParseFoldInstructionsFromInput();

            Paper paper = new(dotCoordinates);
            paper.PerformFoldInstruction(foldInstructions.First());

            return paper.CountDots.ToString();
        }

        private List<FoldInstruction> ParseFoldInstructionsFromInput()
        {
            List<string> inputLines = GetInputLines();
            int firstFoldInstructionIndex = GetEmptyLineIndex() + 1;

            List<FoldInstruction> instructions = new();

            for(int i = firstFoldInstructionIndex; i < inputLines.Count; i++)
            {
                var instructionAndLine = inputLines[i].Split('=');
                int lineNumber;

                if(!int.TryParse(instructionAndLine[1], out lineNumber))
                {
                    throw new Exception($"Invalid line number found when parsing instruction on line {i+1}: {inputLines[i]}");
                }

                if (instructionAndLine[0].Contains("fold along x"))
                {
                    instructions.Add(new FoldInstruction(FoldInstructions.FoldAlongX, lineNumber));
                }
                else if(instructionAndLine[0].Contains("fold along y"))
                {
                    instructions.Add(new FoldInstruction(FoldInstructions.FoldAlongY, lineNumber));
                }
                else
                {
                    throw new Exception($"Invalid instruction line found on line {i+1}: {inputLines[i]}");
                }
            }

            return instructions;
        }

        private List<Tuple<int, int>> ParseDotCoordinatesFromInput()
        {
            List<string> inputLines = GetInputLines();
            int lastCoordinateIndex = GetEmptyLineIndex() - 1;

            List<Tuple<int, int>> coordinates = new();

            for (int i = 0; i <= lastCoordinateIndex; i++)
            {
                int xCoordinate, yCoordinate;
                var coordinatesAsStrings = inputLines[i].Split(',');
                if (coordinatesAsStrings.Length == 2 && int.TryParse(coordinatesAsStrings[0], out xCoordinate) && int.TryParse(coordinatesAsStrings[1], out yCoordinate))
                {
                    coordinates.Add(Tuple.Create(xCoordinate, yCoordinate));
                }
                else
                {
                    throw new Exception($"Error parsing coordinates from line {i + 1}: {inputLines[i]}");
                }
            }

            return coordinates;
        }

        private int GetEmptyLineIndex()
        {
            var inputLines = GetInputLines();
            int emptyLineIndex = inputLines.FindIndex(l => String.IsNullOrWhiteSpace(l));

            return emptyLineIndex;
        }

        private List<string> GetInputLines()
        {
            if(_inputLines != null)
            {
                return _inputLines;
            }
            else
            {
                _inputLines = _input.Split(Environment.NewLine).ToList();
                return _inputLines;
            }
        }
    }
}
