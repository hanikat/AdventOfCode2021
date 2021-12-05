using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AdventOfCode2021.Common
{
    public static class Utility
    {
        public static string[] SplitStringByLine(string input)
        {
            return input.Split(Environment.NewLine, StringSplitOptions.None);
        }

        public static List<int> ConvertStringsToInt(string[] strings)
        {
            List<int> listOfIntegers = new List<int>();

            foreach (var str in strings)
            {
                int integerValue;
                if (!int.TryParse(str, out integerValue))
                {
                    throw new Exception($"Expected integer, but value was: {str}");
                }
                else
                {
                    listOfIntegers.Add(integerValue);
                }
            }

            return listOfIntegers;
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
