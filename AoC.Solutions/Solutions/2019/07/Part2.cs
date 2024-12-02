using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._07;

[UsedImplicitly]
public class Part2 : Base
{
    private static readonly int[] Array = [5, 6, 7, 8, 9];

    public override string GetAnswer()
    {
        return GetAnswer(Array.GetPermutations());
    }
}