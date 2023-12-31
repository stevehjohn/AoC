namespace AoC.Solutions.Solutions._2021._19;

public class TransformParameters
{
    public Axis[] Mappings { get; }

    public Sign[] Flips { get; }

    public TransformParameters(Axis[] mappings, Sign[] flips)
    {
        Mappings = mappings;

        Flips = flips;
    }
}