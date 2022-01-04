using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2021.Problems.Problem24
{
    public class Path : IEquatable<Path>
    {
        List<Cave> _path;

        public Path()
        {
            _path = new();
        }

        public bool HasReachedEnd
        {
            get
            {
                if (_path.Count > 0)
                {
                    return _path.Last().Name == "end";
                }
                else
                {
                    return false;
                }
            }
        }


        private bool HasTraversedCave(Cave cave)
        {
            if (_path.Count(c => c.Name == cave.Name) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool HasTraversedSmallCaveTwice()
        {
            var namesOfTraversedSmallCaves = _path.Where(c => !c.IsLargeCave).Select(c => c.Name);

            if (namesOfTraversedSmallCaves.Count() != namesOfTraversedSmallCaves.Distinct().Count())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CaveCanBeTraversed(Cave cave)
        {
            if (cave.Name == "start" ||
                (!cave.IsLargeCave &&
                HasTraversedSmallCaveTwice() &&
                HasTraversedCave(cave)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void AddCaveToPath(Cave cave)
        {
            _path.Add(cave);
        }

        private Cave GetLastCaveInPath()
        {
            return _path.Last();
        }

        public bool Equals(Path other)
        {
            return ToString() == other.ToString();
        }

        public override string ToString()
        {
            var path = "";
            foreach (var cave in _path)
            {
                path += cave.Name + ",";
            }
            path = path.Remove(path.Length - 1, 1);

            return path;
        }

        public bool IsStuck()
        {
            List<Cave> nextCaves = GetLastCaveInPath().ConnectedCaves;

            if (!HasReachedEnd && nextCaves.TrueForAll(c => !CaveCanBeTraversed(c)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Path> FindPathExtensions()
        {
            if(!HasReachedEnd)
            {
                List<Path> paths = new();
                List<Cave> connectedCavesThatCanBeTraversed = GetLastCaveInPath().ConnectedCaves.Where(c => CaveCanBeTraversed(c)).ToList();


                foreach (var nextCave in connectedCavesThatCanBeTraversed)
                {
                    Path newPath = new();
                    foreach (var caveInPath in _path)
                    {
                        newPath.AddCaveToPath(caveInPath);
                    }
                    newPath.AddCaveToPath(nextCave);
                    paths.Add(newPath);
                }

                return paths;
            }
            else
            {
                return new();
            }
        }
    }
}
