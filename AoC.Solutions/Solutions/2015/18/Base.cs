using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._18;

public abstract class Base : Solution
{
    public override string Description => "Bournville GIF";

    private const int GridSize = 100;

    protected HashSet<Point> Lights = new();

    protected void RunStep(bool isPart2 = false)
    {
        var newLights = new HashSet<Point>();

        for (var y = 0; y < GridSize; y++)
        {
            for (var x = 0; x < GridSize; x++)
            {
                var position = new Point(x, y);

                if (Lights.Contains(position))
                {
                    if (CountNeighbors(position) is 2 or 3)
                    {
                        newLights.Add(position);
                    }
                }
                else
                {
                    if (CountNeighbors(position) == 3)
                    {
                        newLights.Add(position);
                    }
                }
            }
        }

        Lights = newLights;

        if (isPart2)
        {
            Lights.Add(new Point(0, 0));

            Lights.Add(new Point(0, 99));

            Lights.Add(new Point(99, 0));

            Lights.Add(new Point(99, 99));
        }
    }

    private int CountNeighbors(Point position)
    {
        var count = 0;

        count += Lights.Contains(new Point(position.X - 1, position.Y - 1)) ? 1 : 0;

        count += Lights.Contains(new Point(position.X, position.Y - 1)) ? 1 : 0;

        count += Lights.Contains(new Point(position.X + 1, position.Y - 1)) ? 1 : 0;

        count += Lights.Contains(new Point(position.X - 1, position.Y)) ? 1 : 0;

        count += Lights.Contains(new Point(position.X + 1, position.Y)) ? 1 : 0;

        count += Lights.Contains(new Point(position.X - 1, position.Y + 1)) ? 1 : 0;

        count += Lights.Contains(new Point(position.X, position.Y + 1)) ? 1 : 0;

        count += Lights.Contains(new Point(position.X + 1, position.Y + 1)) ? 1 : 0;

        return count;
    }

    protected void ParseInput()
    {
        for (var y = 0; y < GridSize; y++)
        {
            for (var x = 0; x < GridSize; x++)
            {
                if (Input[y][x] == '#')
                {
                    Lights.Add(new Point(x, y));
                }
            }
        }
    }
}