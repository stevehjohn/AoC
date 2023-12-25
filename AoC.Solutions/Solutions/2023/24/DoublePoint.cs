namespace AoC.Solutions.Solutions._2023._24;

public class DoublePoint
{
    public double X { get; set; }
    
    public double Y { get; set; }
    
    public double Z { get; set; }

    private DoublePoint()
    {
    }

    protected DoublePoint(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
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

    public static DoublePoint operator -(DoublePoint left, DoublePoint right)
    {
        return new DoublePoint(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
    }

    public override string ToString()
    {
        return $"{X}, {Y}, {Z}";
    }
}