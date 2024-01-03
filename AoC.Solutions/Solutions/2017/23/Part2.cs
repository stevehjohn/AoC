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

        while (true)
        {
            var isPrime = true;

            for (var i = 2; i < registerB / 2; i++)
            {
                if (registerB % i == 0)
                {
                    isPrime = false;

                    break;
                }
            }

            if (! isPrime)
            {
                registerH++;
            }

            if (registerB == registerC)
            {
                break;
            }

            registerB += 17;
        }

        return registerH.ToString();
    }
}