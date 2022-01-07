using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Problems.Problem26
{
    public enum FoldInstructions
    {
        FoldAlongX,
        FoldAlongY
    }

    public class FoldInstruction
    {
        public FoldInstruction(FoldInstructions foldInstruction, int lineNumber)
        {
            Instruction = foldInstruction;
            LineNumber = lineNumber;
        }

        public FoldInstructions Instruction { get; private set; }

        public int LineNumber { get; private set; }
    }
}
