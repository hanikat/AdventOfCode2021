using System;
using AdventOfCode2021.Answers.Problems;
using AdventOfCode2021.Common;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            // print program information
            Console.WriteLine("Welcome to Advent of Code 2021 problem solver.");
            Console.WriteLine("For a complete list of the problems targeted by this application, see: https://adventofcode.com/2021");
            Console.WriteLine("----- ----- ------");

            // prompt the user for which problem to solve
            var problems = Problem.EnumerateProblems<Problem>().ToList();
            int problemNumberToSolve = GetSelectedProblem(problems);
            Problem problemToSolve = problems[problemNumberToSolve];

            // prompt user for which input source and input to use
            bool useFileInput = FileInputIsUsed();
            SetProblemInput(problemToSolve, useFileInput);

            // solve the problem and time its execution
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            problemToSolve.Solve();
            watch.Stop();

            // print the result of program execution
            Console.WriteLine($"The problem was solved in {watch.ElapsedMilliseconds} ms");
            Console.WriteLine($"The solution to the problem is: {problemToSolve.GetAnswer()}");
        }


        /// <summary>
        /// Prompts the user for which problem to solve
        /// </summary>
        /// <param name="problems">A ordered list of all available problems which the user can choose to solve.</param>
        /// <returns>The index of the problem to solve</returns>
        private static int GetSelectedProblem(IEnumerable<Problem> problems)
        {
            Console.WriteLine("Please choose which problem to solve:");

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
                    if (selectedNumber > i || selectedNumber < 1)
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

            return selectedNumber - 1;
        }


        /// <summary>
        /// Prompts the user for which input source to use for the problem
        /// </summary>
        /// <returns>returns true if file input is used, returns false if console input is used</returns>
        private static bool FileInputIsUsed()
        {
            int selectedNumber;
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

            return selectedNumber == 1;
        }

        /// <summary>
        /// Sets the input for the problem to solve.
        /// </summary>
        /// <param name="problemToSolve">The problem to be solved.</param>
        /// <param name="useFileInput">Marks wether the input should be taken from file or console.</param>
        private static void SetProblemInput(Problem problemToSolve, bool useFileInput)
        {
            if (useFileInput)
            {
                problemToSolve.GetInputFromFile();
            }
            else
            {
                Console.WriteLine("Provide the input for the problem:");
                var input = Console.ReadLine();
                problemToSolve.SetInput(input);
            }
        }
    }
}
