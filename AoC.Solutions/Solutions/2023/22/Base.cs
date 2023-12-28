using AoC.Solutions.Common;
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._22;

public abstract class Base : Solution
{
    public override string Description => "Sand slabs";

    protected const int MaxHeight = 300;
    
    protected readonly int[,,] Map = new int[MaxHeight, 10, 10];

    protected readonly HashSet<(int Id, int SupportedById)> Supported = new();

    protected int Count;
    
    protected void BuildStructure()
    {
        for (var z = 2; z < MaxHeight; z++)
        {
            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 10; y++)
                {
                    var brick = Map[z, x, y];

                    if (brick == 0)
                    {
                        continue;
                    }

                    var below = Map[z - 1, x, y];
                    
                    if (below != 0 && below != brick)
                    {
                        Supported.Add((brick, below));
                    }
                }
            }
        }
    }

    protected bool SettleBricks(bool move = true)
    {
        var found = new HashSet<int>();

        var supported = new HashSet<int>();

        var dropped = true;

        while (dropped)
        {
            dropped = false;

            for (var z = 2; z < MaxHeight; z++)
            {
                for (var x = 0; x < 10; x++)
                {
                    for (var y = 0; y < 10; y++)
                    {
                        var brick = Map[z, x, y];

                        if (brick == 0)
                        {
                            continue;
                        }

                        found.Add(brick);

                        if (Map[z - 1, x, y] != 0)
                        {
                            supported.Add(brick);
                        }
                    }
                }

                for (var x = 0; x < 10; x++)
                {
                    for (var y = 0; y < 10; y++)
                    {
                        var brick = Map[z, x, y];

                        if (found.Contains(brick) && ! supported.Contains(brick))
                        {
                            Map[z - 1, x, y] = brick;

                            Map[z, x, y] = 0;

                            if (! move)
                            {
                                return true;
                            }

                            dropped = true;
                        }
                    }
                }

                found.Clear();

                supported.Clear();
            }
        }

        return false;
    }

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split('~');

            var start = Point.Parse(parts[0]);

            var end = Point.Parse(parts[1]);

            if (end.Z < start.Z)
            {
                (start, end) = (end, start);
            }

            Count++;

            Map[start.Z, start.X, start.Y] = Count;

            while (! start.Equals(end))
            {
                start = new Point(
                    start.X.Converge(end.X),
                    start.Y.Converge(end.Y),
                    start.Z.Converge(end.Z));

                Map[start.Z, start.X, start.Y] = Count;
            }
        }
    }
}