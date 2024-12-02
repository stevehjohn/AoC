using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._02;

public abstract class Base : Solution
{
    public override string Description => "Red-Nosed Reports";

    protected static bool IsSafe(string[] levels)
    {
        var sign = 0;

        var safe = true;
        
        for (var j = 1; j < levels.Length; j++)
        {
            var difference = int.Parse(levels[j - 1]) - int.Parse(levels[j]);

            if (Math.Abs(difference) is < 1 or > 3)
            {
                safe = false;
                
                break;
            }

            if (sign == 0)
            {
                sign = difference < 0 ? -1 : 1;
            }
            else
            {
                if ((difference < 0 ? -1 : 1) != sign)
                {
                    safe = false;
                
                    break;
                }
            }
        }

        return safe;
    }
}