using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._06;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var times = Input[0][9..].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        
        var records = Input[1][9..].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

        var total = 1;
        
        for (var i = 0; i < times.Length; i++)
        {
            total *= GetRaceWinPossibilities(times[i], records[i]);
        }

        return total.ToString();
    }

    private static int GetRaceWinPossibilities(long time, long record)
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