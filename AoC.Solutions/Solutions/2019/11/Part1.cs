using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._11;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        RunRobot(0);

        return Panels.Count.ToString();
    }
}