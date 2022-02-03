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
}