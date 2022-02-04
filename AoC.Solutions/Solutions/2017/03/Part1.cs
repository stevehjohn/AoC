using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._03;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var number = int.Parse(Input[0]);

        var ring = Ring(number);

        var ringEnd = RingEnd(ring + 1);

        var previousRingEnd = RingEnd(ring);

        var ringStart = RingStart(Ring(number)) + 1;

        var columns = (ringEnd - previousRingEnd) / 4 + 1;

        var center = ringStart + columns / 2 - 1;

        var sequence = number - ringStart;

        var edgeCenter = sequence / (ring * 2);

        edgeCenter *= ring * 2;

        edgeCenter += center;

        var offset = Math.Abs(edgeCenter - number);

        var manhattan = ring + offset;

        return manhattan.ToString();
    }

    private static int Ring(int number)
    {
        if (number < 2)
        {
            return 0;
        }

        return ((int) Math.Sqrt(number - 1) - 1) / 2 + 1;
    }

    private static int RingEnd(int ring)
    {
        return (int) Math.Pow(2 * ring - 1, 2);
    }

    private static int RingStart(int ring)
    {
        return (int) Math.Pow(2 * ring - 1, 2);
    }
}