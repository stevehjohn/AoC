using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._12;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = Navigate(false);

        return result.ToString();
    }
}