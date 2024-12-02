using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._02;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var result = 0;
    
        for (var i = 0; i < Input.Length; i++)
        {
            var parts = Input[i].Split(' ');

            var sign = 0;

            var notSafe = 0;

            var l = 0;

            var r = 1;

            while (r < parts.Length)
            {
                var difference = int.Parse(parts[l]) - int.Parse(parts[r]);
                
                if (Math.Abs(difference) is < 1 or > 3)
                {
                    notSafe++;

                    r++;
                
                    continue;
                }

                if (sign == 0)
                {
                    sign = difference < 0 ? -1 : 1;
                }
                else
                {
                    if ((difference < 0 ? -1 : 1) != sign)
                    {
                        notSafe++;

                        r++;
                
                        continue;
                    }
                }

                l++;
                
                r++;
            }

            if (notSafe < 2)
            {
                result++;
            }
        }

        return result.ToString();
    }
}