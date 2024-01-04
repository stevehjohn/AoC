using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._16;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        OptimiseGraph();

        Traverse(26);

        return Solve().ToString();
    }

    private int Solve()
    {
        var max = 0;

        var cache = StateCache.Where(i => i.Value.OpenedCount > 4).ToList();

        foreach (var human in cache)
        {
            foreach (var elephant in cache)
            {
                if (human.Key == elephant.Key)
                {
                    break;
                }

                if ((human.Key & elephant.Key) > 0)
                {
                    continue;
                }

                max = Math.Max(max, human.Value.Flow + elephant.Value.Flow);
            }
        }

        return max;
    }
}