using System;
namespace AdventOfCode2021.Problems.Problem9
{
    public class Grid
    {
        private int _width = 0;
        private int _height = 0;
        private GridPoint[,] _grid;
        private bool _allowDiagonalLines;

        public Grid(int width, int height, bool allowDiagonalLines)
        {
            _width = width + 1;
            _height = height + 1;
            _allowDiagonalLines = allowDiagonalLines;

            _grid = new GridPoint[_width,_height];

            for(int x = 0; x < _width; x++)
            {
                for(int y = 0; y < _height; y++)
                {
                    _grid[x, y] = new GridPoint();
                }
            }

        }

        public void AddLineToGrid(Line line)
        {
            if(!line.IsDiagonalLine)
            {
                AddNonDiagonalLineToGrid(line);
            }
            else if(_allowDiagonalLines)
            {
                AddDiagonalLineToGrid(line);
            }
        }

        private void AddNonDiagonalLineToGrid(Line line)
        {
            if(line.IsVerticalLine)
            {
                int x = line.StartPoint.Item1;
                for (int y = line.StartPoint.Item2; y <= line.EndPoint.Item2; y++)
                {
                    _grid[x, y].IncrementIntersections();
                }
            }
            else if(line.IsHorizontalLine)
            {
                int y = line.StartPoint.Item2;
                for (int x = line.StartPoint.Item1; x <= line.EndPoint.Item1; x++)
                {
                    _grid[x, y].IncrementIntersections();
                }
            }
        }

        private void AddDiagonalLineToGrid(Line line)
        {
            if(line.IsDiagonalLine)
            {
                int x = line.StartPoint.Item1;
                int y = line.StartPoint.Item2;

                // get correct direction for line 
                int xOrientation = x - line.EndPoint.Item1 < 0 ? 1 : -1;
                int yOrientation = y - line.EndPoint.Item2 < 0 ? 1 : -1;

                int i = -1;
                do
                {
                    i++;
                    _grid[x + (i * xOrientation), y + (i * yOrientation)].IncrementIntersections();
                } while ((i * xOrientation) + x != line.EndPoint.Item1);

            }
        }

        public int GetNumberOfOverlaps()
        {
            int pointsWithMultipleLines = 0;
            foreach(var gridPoint in _grid)
            {
                if(gridPoint.GetNumberOfInstersections() > 1)
                {
                    pointsWithMultipleLines++;
                }
            }

            return pointsWithMultipleLines;
        }

        private class GridPoint
        {
            private int _linesIntersectingPoint = 0;
            public GridPoint()
            {

            }

            public void IncrementIntersections()
            {
                _linesIntersectingPoint++;
            }

            public int GetNumberOfInstersections()
            {
                return _linesIntersectingPoint;
            }
        }
    }
}
