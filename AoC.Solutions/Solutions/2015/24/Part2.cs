using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._24;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var weights = Input.Select(int.Parse).OrderByDescending(w => w).ToList();

        TryFindQeOfGroup(weights, weights.Sum() / 4);

        return Smallest.ToString();
    }
}