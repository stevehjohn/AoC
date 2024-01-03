using AoC.Solutions.Common;
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

        var ySums = new int[GridSize];

        for (var y = squareSize - 1; y < GridSize - squareSize; y++)
        {
            var sum = GetSumAtPoint(squareSize - 1, y, squareSize, ySums);

            if (sum > max)
            {
                max = sum;

                position = new Point(squareSize - 1, y);
            }

            for (var x = squareSize; x < GridSize - squareSize; x++)
            {
                var ySum = 0;

                for (var oY = 0; oY < squareSize; oY++)
                {
                    ySum += _grid[x + squareSize - 1, y + oY];
                }

                sum += ySum;

                sum -= ySums[x - 1];

                ySums[x + squareSize - 1] = ySum;

                if (sum > max)
                {
                    max = sum;

                    position = new Point(x, y);
                }
            }
        }

        return (position, max);
    }

    private int GetSumAtPoint(int x, int y, int squareSize, int[] ySums)
    {
        var sum = 0;

        for (var oX = 0; oX < squareSize; oX++)
        {
            var ySum = 0;

            for (var oY = 0; oY < squareSize; oY++)
            {
                ySum += _grid[x + oX, y + oY];
            }

            sum += ySum;

            ySums[x + oX] = ySum;
        }

        return sum;
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