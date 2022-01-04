using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Problems.Problem18
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
                    _heightMap[h, w] = new HeightMapPoint(heightPoints[heightPointIndex], Tuple.Create(w, h));
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

        public int CalculateScoreOfLargestBasins()
        {
            var top3LargestBasins = FindAllBasins().OrderByDescending(c => c.Count).Take(3);
            int score = 1;
            foreach(var basin in top3LargestBasins)
            {
                score *= basin.Count;
            }
            return score;
        }

        private List<List<HeightMapPoint>> FindAllBasins()
        {
            List<List<HeightMapPoint>> listOfBasins = new();

            foreach(var mapPoint in _heightMap)
            {
                if(mapPoint.RiskLevel() > 0)
                {
                    listOfBasins.Add(FindBasin(mapPoint));
                }
            }

            return listOfBasins;
        }

        private List<HeightMapPoint> FindBasin(HeightMapPoint currentHeightMapPoint)
        {
            List<HeightMapPoint> mapPointsInBasin = new();

            List<HeightMapPoint> mapPointsLeftToTraverse = new();

            if(currentHeightMapPoint.RiskLevel() > 0)
            {
                do
                {
                    mapPointsInBasin.Add(currentHeightMapPoint);
                    mapPointsLeftToTraverse.Remove(currentHeightMapPoint);

                    if (currentHeightMapPoint.LeftNeighbour != null &&
                        currentHeightMapPoint.LeftNeighbour.Height != 9 &&
                        !mapPointsInBasin.Contains(currentHeightMapPoint.LeftNeighbour) &&
                        !mapPointsLeftToTraverse.Contains(currentHeightMapPoint.LeftNeighbour))
                    {
                        mapPointsLeftToTraverse.Add(currentHeightMapPoint.LeftNeighbour);
                    }
                    if (currentHeightMapPoint.RightNeighbour != null &&
                        currentHeightMapPoint.RightNeighbour.Height != 9 &&
                        !mapPointsInBasin.Contains(currentHeightMapPoint.RightNeighbour) &&
                        !mapPointsLeftToTraverse.Contains(currentHeightMapPoint.RightNeighbour))
                    {
                        mapPointsLeftToTraverse.Add(currentHeightMapPoint.RightNeighbour);
                    }
                    if (currentHeightMapPoint.UpperNeighbour != null &&
                        currentHeightMapPoint.UpperNeighbour.Height != 9 &&
                        !mapPointsInBasin.Contains(currentHeightMapPoint.UpperNeighbour) &&
                        !mapPointsLeftToTraverse.Contains(currentHeightMapPoint.UpperNeighbour))
                    {
                        mapPointsLeftToTraverse.Add(currentHeightMapPoint.UpperNeighbour);
                    }
                    if (currentHeightMapPoint.LowerNeighbour != null &&
                        currentHeightMapPoint.LowerNeighbour.Height != 9 &&
                        !mapPointsInBasin.Contains(currentHeightMapPoint.LowerNeighbour) &&
                        !mapPointsLeftToTraverse.Contains(currentHeightMapPoint.LowerNeighbour))
                    {
                        mapPointsLeftToTraverse.Add(currentHeightMapPoint.LowerNeighbour);
                    }

                    currentHeightMapPoint = mapPointsLeftToTraverse.FirstOrDefault();

                } while (mapPointsLeftToTraverse.Count > 0);
                
            }

            return mapPointsInBasin;
        }

        private class HeightMapPoint : IEquatable<HeightMapPoint>
        {
            public HeightMapPoint(int height, Tuple<int,int> coordinates)
            {
                Height = height;
                Coordinates = coordinates;
            }

            public int Height { get; private set; }

            public Tuple<int, int> Coordinates { get; private set; }

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

            public bool Equals(HeightMapPoint other)
            {
                if(other.Coordinates.Item1 == Coordinates.Item1 &&
                   other.Coordinates.Item2 == Coordinates.Item2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
