using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._15;

public abstract class Base : Solution
{
    public override string Description => "Disc timing";

    protected static int Solve(List<(int Disc, int Slots, int Position)> discs)
    {
        var delay = 0;

        while (true)
        {
            var caught = false;

            foreach (var disc in discs)
            {
                if ((delay + disc.Disc + disc.Position) % disc.Slots != 0)
                {
                    caught = true;

                    break;
                }
            }

            if (! caught)
            {
                break;
            }

            delay++;
        }

        return delay;
    }

    protected List<(int Disc, int Slots, int Position)> ParseInput()
    {
        var discs = new List<(int Disc, int Slots, int Position)>();

        foreach (var line in Input)
        {
            var parts = line.Split(' ');

            discs.Add((parts[1][1] - '0', int.Parse(parts[3]), int.Parse(parts[11][..^1])));
        }

        return discs;
    }
}