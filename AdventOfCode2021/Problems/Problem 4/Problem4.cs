using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Common;

namespace AdventOfCode2021.Answers.Problems
{
    public class Problem4 : Problem
    {
        public Problem4()
        {
        }

        public override byte ProblemNumber
        {
            get
            {
                return 4;
            }
        }

        public override string ProblemName
        {
            get
            {
                return "Day 2: Dive! - Part Two";
            }
        }

        protected override string GetSolution()
        {
            List<Tuple<string, int>> commands = GetCommandList(Utility.SplitStringByLine(_input));

            Tuple<int, int> endPosition = CalculatePositionFromCommands(0, 0, 0, commands);

            return (endPosition.Item1 * endPosition.Item2).ToString();
        }

        private List<Tuple<string,int>> GetCommandList(string[] commandStrings)
        {
            List<Tuple<string, int>> commands = new List<Tuple<string,int>>();

            foreach(var commandString in commandStrings)
            {
                var commandOptions = commandString.Split(' ');

                if(commandOptions.Length != 2)
                {
                    throw new Exception($"The passed command does not have a valid amount of arguments: {commandOptions.Length}");
                }
                else
                {
                    int positionChange;
                    if(!int.TryParse(commandOptions.ElementAt(1), out positionChange))
                    {
                        throw new Exception($"The second argument of the command was not an integer: {commandString}");
                    }
                    else
                    {
                        commands.Add(Tuple.Create(commandOptions.ElementAt(0), positionChange));
                    }
                }
            }

            return commands;
        }

        private Tuple<int,int> CalculatePositionFromCommands(int horizontalPosition, int depth, int aim, List<Tuple<string,int>> commands)
        {
            if(commands.Count == 0)
            {
                return Tuple.Create(horizontalPosition, depth);
            }
            else
            {
                var currentCommand = commands.ElementAt(0);
                switch (currentCommand.Item1)
                {
                    case "forward":
                        horizontalPosition += currentCommand.Item2;
                        depth += currentCommand.Item2 * aim;
                        break;

                    case "down":
                        aim += currentCommand.Item2;
                        break;

                    case "up":
                        aim -= currentCommand.Item2;
                        break;
                }

                commands.RemoveAt(0);
                return CalculatePositionFromCommands(horizontalPosition, depth, aim, commands);
            }
        }
    }
}
