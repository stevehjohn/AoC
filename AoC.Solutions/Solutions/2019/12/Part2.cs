using AoC.Solutions.Libraries;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._12;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var xCycle = -1;

        var yCycle = -1;

        var zCycle = -1;

        var cycle = 0;

        while (true)
        {
            RunCycle();

            cycle++;

            if (xCycle == -1 && Moons.All(m => m.Velocity.X == 0))
            {
                xCycle = cycle;
            }

            if (yCycle == -1 && Moons.All(m => m.Velocity.Y == 0))
            {
                yCycle = cycle;
            }

            if (zCycle == -1 && Moons.All(m => m.Velocity.Z == 0))
            {
                zCycle = cycle;
            }

            if (xCycle > -1 && yCycle > -1 && zCycle > -1)
            {
                break;
            }
        }

        var lowestCommonMultiple = Maths.LowestCommonMultiple(new List<long> { xCycle, yCycle, zCycle }) * 2;

        return lowestCommonMultiple.ToString();
    }
}