using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._11;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var floors = ParseInput();

        var result = Solve(floors);

        return result.ToString();
    }
}