using System;
using AdventOfCode2021.Answers.Problems;
using AdventOfCode2021.Common;
using System.Linq;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Advent of Code 2021 problem solver.");
            Console.WriteLine("For a complete list of the problems targeted by this application, see: https://adventofcode.com/2021");
            Console.WriteLine("----- ----- ------");


            Console.WriteLine("Please choose which problem to solve:");

            var problems = Problem.EnumerateProblems<Problem>();
            byte i = 0;

            foreach (var problem in problems)
            {
                Console.WriteLine($"{++i} - {problem.ProblemName}");
            }


            int selectedNumber;
            while (true)
            {
                Console.Write("Enter the number of the problem to solve: ");
                var input = Console.ReadLine();
                

                if (int.TryParse(input, out selectedNumber))
                {
                    if(selectedNumber > i || selectedNumber < 1)
                    {
                        Console.WriteLine($"The selected number is out of bounds. Please specify a number between 1 and {i}.");
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("The provided input was not a valid number. Try Again.");
                }
            }

            var problemToSolve = problems.ElementAt(selectedNumber - 1);

            while (true)
            {
                Console.WriteLine("Which input method would you like to use?");
                Console.WriteLine("1: File input");
                Console.WriteLine("2: Command line input");
                var input = Console.ReadLine();

                if (int.TryParse(input, out selectedNumber))
                {
                    if (selectedNumber == 1 || selectedNumber == 2)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"The selected number is out of bounds. Please specify a number between 1 and 2.");
                    }
                }
                else
                {
                    Console.WriteLine("The provided input was not a valid number. Try Again.");
                }
            }

            if(selectedNumber == 1)
            {
                problemToSolve.GetInputFromFile();
            }
            else
            {
                Console.WriteLine("Provide the input for the problem:");
                var input = Console.ReadLine();
                problemToSolve.SetInput(input);
            }

            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            problemToSolve.Solve();
            watch.Stop();

            Console.WriteLine($"The problem was solved in {watch.ElapsedMilliseconds} ms");
            Console.WriteLine($"The solution to the problem is: ${problemToSolve.GetAnswer()}");
        }
    }
}
