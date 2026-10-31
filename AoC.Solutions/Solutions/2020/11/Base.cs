using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._11;

public abstract class Base : Solution
{
    public override string Description => "Conway's game of seating";

    protected int Width;

    protected int Height;

    protected char[,] Map;

    protected void ParseInput()
    {
        Width = Input[0].Length;

        Height = Input.Length;

        Map = new char[Width + 2, Height + 2];

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                Map[x + 1, y + 1] = Input[y][x];
            }
        }
    }
}