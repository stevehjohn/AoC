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
            var levelCount = GetLevels(Input[i]);
            
            if (IsSafe(levelCount))
            {
                result++;
            }
        }

        return result.ToString();
    }
}