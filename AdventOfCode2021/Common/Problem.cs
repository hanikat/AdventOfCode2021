using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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

        public void GetInputFromFile()
        {
            string expectedFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"/Input/Problem{ProblemNumber}-input.txt");
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
            string resultFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"/Answers/Problem{ProblemNumber}-answer.txt");
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

        public static IEnumerable<T> EnumerateProblems<T>() where T : Problem
        {
            List<T> problems = new List<T>();
            foreach (Type type in
                Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
            {
                problems.Add((T)Activator.CreateInstance(type));
            }
            problems.OrderBy(p => p.ProblemNumber);
            return problems;
        }
    }
}
