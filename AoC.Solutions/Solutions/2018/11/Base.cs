using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._11;

public abstract class Base : Solution
{
    public override string Description => "Max power... cell";

    private int[,] _grid;

    private const int GridSize = 300;
    
    protected Point GetMaxPower(int squareSize)
    {
        var max = 0;

        Point position = null;

        for (var y = 1; y < GridSize - 1; y++)
        {
            for (var x = 1; x < GridSize - 1; x++)
            {
                var sum = 0;

                var halfSquare = squareSize / 2;

                for (var oX = -halfSquare; oX <= halfSquare; oX++)
                {
                    for (var oY = -halfSquare; oY <= halfSquare; oY++)
                    {
                        sum += _grid[x + oX, y + oY];
                    }
                }

                if (sum > max)
                {
                    max = sum;

                    position = new Point(x - halfSquare, y - halfSquare);
                }
            }
        }

        if (position == null)
        {
            throw new PuzzleException("Solution not found.");
        }

        return position;
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