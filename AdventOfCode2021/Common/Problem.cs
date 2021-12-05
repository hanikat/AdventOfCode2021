using System;
using System.IO;

namespace AdventOfCode2021.Common
{
    public abstract class Problem
    {
        public Problem()
        {
        }

        protected string _input;
        protected string _result;

        protected abstract string GetSolution();

        /// <summary>
        /// Solves the problem. Requires that the input for the problem has been set.
        /// </summary>
        public void Solve()
        {
            if(String.IsNullOrWhiteSpace(_input))
            {
                throw new Exception("No input was given to the problem.");
            }

            _result = GetSolution();

            if(String.IsNullOrWhiteSpace(_result))
            {
                throw new Exception("Unexpected answer to solution. The output was null or whitespace.");
            }
            else
            {
                WriteAnswerToFile(_result);
            }
        }

        /// <summary>
        /// Get the result to the solved problem. Requires that the problem has been solved.
        /// </summary>
        /// <returns>The answer to the solved problem.</returns>
        public string GetAnswer()
        {
            if (String.IsNullOrWhiteSpace(_result))
            {
                throw new Exception("Unexpected answer to solution. The output was null or whitespace.");
            }
            else
            {
                return _result;
            }
        }

        /// <summary>
        /// Loads the input to the problem from local file in the Input folder.
        /// </summary>
        public void GetInputFromFile()
        {
            string currentDirectoryPath = Directory.GetCurrentDirectory();
            string solutionDirectoryPath = currentDirectoryPath.Substring(0, currentDirectoryPath.LastIndexOf("/bin"));
            string expectedFilePath = Path.Combine(solutionDirectoryPath, $"Input/Problem{ProblemNumber}-input.txt");
            if(!File.Exists(expectedFilePath))
            {
                throw new Exception($"The expected input file was not found at path: {expectedFilePath}");
            }
            else
            {
                string inputFileContent = File.ReadAllText(expectedFilePath);
                if(String.IsNullOrWhiteSpace(inputFileContent))
                {
                    throw new Exception($"The input file has no content. Input file was found at path:; {expectedFilePath}");
                }
                else
                {
                    _input = inputFileContent;
                }
            }
        }

        /// <summary>
        /// Sets the input for the problem.
        /// </summary>
        /// <param name="input">The value to set the problems input to.</param>
        public void SetInput(string input)
        {
            if(String.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Empty input to the problem is not allowed!");
            }
            else
            {
                _input = input;
            }
        }

        private void WriteAnswerToFile(string answer)
        {
            string currentDirectoryPath = Directory.GetCurrentDirectory();
            string solutionDirectoryPath = currentDirectoryPath.Substring(0, currentDirectoryPath.LastIndexOf("/bin"));
            string resultFilePath = Path.Combine(solutionDirectoryPath, $"Answers/Problem{ProblemNumber}-answer.txt");
            File.WriteAllText(resultFilePath, answer, System.Text.Encoding.UTF8);
        }

        public abstract byte ProblemNumber
        {
            get;
        }

        public abstract string ProblemName
        {
            get;
        }
    }
}
