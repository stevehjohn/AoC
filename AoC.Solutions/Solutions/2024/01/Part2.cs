using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._01;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        var similarity = 0;
        
        for (var i = 0; i < Left.Count; i++)
        {
            var left = Left[i];

            if (RightCounts.TryGetValue(left, out var count))
            {
                similarity += left * count;
            }
        }

        return similarity.ToString();
    }
}