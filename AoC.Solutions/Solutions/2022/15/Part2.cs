using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._15;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var position = GetDeadZone();
        
        return (position.X * 4_000_000L + position.Y).ToString();
    }
}