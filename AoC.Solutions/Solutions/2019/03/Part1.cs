using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._03;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var intersections = GetIntersections();

        var closest = intersections.Min(i => Math.Abs(i.X) + Math.Abs(i.Y)).ToString();

        return closest;
    }
}