using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._11;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        PlayRounds();

        return GetMonkeyBusiness().ToString();
    }
}