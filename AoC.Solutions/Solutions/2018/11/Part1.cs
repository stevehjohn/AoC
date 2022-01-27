using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._11;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        CalculateCellPowers(ParseInput());

        var result = GetMaxPower(3);

        return $"{result.Position.X},{result.Position.Y}";
    }
}