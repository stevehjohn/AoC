using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._17;

[UsedImplicitly]
public class Part2 : Base
{
    public override string Description => "Probe launch trick shot";

    public override string GetAnswer()
    {
        Simulate();

        return Velocities.Count.ToString();
    }
}