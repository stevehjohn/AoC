using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._06;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var time = long.Parse(Input[0][9..].Replace(" ", string.Empty));
        
        var record = long.Parse(Input[1][9..].Replace(" ", string.Empty));

        var wins = GetRaceWinPossibilities(time, record);
        
        return wins.ToString();
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