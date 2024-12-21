using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._21;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var result = 0L;
        
        foreach (var line in Input)
        {
            result += Solve(line, 8) * int.Parse(line[..3]);
        }
        
        return result.ToString();
    }
}