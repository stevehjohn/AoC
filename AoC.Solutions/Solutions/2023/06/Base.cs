using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._06;

public abstract class Base : Solution
{
    public override string Description => "Wait For It";

    protected static int GetRaceWinPossibilities(long time, long record)
    {
        var wins = 0;

        var charge = 1;

        while (charge < time)
        {
            var distance = (time - charge) * charge;

            if (distance > record)
            {
                wins++;
            }

            charge++;
        }

        return wins;
    }
}