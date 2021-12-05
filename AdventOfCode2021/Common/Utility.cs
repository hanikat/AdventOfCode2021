using System;
using System.Collections.Generic;

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
    }
}
