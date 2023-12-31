using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._01;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var fuel = 0;

        foreach (var line in Input)
        {
            fuel += GetFuel(int.Parse(line));
        }

        return fuel.ToString();
    }

    private static int GetFuel(int mass, int total = 0)
    {
        var fuel = mass / 3 - 2;

        if (fuel < 1)
        {
            return total;
        }

        total += fuel;

        return GetFuel(fuel, total);
    }
}