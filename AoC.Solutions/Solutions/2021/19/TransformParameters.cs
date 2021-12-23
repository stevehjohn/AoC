namespace AoC.Solutions.Solutions._2021._19;

public class TransformParameters
{
    public Axis[] Mappings { get; } = new Axis[3];

    public int[] Deltas { get; } = new int[3];

    public Sign[] Signs { get; } = new Sign[3];

    public Sign[] FlipResult { get; } = new Sign[3];

    public TransformParameters()
    {
        Mappings[0] = Axis.Unknown;

        Mappings[1] = Axis.Unknown;
        
        Mappings[2] = Axis.Unknown;
    }
}