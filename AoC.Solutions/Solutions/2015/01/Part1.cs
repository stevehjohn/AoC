using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._01;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var floor = 0;

        foreach (var c in Input[0])
        {
            floor += c == '(' ? 1 : -1;
        }

        return floor.ToString();
    }
}