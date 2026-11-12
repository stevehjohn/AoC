using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._21;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = 0L;
        
        foreach (var line in Input)
        {
            result += Solve(line, 2) * int.FastParse(line[..3]);
        }
        
        return result.ToString();
    }
}