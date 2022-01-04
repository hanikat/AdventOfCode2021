using System;
using System.Collections.Generic;

namespace AdventOfCode2021.Problems.Problem17
{
    public class HeightMap
    {
        private HeightMapPoint[,] _heightMap;

        public HeightMap(List<int> heightPoints, int mapWidth)
        {
            int mapHeight = heightPoints.Count / mapWidth;
            _heightMap = new HeightMapPoint[mapHeight, mapWidth];
            ConstructHeightMap(heightPoints, mapWidth, mapHeight);
            MapNeighbourMapPoints(mapWidth, mapHeight);
        }

        private void MapNeighbourMapPoints(int mapWidth, int mapHeight)
        {
            for (int h = 0; h < mapHeight; h++)
            {
                for (int w = 0; w < mapWidth; w++)
                {
                    if(h > 0)
                    {
                        _heightMap[h, w].LeftNeighbour = _heightMap[h - 1, w];
                    }
                    if(h < mapHeight - 1)
                    {
                        _heightMap[h, w].RightNeighbour = _heightMap[h + 1, w];
                    }
                    if(w > 0)
                    {
                        _heightMap[h, w].UpperNeighbour = _heightMap[h, w - 1];
                    }
                    if(w < mapWidth - 1)
                    {
                        _heightMap[h, w].LowerNeighbour = _heightMap[h, w + 1];
                    }
                }
            }
        }

        private void ConstructHeightMap(List<int> heightPoints, int mapWidth, int mapHeight)
        {
            
            for (int h = 0; h < mapHeight; h++)
            {
                for (int w = 0; w < mapWidth; w++)
                {
                    var heightPointIndex = h * mapWidth + w;
                    _heightMap[h, w] = new HeightMapPoint(heightPoints[heightPointIndex]);
                }
            }
        }

        public int SumOfRiskLevels()
        {
            int sum = 0, index = 0;
            foreach(var heightPoint in _heightMap)
            {
                var riskLevel = heightPoint.RiskLevel();
                if(riskLevel > 0)
                {
                    Console.WriteLine($"Risk point found at: x:{index % 100} y:{(int)(Math.Floor(index / 100.0))}, with height: {heightPoint.Height}");
                }
                sum += riskLevel;
                index++;
            }
            return sum;
        }

        private class HeightMapPoint
        {
            public HeightMapPoint(int height)
            {
                Height = height;
            }

            public int Height { get; private set; }

            public HeightMapPoint LeftNeighbour { get; set; }
            public HeightMapPoint RightNeighbour { get; set; }
            public HeightMapPoint UpperNeighbour { get; set; }
            public HeightMapPoint LowerNeighbour { get; set; }

            private bool IsLowPoint()
            {
                bool isLowestPoint = true;

                if(LeftNeighbour != null && LeftNeighbour.Height <= Height)
                {
                    isLowestPoint = false;
                }
                if (RightNeighbour != null && RightNeighbour.Height <= Height)
                {
                    isLowestPoint = false;
                }
                if (UpperNeighbour != null && UpperNeighbour.Height <= Height)
                {
                    isLowestPoint = false;
                }
                if (LowerNeighbour != null && LowerNeighbour.Height <= Height)
                {
                    isLowestPoint = false;
                }

                return isLowestPoint;
            }

            public int RiskLevel()
            {
                if(IsLowPoint())
                {
                    return Height + 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
