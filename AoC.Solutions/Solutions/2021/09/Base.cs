using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._09;

public abstract class Base : Solution
{
    public override string Description => "Lava level";

    protected int[,] Map;

    protected int Width;

    protected int Height;

    protected void ParseInput()
    {
        Width = Input[0].Length + 2;

        Height = Input.Length + 2;

        Map = new int[Width, Height];

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (y == 0 || y == Height - 1 || x == 0 || x == Width - 1)
                {
                    Map[x, y] = 9;

                    continue;
                }

                Map[x, y] = Input[y - 1][x - 1] - '0';
            }
        }
    }
}