﻿using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._11;

public abstract class Base : Solution
{
    public override string Description => "Max power... cell";

    private int[,] _grid;

    protected const int GridSize = 300;
    
    protected (Point Position, int Max) GetMaxPower(int squareSize)
    {
        var max = 0;

        Point position = null;

        for (var y = 0; y < GridSize - squareSize + 1; y++)
        {
            for (var x = 0; x < GridSize - squareSize + 1; x++)
            {
                var sum = 0;

                for (var oX = 0; oX < squareSize; oX++)
                {
                    for (var oY = 0; oY < squareSize; oY++)
                    {
                        sum += _grid[x + oX, y + oY];
                    }
                }

                if (sum > max)
                {
                    max = sum;

                    position = new Point(x, y);
                }
            }
        }

        return (position, max);
    }

    protected void CalculateCellPowers(int serial)
    {
        _grid = new int[GridSize, GridSize];

        for (var y = 0; y < GridSize; y++)
        {
            for (var x = 0; x < GridSize; x++)
            {
                _grid[x, y] = CalculatePowerLevel(x, y, serial);
            }
        }
    }

    protected int ParseInput()
    {
        return int.Parse(Input[0]);
    }

    private static int CalculatePowerLevel(int x, int y, int serial)
    {
        var result = x + 10;

        result *= y;

        result += serial;

        result *= x + 10;

        result = result / 100 % 10;

        result -= 5;

        return result;
    }
}