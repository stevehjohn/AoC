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

        var lowestCommonMultiple = LowestCommonMultiple(new List<long> { xCycle, yCycle, zCycle }) * 2;

        return lowestCommonMultiple.ToString();
    }

    private static long LowestCommonMultiple(List<long> input)
    {
        if (input.Count == 2)
        {
            var left = input[0];

            var right = input[1];

            return left * right / GreatestCommonFactor(left, right);
        }

        var lowestCommonMultiple = LowestCommonMultiple(input.Take(2).ToList());

        var remaining = input.Skip(2).ToList();

        remaining.Add(lowestCommonMultiple);

        return LowestCommonMultiple(remaining);
    }

    private static long GreatestCommonFactor(long left, long right)
    {
        while (left != right)
        {
            if (left > right)
            {
                left -= right;
            }
            else
            {
                right -= left;
            }
        }

        return left;
    }
}