namespace AoC.Solutions.Solutions._2023._24;

public class DoublePoint
{
    public double X { get; private init; }
    
    public double Y { get; private init; }
    
    public double Z { get; private init; }

    private DoublePoint()
    {
    }

    public static DoublePoint Parse(string input)
    {
        var split = input.Split(',', StringSplitOptions.TrimEntries);

        var point = new DoublePoint
        {
            X = double.Parse(split[0]),
            Y = double.Parse(split[1]),
            Z = double.Parse(split[2])
        };

        return point;
    }
}