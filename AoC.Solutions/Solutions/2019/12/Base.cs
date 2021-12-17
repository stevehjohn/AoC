﻿using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._12;

public abstract class Base : Solution
{
    public override string Description => "Orbital simulation";

    protected readonly List<Moon> Moons = new();

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var components = line[1..^1].Split(',', StringSplitOptions.TrimEntries);

            var moon = new Moon
                       {
                           Position =
                           {
                               X = ParseComponent(components[0]),
                               Y = ParseComponent(components[1]),
                               Z = ParseComponent(components[2])
                           }
                       };

            Moons.Add(moon);
        }
    }

    protected static void ApplyGravity(Moon comparer, Moon comparee)
    {
        comparer.Velocity.X += GetGravity(comparer.Position.X, comparee.Position.X);

        comparer.Velocity.Y += GetGravity(comparer.Position.Y, comparee.Position.Y);

        comparer.Velocity.Z += GetGravity(comparer.Position.Z, comparee.Position.Z);

        comparee.Velocity.X += GetGravity(comparee.Position.X, comparer.Position.X);

        comparee.Velocity.Y += GetGravity(comparee.Position.Y, comparer.Position.Y);

        comparee.Velocity.Z += GetGravity(comparee.Position.Z, comparer.Position.Z);
    }

    private static int GetGravity(int left, int right)
    {
        if (left < right)
        {
            return 1;
        }

        if (left > right)
        {
            return -1;
        }

        return 0;
    }

    private static int ParseComponent(string component)
    {
        return int.Parse(component.Split('=', StringSplitOptions.TrimEntries)[1]);

    }
}