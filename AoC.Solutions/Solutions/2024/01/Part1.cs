using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._01;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
                
        Left.Sort();
        
        Right.Sort();

        var difference = 0;
        
        for (var i = 0; i < Left.Count; i++)
        {
            difference += Math.Abs(Left[i] - Right[i]);
        }

        return difference.ToString();
    }
}