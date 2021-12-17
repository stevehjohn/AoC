using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._12;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        for (var step = 0; step < 1000; step++)
        {
            RunCycle();
        }

        var energy = Moons.Sum(m => m.Energy);

        return energy.ToString();
    }
}