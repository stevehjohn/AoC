using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._01;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        var similarity = 0;
        
        for (var i = 0; i < _left.Count; i++)
        {
            var left = _left[i];

            if (_rightCounts.TryGetValue(left, out var count))
            {
                similarity += left * count;
            }
        }

        return similarity.ToString();
    }
}