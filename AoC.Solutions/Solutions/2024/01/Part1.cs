using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._01;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        var difference = 0;
        
        for (var i = 0; i < _left.Count; i++)
        {
            difference += Math.Abs(_left[i] - _right[i]);
        }

        return difference.ToString();
    }
}