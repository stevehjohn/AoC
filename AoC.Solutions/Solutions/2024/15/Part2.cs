using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._15;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        IsPart2 = true;
        
        ParseInput();

        RunRobot();

        var result = SumCoordinates();
        
        return result.ToString();
    }
}