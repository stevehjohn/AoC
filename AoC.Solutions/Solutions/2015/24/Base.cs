using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._24;

public abstract class Base : Solution
{
    public override string Description => "It hangs in the balance";

    protected long Smallest = long.MaxValue;

    private int _minDepth = int.MaxValue;

    protected void TryFindQeOfGroup(List<int> weights, int target, long qe = 1, int current = 0, int depth = 0)
    {
        foreach (var weight in weights)
        {
            if (weight + current == target)
            {
                if (qe * weight < Smallest)
                {
                    Smallest = qe * weight;

                    _minDepth = depth;
                }

                continue;
            }

            if (weight + current > target)
            {
                continue;
            }

            if (depth + 1 < _minDepth)
            {
                TryFindQeOfGroup(weights.Where(w => w != weight).ToList(), target, qe * weight, current + weight, depth + 1);
            }
        }
    }
}