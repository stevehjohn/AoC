using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._12;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var previousStates = new List<int>[3];

        var cycleCounts = new long[3];

        var xHash = 0;

        var yHash = 0;

        var zHash = 0;

        for (var i = 0; i < Moons.Count; i++)
        {
            xHash = HashCode.Combine(xHash, Moons[i].Position.X, Moons[i].Velocity.X);

            yHash = HashCode.Combine(yHash, Moons[i].Position.Y, Moons[i].Velocity.Y);

            zHash = HashCode.Combine(zHash, Moons[i].Position.Z, Moons[i].Velocity.Z);
        }

        previousStates[0] = new List<int>();

        previousStates[1] = new List<int>();

        previousStates[2] = new List<int>();

        previousStates[0].Add(xHash);

        previousStates[1].Add(yHash);
        
        previousStates[2].Add(zHash);

        var cycle = 0;

        var found = 0;

        while (true)
        {
            RunCycle();

            cycle++;

            xHash = 0;
            
            yHash = 0;
            
            zHash = 0;

            for (var i = 0; i < Moons.Count; i++)
            {
                xHash = HashCode.Combine(xHash, Moons[i].Position.X, Moons[i].Velocity.X);

                yHash = HashCode.Combine(yHash, Moons[i].Position.Y, Moons[i].Velocity.Y);

                zHash = HashCode.Combine(zHash, Moons[i].Position.Z, Moons[i].Velocity.Z);
            }

            if (cycleCounts[0] == 0 && cycle > 0)
            {
                if (previousStates[0].Any(s => s == xHash))
                {
                    cycleCounts[0] = cycle;

                    found++;
                }

                if (previousStates[1].Any(s => s == yHash))
                {
                    cycleCounts[1] = cycle;

                    found++;
                }

                if (previousStates[2].Any(s => s == zHash))
                {
                    cycleCounts[2] = cycle;

                    found++;
                }
            }

            previousStates[0].Add(xHash);

            previousStates[1].Add(yHash);

            previousStates[2].Add(zHash);

            if (found == 3)
            {
                break;
            }
        }

        var lowestCommonMultiple = LowestCommonMultiple(cycleCounts.ToList());

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