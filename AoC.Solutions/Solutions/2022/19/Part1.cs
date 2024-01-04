using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._19;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var results = Simulate(24);

        return results.Sum(r => r.Best * r.Id).ToString();
    }
}