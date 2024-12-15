using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._15;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        RunRobot();

        var result = SumCoordinates();
        
        return result.ToString();
    }
}