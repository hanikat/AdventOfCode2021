using System;
using System.Collections.Generic;

namespace AdventOfCode2021.Problems.Problem21
{
    public class DumboOctupusSchool
    {
        private DumboOctupus[,] _octupusSchool;

        public DumboOctupusSchool(List<int> energyLevels, int schoolWidth)
        {
            var schoolHeight = energyLevels.Count / schoolWidth;
            CreateOctopusSchool(energyLevels, schoolHeight, schoolWidth);
            RegisterAdjacentOctupuses(energyLevels.Count / schoolWidth, schoolWidth);
        }

        private void CreateOctopusSchool(List<int> energyLevels, int schoolHeight, int schoolWidth)
        {
            _octupusSchool = new DumboOctupus[schoolHeight, schoolWidth];

            for(int h = 0; h < schoolHeight; h++)
            {
                for(int w = 0; w < schoolWidth; w++)
                {
                    var index = h * schoolWidth + w;
                    _octupusSchool[h, w] = new DumboOctupus(energyLevels[index], Tuple.Create(w, h)) ;
                }
            }
        }

        private void RegisterAdjacentOctupuses(int schoolHeight, int schoolWidth)
        {
            for (int h = 0; h < schoolHeight; h++)
            {
                for (int w = 0; w < schoolWidth; w++)
                {
                    List<DumboOctupus> adjacentOctupuses = new();

                    // register all octupuses which are on the line above in the grid
                    if(h > 0)
                    {
                        adjacentOctupuses.Add(_octupusSchool[h - 1, w]);
                        if(w > 0)
                        {
                            adjacentOctupuses.Add(_octupusSchool[h - 1, w - 1]);
                        }
                        if(w < schoolWidth - 1)
                        {
                            adjacentOctupuses.Add(_octupusSchool[h - 1, w + 1]);
                        }
                    }

                    // register all octupuses to the left and right in the grid
                    if(w > 0)
                    {
                        adjacentOctupuses.Add(_octupusSchool[h, w - 1]);
                    }
                    if(w < schoolWidth - 1)
                    {
                        adjacentOctupuses.Add(_octupusSchool[h, w + 1]);
                    }

                    // register all octupuses which are on the line below in the grid
                    if(h < schoolHeight - 1)
                    {
                        adjacentOctupuses.Add(_octupusSchool[h + 1, w]);
                        if (w > 0)
                        {
                            adjacentOctupuses.Add(_octupusSchool[h + 1, w - 1]);
                        }
                        if (w < schoolWidth - 1)
                        {
                            adjacentOctupuses.Add(_octupusSchool[h + 1, w + 1]);
                        }
                    }

                    _octupusSchool[h, w].AdjacentOcupuses = adjacentOctupuses;
                }
            }
        }

        public int CountFlashesAfterSteps(int steps)
        {
            for(int i = 0; i < steps; i++)
            {
                List<DumboOctupus> flashingOctupuses = new();

                // step forward
                foreach(var octupus in _octupusSchool)
                {
                    octupus.StepForward();
                    if(octupus.FlashingInThisStep)
                    {
                        flashingOctupuses.Add(octupus);
                    }
                }

                // handle flashes
                while(flashingOctupuses.Count > 0)
                {
                    List<DumboOctupus> newFlashingOctupuses = new();
                    foreach(var octupus in flashingOctupuses)
                    {
                        foreach(var adjacentOctupus in octupus.AdjacentOcupuses)
                        {
                            if(!adjacentOctupus.FlashingInThisStep)
                            {
                                adjacentOctupus.EnergyLevel++;
                                if(adjacentOctupus.FlashingInThisStep)
                                {
                                    newFlashingOctupuses.Add(adjacentOctupus);
                                }
                            }
                        }
                    }
                    flashingOctupuses = newFlashingOctupuses;
                }
            }

            int totalFlashes = 0;

            foreach(var octupus in _octupusSchool)
            {
                totalFlashes += octupus.Flashes;
            }

            return totalFlashes;
        }

        private class DumboOctupus
        {
            private int _energyLevel;

            public DumboOctupus(int energyLevel, Tuple<int,int> coordinates)
            {
                _energyLevel = energyLevel;
                FlashingInThisStep = false;
                Cooridnates = coordinates;
            }

            public Tuple<int,int> Cooridnates { get; set; }

            public int EnergyLevel {
                get
                {
                    return _energyLevel;
                }
                set
                {
                    if(value > 9 && !FlashingInThisStep)
                    {
                        Flash();
                        _energyLevel = 0;
                    }
                    else if(!FlashingInThisStep)
                    {
                        _energyLevel = value;
                    }
                }
            }

            public override string ToString()
            {
                return _energyLevel.ToString();
            }

            public void StepForward()
            {
                FlashingInThisStep = false;
                EnergyLevel++;
            }

            public bool FlashingInThisStep { get; private set; }

            public int Flashes { get; private set; }

            public List<DumboOctupus> AdjacentOcupuses { get; set; }

            private void Flash()
            {
                FlashingInThisStep = true;
                Flashes++;
            }
        }
    }
}
