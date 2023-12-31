using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._08;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var input = ParseInput();

        var result = SumMetadata(input, true);

        return result.Sum.ToString();
    }
}