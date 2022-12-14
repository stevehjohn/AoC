﻿namespace AoC.Visualisations.Visualisations._2022._14;

public class Map
{
    private int[,] _map;

    public int Width { get; private set; }

    public int Height { get; private set; }

    private Random Random { get; } = new();

    public int XMin { get; private set; }

    public void CreateMap(char[,] puzzleMap)
    {
        Width = puzzleMap.GetLength(0);

        Height = puzzleMap.GetLength(1);

        _map = new int[Width, Height];

        XMin = int.MaxValue;

        GetXMin(puzzleMap);

        AddBackgroundBricks();

        AddLines(puzzleMap);
    }

    public int this[int x, int y] => _map[x, y];

    private void GetXMin(char[,] puzzleMap)
    {
        for (var y = 1; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (x > XMin)
                {
                    break;
                }

                if (puzzleMap[x, y] == '#')
                {
                    XMin = x;

                    break;
                }
            }
        }

        XMin -= 10;
    }

    public void CopySand(char[,] puzzleMap)
    {
        for (var y = 1; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (puzzleMap[x, y] == 'o')
                {
                    _map[x - XMin, y] = 'o';
                }
            }
        }
    }

    private void AddLines(char[,] puzzleMap)
    {
        var xMin = int.MaxValue;

        for (var y = 1; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (x > xMin)
                {
                    break;
                }

                if (puzzleMap[x, y] == '#')
                {
                    xMin = x;

                    break;
                }
            }
        }

        xMin -= 10;

        for (var y = 1; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var type = 1 + Random.Next(3) * 6;

                if (puzzleMap[x, y] == '#')
                {
                    if (_map[x - xMin, y - 1] != 0 && _map[x - xMin, y - 1] < 18)
                    {
                        type = _map[x - xMin, y - 1];
                    }
                    if (_map[x - xMin - 1, y] != 0 && _map[x - xMin - 1, y] < 18)
                    {
                        type = _map[x - xMin - 1, y];
                    }

                    _map[x - xMin, y] = type;
                }
            }
        }
    }
    
    private void AddBackgroundBricks()
    {
        for (var y = 1; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (Random.Next(50) == 0)
                {
                    AddBrickClump(x, y);
                }
            }
        }
    }

    private void AddBrickClump(int x, int y)
    {
        var startLeft = x - 1 - Random.Next(8);

        var startRight = x + 1 + Random.Next(8);

        AddClumpRow(startLeft, startRight, y);

        var left = startLeft;

        var right = startRight;

        for (var mY = y; mY > y - Random.Next(4); mY++)
        {
            left += Random.Next(3);

            right -= Random.Next(3);

            if (left > right)
            {
                break;
            }

            AddClumpRow(left, right, mY);
        }

        left = startLeft;

        right = startRight;

        for (var mY = y; mY < y + Random.Next(4); mY--)
        {
            left += Random.Next(3);

            right -= Random.Next(3);

            if (left > right)
            {
                break;
            }

            AddClumpRow(left, right, mY);
        }
    }

    private void AddClumpRow(int left, int right, int y)
    {
        SafeSetMap(left, y, 18 + Random.Next(6));

        SafeSetMap(right, y, 18 + Random.Next(6));

        for (var x = left + 1; x <= right - 1; x++)
        {
            SafeSetMap(x, y, 34);
        }
    }

    private void SafeSetMap(int x, int y, int value)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            _map[x, y] = value;
        }
    }
}