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
            similarity += _left[i] * _right.Count(n => n == _left[i]);
        }

        return similarity.ToString();
    }
}