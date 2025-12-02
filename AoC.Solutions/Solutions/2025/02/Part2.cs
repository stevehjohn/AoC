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

        for (var patternLength = 1; patternLength <= text.Length / 2; patternLength++)
        {
            if (text.Length % patternLength != 0)
            {
                continue;
            }

            var pattern = text[..patternLength];
            
            var isRepeating = true;

            for (var i = patternLength; i < text.Length; i += patternLength)
            {
                var segment = text[i..(i + patternLength)];
            
                if (segment != pattern)
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