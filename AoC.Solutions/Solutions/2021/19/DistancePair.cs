namespace AoC.Solutions.Solutions._2021._19;

public class DistancePair
{
    public Distance Origin { get; }

    public Distance Target { get; }

    public DistancePair(Distance origin, Distance target)
    {
        Origin = origin;
        Target = target;
    }
}