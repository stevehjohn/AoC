using System.Windows.Markup;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._12;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var previousStates = new List<Moon>[Moons.Count];

        var cycleCounts = new int[Moons.Count, 3];

        for (var i = 0; i < Moons.Count; i++)
        {
            previousStates[i] = new List<Moon>
                                {
                                    new(Moons[i])
                                };
        }

        var cycle = 1;

        var found = 0;

        while (true)
        {
            RunCycle();

            cycle++;

            for (var i = 0; i < Moons.Count; i++)
            {
                if (previousStates[i].Any(m => m.Position.X == Moons[i].Position.X && m.Velocity.X == Moons[i].Velocity.X))
                {
                    if (cycleCounts[i, 0] == 0 && cycle > 0)
                    {
                        cycleCounts[i, 0] = cycle;

                        found++;
                    }
                }

                if (previousStates[i].Any(m => m.Position.Y == Moons[i].Position.Y && m.Velocity.Y == Moons[i].Velocity.Y))
                {
                    if (cycleCounts[i, 1] == 0 && cycle > 0)
                    {
                        cycleCounts[i, 1] = cycle;

                        found++;
                    }
                }

                if (previousStates[i].Any(m => m.Position.Z == Moons[i].Position.Z && m.Velocity.Z == Moons[i].Velocity.Z))
                {
                    if (cycleCounts[i, 2] == 0 && cycle > 0)
                    {
                        cycleCounts[i, 2] = cycle;

                        found++;
                    }
                }

                previousStates[i].Add(new Moon(Moons[i]));
            }

            if (found == Moons.Count * 3)
            {
                break;
            }
        }

        var values = new List<long>();

        for (var i = 0; i < Moons.Count; i++)
        {
            values.Add(cycleCounts[i, 0]);

            values.Add(cycleCounts[i, 1]);
            
            values.Add(cycleCounts[i, 2]);
        }

        var lowestCommonMultiple = LowestCommonMultiple(values);

        return "TESTING";
    }

    private long LowestCommonMultiple(List<long> input)
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

    private long GreatestCommonFactor(long left, long right)
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