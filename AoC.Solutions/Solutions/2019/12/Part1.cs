using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._12;

[UsedImplicitly]
public class Part1 : Solution
{
    private readonly List<Moon> _moons = new();

    public override string GetAnswer()
    {
        ParseInput();

        for (var step = 0; step < 1000; step++)
        {
            for (var comparer = 0; comparer < _moons.Count; comparer++)
            {
                for (var comparee = comparer + 1; comparee < _moons.Count; comparee++)
                {
                    ApplyGravity(_moons[comparer], _moons[comparee]);
                }
            }

            for (var i = 0; i < _moons.Count; i++)
            {
                _moons[i].Position.X += _moons[i].Velocity.X;

                _moons[i].Position.Y += _moons[i].Velocity.Y;
                
                _moons[i].Position.Z += _moons[i].Velocity.Z;
            }
        }

        var energy = _moons.Sum(m => m.Energy);

        return energy.ToString();
    }

    private void ParseInput()
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

            _moons.Add(moon);
        }
    }

    private static int ParseComponent(string component)
    {
        return int.Parse(component.Split('=', StringSplitOptions.TrimEntries)[1]);
    }

    private static void ApplyGravity(Moon comparer, Moon comparee)
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
}