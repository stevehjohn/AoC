using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._15;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var dead = GetDeadZones(2_000_000);

        return dead.ToString();
    }
}