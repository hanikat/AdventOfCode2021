using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Common;
using AdventOfCode2021.Problems.Problem23;

namespace AdventOfCode2021.Answers.Problems
{
    public class Problem23 : Problem
    {
        public Problem23()
        {
        }

        public override byte ProblemNumber
        {
            get
            {
                return 23;
            }
        }

        public override string ProblemName
        {
            get
            {
                return "Day 12: Passage Pathing";
            }
        }

        protected override string GetSolution()
        {
            var inputLines = Utility.SplitStringByLine(_input).ToList();
            var caves = ModelCaveNetwork(inputLines);

            List<Path> paths = new();
            var initialPath = new Path();
            initialPath.AddCaveToPath(caves.Single(c => c.Name == "start"));

            paths.Add(initialPath);
            bool allPathsHasReachedEnd = false;

            while(!allPathsHasReachedEnd)
            {
                List<Path> newPaths = new();
                List<Path> pathsToRemove = new();
                foreach(var path in paths.Where(c => !c.HasReachedEnd))
                {
                    var newExtendedPaths = path.FindPathExtensions();
                    newPaths.AddRange(newExtendedPaths);
                    pathsToRemove.Add(path);
                }

                paths.AddRange(newPaths);
                pathsToRemove.AddRange(paths.Where(p => p.IsStuck()));
                paths.RemoveAll(p => pathsToRemove.Contains(p));

                allPathsHasReachedEnd = paths.TrueForAll(p => p.HasReachedEnd);
            }

            return paths.Count.ToString();
        }

        private List<Cave> ModelCaveNetwork(List<string> paths)
        {
            List<string> caveNames = new();
            paths.ForEach(p => p.Split('-').ToList().ForEach(n => caveNames.Add(n)));


            List<Cave> caves = new();
            foreach (var caveName in caveNames.Distinct())
            {
                if (caveName.ToList().TrueForAll(c => Char.IsUpper(c)))
                {
                    var largeCave = new Cave(caveName, true);
                    caves.Add(largeCave);
                }
                else
                {
                    var smallCave = new Cave(caveName, false);
                    caves.Add(smallCave);
                }
            }

            foreach(var path in paths)
            {
                var connectedCaves = path.Split('-');
                if(connectedCaves.Length != 2)
                {
                    throw new Exception("Unexpected number of paths on the same line.");
                }

                var firstCave = caves.Single(c => c.Name == connectedCaves[0]);
                var secondCave = caves.Single(c => c.Name == connectedCaves[1]);

                firstCave.ConnectedCaves.Add(secondCave);
                secondCave.ConnectedCaves.Add(firstCave);
            }

            return caves;
        }
    }
}
