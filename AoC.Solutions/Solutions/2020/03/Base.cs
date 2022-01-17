using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._03;

public abstract class Base : Solution
{
    public override string Description => "Tobogganing through the trees";

    protected bool[,] Map;

    protected int Width;

    protected int Height;

    protected void ParseInput()
    {
        Width = Input[0].Length;

        Height = Input.Length;

        Map = new bool[Width, Height];

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                Map[x, y] = Input[y][x] == '#';
            }
        }
    }
}