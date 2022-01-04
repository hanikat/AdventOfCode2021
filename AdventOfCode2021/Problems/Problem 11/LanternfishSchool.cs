using System;
using System.Collections.Generic;

namespace AdventOfCode2021.Problems.Problem11
{
    public class LanternfishSchool
    {
        List<Lanternfish> _lanternfishes;

        public LanternfishSchool(List<int> initialSpawnDays)
        {
            _lanternfishes = new List<Lanternfish>();

            foreach(var spawnDay in initialSpawnDays)
            {
                _lanternfishes.Add(new Lanternfish(spawnDay));
            }
        }

        public void IncrementByOneDay()
        {
            List<Lanternfish> newLanternfishes = new();
            foreach(var lanternfish in _lanternfishes)
            {
                if(lanternfish.DaysUntilSpawn != 0)
                {
                    lanternfish.DaysUntilSpawn = lanternfish.DaysUntilSpawn - 1;
                }
                else
                {
                    lanternfish.DaysUntilSpawn = 6;
                    newLanternfishes.Add(new Lanternfish(8));
                }
            }

            _lanternfishes.AddRange(newLanternfishes);
        }


        public int NumberOfLanternfish
        {
            get
            {
                return _lanternfishes.Count;
            }
        }

        private class Lanternfish
        {
            private int _minDaysUntilRespawn = 0;
            private int _maxDaysUntilRespawn = 8;
            private int _daysUntilRespawn;
            public Lanternfish(int daysUntilSpawn)
            {
                DaysUntilSpawn = daysUntilSpawn;
            }

            public int DaysUntilSpawn {
                get
                {
                    return _daysUntilRespawn;
                }
                set
                {
                    if (value >= _minDaysUntilRespawn && value <= _maxDaysUntilRespawn)
                    {
                        _daysUntilRespawn = value;
                    }
                    else
                    {
                        throw new Exception($"Invalid argument. The number of days until next respawn is out of range: {value}");
                    }
                }
            }
        }
    }
}
