using AoC.Solutions.Common;
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

    protected int RunGame(int vacateQuantity)
    {
        var flips = new List<Point>();

        do
        {
            flips.Clear();

            for (var y = 1; y <= Height; y++)
            {
                for (var x = 1; x <= Width; x++)
                {
                    if (Map[x, y] == '.' )
                    {
                        continue;
                    }

                    var occupiedNeighbors = CountOccupiedNeighbors(x, y);

                    if (Map[x, y] == 'L')
                    {
                        if (occupiedNeighbors == 0)
                        {
                            flips.Add(new Point(x, y));
                        }

                        continue;
                    }

                    if (occupiedNeighbors >= vacateQuantity)
                    {
                        flips.Add(new Point(x, y));
                    }
                }
            }

            foreach (var flip in flips)
            {
                Map[flip.X, flip.Y] = Map[flip.X, flip.Y] == '#' ? 'L' : '#';
            }
        } while (flips.Count > 0);

        var occupied = 0;

        for (var y = 1; y <= Height; y++)
        {
            for (var x = 1; x <= Width; x++)
            {
                occupied += Map[x, y] == '#' ? 1 : 0;
            }
        }

        return occupied;
    }

    protected abstract int CountOccupiedNeighbors(int x, int y);
}