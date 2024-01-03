using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._07;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        return GetAnswer(new[] { 0, 1, 2, 3, 4 }.GetPermutations());
    }
}