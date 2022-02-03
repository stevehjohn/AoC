using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._23;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = CountInRangeOfStrongest();

        return result.ToString();
    }

    private int CountInRangeOfStrongest()
    {
        var strongest = Nanobots.MaxBy(b => b.Range);

        var count = 0;

        foreach (var bot in Nanobots)
        {
            if (GetManhattanDistance(strongest.Position, bot.Position) <= strongest.Range)
            {
                count++;
            }
        }

        return count;
    }

    private static int GetManhattanDistance(Point a, Point b)
    {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z);
    }
}