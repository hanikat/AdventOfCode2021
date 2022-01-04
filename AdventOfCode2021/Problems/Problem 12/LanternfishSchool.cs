using System;
using System.Collections.Generic;

namespace AdventOfCode2021.Problems.Problem12
{
    public class LanternfishSchool
    {
        private int _maxSpawnDays = 8;
        private int _minSpawnDays = 0;
        private int _resetSpawnDays = 6;

        // use dictionary to group fishes by number of days left until new spawn
        private Dictionary<int, ulong> _numberOfLanternfishesBySpawnDays;


        public LanternfishSchool(List<ulong> initialSpawnDays)
        {
            _numberOfLanternfishesBySpawnDays = new();

            // create one dictionary entry for each possible spawn day
            for(int i = 0; i <= _maxSpawnDays; i++)
            {
                _numberOfLanternfishesBySpawnDays.Add(i, (ulong)initialSpawnDays.FindAll(d => d == (ulong)i).Count);
            }

        }

        public void IncrementByOneDay()
        {
            var numberOfRespawn = _numberOfLanternfishesBySpawnDays[_minSpawnDays];
            for(int i = 1; i <= _maxSpawnDays; i++)
            {
                if(i - 1 == _resetSpawnDays)
                {
                    _numberOfLanternfishesBySpawnDays[i - 1] = _numberOfLanternfishesBySpawnDays[i] + numberOfRespawn;
                }
                else
                {
                    _numberOfLanternfishesBySpawnDays[i - 1] = _numberOfLanternfishesBySpawnDays[i];
                }   
            }
            _numberOfLanternfishesBySpawnDays[_maxSpawnDays] = numberOfRespawn;
        }

        public ulong NumberOfLanternfish
        {
            get
            {
                ulong sum = 0;
                foreach(var entry in _numberOfLanternfishesBySpawnDays)
                {
                    sum += entry.Value;
                }

                return sum;
            }
        }
    }
}
