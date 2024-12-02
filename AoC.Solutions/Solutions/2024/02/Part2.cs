using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._02;

[UsedImplicitly]
public class Part2 : Base
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
                
                continue;
            }
            
            for (var j = 0; j < levelCount; j++)
            {
                if (IsSafe(levelCount, j))
                {
                    result++;
                    
                    break;
                }
            }
        }

        return result.ToString();
    }
}