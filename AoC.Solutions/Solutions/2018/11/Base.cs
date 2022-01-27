using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._11;

public abstract class Base : Solution
{
    public override string Description => "Max power... cell";

    protected int[,] Grid;

    protected const int GridSize = 300;

    protected void CalculateCellPowers(int serial)
    {
        Grid = new int[GridSize, GridSize];

        for (var y = 0; y < GridSize; y++)
        {
            for (var x = 0; x < GridSize; x++)
            {
                Grid[x, y] = CalculatePowerLevel(x, y, serial);
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