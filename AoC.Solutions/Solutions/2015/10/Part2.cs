using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._10;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var input = Input[0];

        for (var i = 0; i < 50; i++)
        {
            input = ProcessRound(input);
        }

        return input.Length.ToString();
    }
}