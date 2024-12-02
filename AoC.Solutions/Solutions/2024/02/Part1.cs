using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._02;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = 0;
    
        for (var i = 0; i < Input.Length; i++)
        {
            var levels = Input[i].Split(' ');
            
            if (IsSafe(levels))
            {
                result++;
            }
        }

        return result.ToString();
    }
}