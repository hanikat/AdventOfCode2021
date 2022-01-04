using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode2021.Common;
using AdventOfCode2021.Problems.Problem12;
using AdventOfCode2021.Problems.Problem13;

namespace AdventOfCode2021.Answers.Problems
{
    public class Problem13 : Problem
    {
        public Problem13()
        {
        }

        public override byte ProblemNumber
        {
            get
            {
                return 13;
            }
        }

        public override string ProblemName
        {
            get
            {
                return "Day 7: The Treachery of Whales";
            }
        }

        protected override string GetSolution()
        {
            var initialPositions = ParseInitialPositionFromInput();

            var crabSwarm = new CrabSwarm(initialPositions);
            var positionWithLowestFuelCost = crabSwarm.FindAlignedSwarmPositionWithLowestFuelCost();
            var lowestSwarmAlignementFuelCost = crabSwarm.FuelCostToAlignSwarmToPosition(positionWithLowestFuelCost);

            return lowestSwarmAlignementFuelCost.ToString();
            
        }

        private List<int> ParseInitialPositionFromInput()
        {
            List<int> initialDaysToSpawn = new();

            var listOfInitialDaysAsStrings = _input.Split(",");

            int numberParsed;
            foreach(var numberAsString in listOfInitialDaysAsStrings)
            {
                if(int.TryParse(numberAsString, out numberParsed))
                {
                    initialDaysToSpawn.Add(numberParsed);
                }
                else
                {
                    throw new Exception($"Unable to parse number from input string. Attempted to parse: {numberAsString}");
                }
            }

            return initialDaysToSpawn;
        }

    }
}
