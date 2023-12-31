using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._19;

public class Distance
{
    public Point Delta { get; }

    public Point Beacon1 { get; }

    public Point Beacon2 { get; }

    public Distance(Point delta, Point beacon1, Point beacon2)
    {
        Delta = delta;
        Beacon1 = beacon1;
        Beacon2 = beacon2;
    }
}