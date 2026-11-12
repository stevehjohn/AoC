using AoC.Solutions.Common;
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._23;

public abstract class Base : Solution
{
    public override string Description => "Nanobot teleportation";

    protected readonly List<(Point Position, int Range)> Nanobots = [];

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var split = line[5..].Split(">, r=", StringSplitOptions.TrimEntries);

            var parts = split[0].Split(',', StringSplitOptions.TrimEntries);

            Nanobots.Add((new Point(int.FastParse(parts[0]), int.FastParse(parts[1]), int.FastParse(parts[2])), int.FastParse(split[1])));
        }
    }

    protected static int GetManhattanDistance(Point a, Point b)
    {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z);
    }
}