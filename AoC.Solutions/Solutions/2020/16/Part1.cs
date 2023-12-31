using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._16;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = ValidateOtherTickets();

        return result.ToString();
    }
}