using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._19;

public class Pair
{
    public Point Beacon1 { get; }

    public Point Beacon2 { get; }

    public Pair(Point beacon1, Point beacon2)
    {
        Beacon1 = beacon1;

        Beacon2 = beacon2;
    }
}