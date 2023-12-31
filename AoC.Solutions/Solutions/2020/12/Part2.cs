using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._12;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var result = Navigate(true);

        return result.ToString();
    }
}