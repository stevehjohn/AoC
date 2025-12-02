using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._02;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var sum = IterateInput();
        
        return sum.ToString();
    }

    protected override long SumInvalidIPatterns(long id)
    {
        var text = id.ToString();

        if (text.Length % 2 == 1)
        {
            return 0;
        }

        var left = text[..(text.Length / 2)];

        var right = text[(text.Length / 2)..];

        if (left == right)
        {
            return id;
        }

        return 0;
    }
}