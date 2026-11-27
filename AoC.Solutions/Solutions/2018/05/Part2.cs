using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._05;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var smallest = int.MaxValue;

        var polymer = Input[0];

        for (var c = 'a'; c <= 'z'; c++)
        {
            smallest = Math.Min(smallest, ReactPolymer(polymer, c));
        }

        return smallest.ToString();
    }
}