using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._02;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var sum = IterateInput();

        return sum.ToString();
    }

    protected override long SumInvalidIPatterns(long id)
    {
        var text = id.ToString();

        for (var patternLength = text.Length / 2; patternLength > 0; patternLength--)
        {
            if (text.Length % patternLength != 0)
            {
                continue;
            }

            var isRepeating = true;

            for (var i = patternLength; i < text.Length; i++)
            {
                if (text[i] != text[i % patternLength])
                {
                    isRepeating = false;
                    
                    break;
                }
            }

            if (isRepeating)
            {
                return id;
            }
        }

        return 0;
    }
}