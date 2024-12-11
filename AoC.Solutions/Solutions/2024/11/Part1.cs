using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._11;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = 0L;

        for (var i = 0; i < Stones.Length; i++)
        {
            result += Blink(Stones[i], 25);
        }

        return result.ToString();
    }
}