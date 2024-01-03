using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._21;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        FillUnreachable();
        
        var result = ExtrapolateAnswer();

        return result.ToString();
    }
}