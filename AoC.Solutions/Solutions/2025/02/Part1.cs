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

        var halfway = text.Length / 2;

        for (var i = 0; i < halfway; i++) 
        {
            if (text[i] != text[halfway + i])
            {
                return 0;
            }
        }
        
        return id;
    }
}