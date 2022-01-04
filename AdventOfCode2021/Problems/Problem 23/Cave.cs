using System;
using System.Collections.Generic;

namespace AdventOfCode2021.Problems.Problem23
{
    public class Cave
    {

        public Cave(string name, bool isLargecave)
        {
            Name = name;
            IsLargeCave = isLargecave;
            ConnectedCaves = new();
        }

        public string Name { get; private set; }

        public List<Cave> ConnectedCaves { get; set; }

        public bool IsLargeCave { get; private set; }
    }
}
