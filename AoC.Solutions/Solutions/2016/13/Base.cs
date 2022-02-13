using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._13;

public abstract class Base : Solution
{
    public override string Description => "Cubicle maze";

    protected bool[,] Maze;

    protected int Width;

    protected int Height;

    protected void BuildMaze()
    {
        Maze = new bool[Width, Height];

        var designerNumber = int.Parse(Input[0]);

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var number = x * x + 3 * x + 2 * x * y + y + y * y;

                number += designerNumber;

                var bits = number.CountBits();

                if (bits % 2 == 1)
                {
                    Maze[x, y] = true;
                }
            }
        }
    }
}