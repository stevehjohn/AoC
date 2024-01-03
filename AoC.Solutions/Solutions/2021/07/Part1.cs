using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._07;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var positions = Input[0].Split(',').Select(int.Parse).OrderBy(n => n).ToList();

        var median = positions[positions.Count / 2];

        return positions.Select(n => Math.Abs(n - median)).Sum().ToString();
    }
}