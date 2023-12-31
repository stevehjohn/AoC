using System.Numerics;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._22;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly BigInteger _numberOfCards = 119_315_717_514_047;

    private readonly BigInteger _repetitions = 101_741_582_076_661;

    /*
     * Won't pretend to understand this.
     * C#'ed this Python https://www.reddit.com/r/adventofcode/comments/ee0rqi/2019_day_22_solutions/fbtugcu/?utm_source=reddit&utm_medium=web2x&context=3
     */
    public override string GetAnswer()
    {
        BigInteger a = 1;

        BigInteger b = 0L;

        foreach (var line in Input)
        {
            if (line.StartsWith("deal into"))
            {
                a = -a % _numberOfCards;

                b = (_numberOfCards - 1 - b) % _numberOfCards;

                continue;
            }

            int arg;

            if (line.StartsWith("cut"))
            {
                arg = int.Parse(line.Substring(4));

                b = (b - arg) % _numberOfCards;

                continue;
            }

            arg = int.Parse(line.Substring(20));

            a = a * arg % _numberOfCards;

            b = b * arg % _numberOfCards;
        }

        var r = b * BigInteger.ModPow(1 - a, _numberOfCards - 2, _numberOfCards) % _numberOfCards;

        var card = ((2020 - r) * BigInteger.ModPow(a, _repetitions * (_numberOfCards - 2), _numberOfCards) + r) % _numberOfCards;

        return card.ToString();
    }
}