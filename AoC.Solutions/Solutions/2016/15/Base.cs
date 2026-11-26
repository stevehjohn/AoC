using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._15;

public abstract class Base : Solution
{
    public override string Description => "Disc timing";

    protected static int Solve(List<(int Disc, int Slots, int Position)> discs)
    {
        delays: for (var delay = 0;; delay++)
        {
            foreach (var disc in discs)
            {
                if ((delay + disc.Disc + disc.Position) % disc.Slots != 0)
                {
                    continue delays;
                }
            }

            return delay;
        }
    }

    protected List<(int Disc, int Slots, int Position)> ParseInput()
    {
        var discs = new List<(int Disc, int Slots, int Position)>();

        foreach (var line in Input)
        {
            var parts = line.Split(' ');

            discs.Add((parts[1][1] - '0', int.FastParse(parts[3]), int.FastParse(parts[11][..^1])));
        }

        return discs;
    }
}