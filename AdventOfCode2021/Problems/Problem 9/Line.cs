using System;
namespace AdventOfCode2021.Problems.Problem9
{
    public class Line
    {
        private Tuple<int, int> _startPoint;
        private Tuple<int, int> _endPoint;

        public Line(Tuple<int,int> startPoint, Tuple<int,int> endPoint)
        {
            if((startPoint.Item1 > endPoint.Item1 && startPoint.Item2 == endPoint.Item2)
                || (startPoint.Item2 > endPoint.Item2 && startPoint.Item1 == endPoint.Item1))
            {
                _startPoint = endPoint;
                _endPoint = startPoint;
            }
            else
            {
                _startPoint = startPoint;
                _endPoint = endPoint;
            }
        }

        public bool IsHorizontalLine
        {
            get
            {
                return StartPoint.Item2 == EndPoint.Item2;
            }
        }

        public bool IsVerticalLine
        {
            get
            {
                return StartPoint.Item1 == EndPoint.Item1;
            }
        }

        public bool IsDiagonalLine
        {
            get
            {
                return !IsHorizontalLine && !IsVerticalLine;
            }
        }

        public Tuple<int,int> StartPoint {
            get
            {
                return _startPoint;
            }
        }

        public Tuple<int,int> EndPoint {
            get
            {
                return _endPoint;
            }

        }
    }
}
