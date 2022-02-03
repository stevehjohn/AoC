﻿using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._23;

public abstract class Base : Solution
{
    public override string Description => "Nanobot teleportation";

    protected readonly List<(Point Position, int Range)> Nanobots = new();

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var split = line[5..].Split(">, r=", StringSplitOptions.TrimEntries);

            var parts = split[0].Split(',', StringSplitOptions.TrimEntries);

            Nanobots.Add((new Point(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2])), int.Parse(split[1])));
        }
    }
}