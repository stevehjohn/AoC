using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._21;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var answer = Solve();

        // ReSharper disable once SpecifyACultureInStringConversionExplicitly
        return answer.ToString();
    }
}