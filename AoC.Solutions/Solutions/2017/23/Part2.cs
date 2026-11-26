using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._23;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var registerB = 81 * 100 + 100_000L;

        var registerC = registerB + 17_000L;

        var registerH = 0L;

        numbers: for (; registerB <= registerC; registerB += 17)
        {
            var limit = (int) Math.Sqrt(registerB);

            for (var i = 2; i <= limit; i++)
            {
                if (registerB % i == 0)
                {
                    registerH++;

                    continue numbers;
                }
            }
        }

        return registerH.ToString();
    }
}