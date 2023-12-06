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

    private static long GetRaceWinPossibilities(long time, long record)
    {
        var first = 0L;

        var charge = 1L;

        while (charge < time)
        {
            var distance = (time - charge) * charge;

            if (distance > record)
            {
                first = charge;
                
                break;
            }

            charge++;
        }

        var last = 0L;

        charge = time;

        while (charge > 0)
        {
            var distance = (time - charge) * charge;

            if (distance > record)
            {
                last = charge;
                
                break;
            }

            charge--;
        }
        
        return last - first + 1;
    }
}