using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._07;

[UsedImplicitly]
public class Part1 : Base
{
    private static readonly int[] Array = [0, 1, 2, 3, 4];

    public override string GetAnswer()
    {
        return GetAnswer(Array.GetPermutations());
    }
}