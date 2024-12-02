using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._02;

public abstract class Base : Solution
{
    public override string Description => "Red-Nosed Reports";

    protected static bool IsSafe(int[] levels, int exclude = -1)
    {
        var sign = 0;

        var safe = true;

        var l = 0;

        var r = 1;
        
        while (r < levels.Length)
        {
            if (r == exclude)
            {
                r++;
                
                continue;
            }

            if (l == exclude)
            {
                l++;
                
                continue;
            }

            if (l == r)
            {
                r++;
                
                continue;
            }

            var difference = levels[l] - levels[r];

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

            l++;

            r++;
        }

        return safe;
    }

    protected int[] GetLevels(string input)
    {
        var parts = input.Split(' ');

        var levels = new int[parts.Length];

        for (var i = 0; i < parts.Length; i++)
        {
            levels[i] = int.Parse(parts[i]);
        }

        return levels;
    }
}