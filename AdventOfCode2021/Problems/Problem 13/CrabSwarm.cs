using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Problems.Problem13
{
    public class CrabSwarm
    {
        List<Crab> _crabs = new();

        public CrabSwarm(List<int> initialCrabPositions)
        {
            foreach(var position in initialCrabPositions)
            {
                _crabs.Add(new Crab(position)) ;
            }
        }

        public int FindAlignedSwarmPositionWithLowestFuelCost()
        {
            int maxHorizontalPosition = _crabs.Max(c => c.HorizontalPosition);
            int positionWithLowestFuelCost = -1;
            int lowestFuelCost = int.MaxValue;

            for(int i = 0; i <= maxHorizontalPosition; i++)
            {
                int fuelCostForPosition = FuelCostToAlignSwarmToPosition(i);
                if(fuelCostForPosition < lowestFuelCost)
                {
                    lowestFuelCost = fuelCostForPosition;
                    positionWithLowestFuelCost = i;
                }
            }

            return positionWithLowestFuelCost;
        }

        public int FuelCostToAlignSwarmToPosition(int position)
        {
            int sum = 0;

            foreach(var crab in _crabs)
            {
                sum += Math.Abs(crab.HorizontalPosition - position);
            }

            return sum;
        }

        private class Crab
        {
            public Crab(int position)
            {
                HorizontalPosition = position;
            }

            public int HorizontalPosition { get; set; }
        }
    }
}
