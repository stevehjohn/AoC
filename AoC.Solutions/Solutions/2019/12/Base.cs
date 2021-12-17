using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._12;

public abstract class Base : Solution
{
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

    private static int ParseComponent(string component)
    {
        return int.Parse(component.Split('=', StringSplitOptions.TrimEntries)[1]);

    }
}