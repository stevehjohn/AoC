using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._21;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = CountAtStep(64);
        
        return result.ToString();
    }
}