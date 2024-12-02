using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._02;

public abstract class Base : Solution
{
    public override string Description => "Red-Nosed Reports";

    protected int CountSafeLevels()
    {
        var result = 0;
        
        for (var i = 0; i < Input.Length; i++)
        {
            var parts = Input[i].Split(' ');

            var sign = 0;

            var safe = true;
            
            for (var j = 1; j < parts.Length; j++)
            {
                var difference = int.Parse(parts[j - 1]) - int.Parse(parts[j]);

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

            if (safe)
            {
                result++;
            }
        }

        return result;
    }
}