using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdventOfCode2021.Common;
using AdventOfCode2021.Problems.Problem24;

namespace AdventOfCode2021.Answers.Problems
{
    public class Problem24 : Problem
    {
        public Problem24()
        {
        }

        public override byte ProblemNumber
        {
            get
            {
                return 24;
            }
        }

        public override string ProblemName
        {
            get
            {
                return "Day 12: Passage Pathing - Part Two";
            }
        }

        private static readonly bool _useSingleThread = true;

        protected override string GetSolution()
        {
            if(_useSingleThread)
            {
                return GetSolutionSingleThreaded();
            }
            else
            {
                return GetSolutionMultiThreadedAsync().Result;
            }
        }

        private string GetSolutionSingleThreaded()
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
                Console.WriteLine($"Total of {paths.Count} possible paths remaining...");
            }

            return paths.Count.ToString();
        }

        private async Task<string> GetSolutionMultiThreadedAsync()
        {
            var inputLines = Utility.SplitStringByLine(_input).ToList();
            var caves = ModelCaveNetwork(inputLines);

            List<Path> paths = new();
            var initialPath = new Path();
            initialPath.AddCaveToPath(caves.Single(c => c.Name == "start"));

            paths.Add(initialPath);
            bool allPathsHasReachedEnd = false;

            while (!allPathsHasReachedEnd)
            {
                List<Path> newPaths = new();
                List<Path> pathsToRemove = new();
                List<Task<List<Path>>> tasks = new();

                // create a task for each path to continue exploring
                foreach (var path in paths.Where(c => !c.HasReachedEnd))
                {
                    tasks.Add(Task.Factory.StartNew(() => path.FindPathExtensions()));
                    
                    pathsToRemove.Add(path);
                }

                // wait for all tasks to complete
                var f = await Task.WhenAll(tasks.ToArray()).ConfigureAwait(false);


                // compile list of all new paths found
                foreach(var task in tasks)
                {
                    newPaths.AddRange(task.Result);
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
