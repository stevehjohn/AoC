namespace AoC.Solutions.Solutions._2023._24;

public class DoublePoint
{
    public double X { get; private init; }
    
    public double Y { get; private init; }
    
    public double Z { get; private init; }

    private DoublePoint()
    {
    }

    public DoublePoint(double x, double y, double z)
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

    private bool Equals(DoublePoint other)
    {
        return Math.Abs(X - other.X) < 000_000_001f && Math.Abs(Y - other.Y) < 000_000_001f && Math.Abs(Z - other.Z) < 000_000_001f;
    }

    public static DoublePoint operator +(DoublePoint left, DoublePoint right)
    {
        return new DoublePoint(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    }
    
    public override string ToString()
    {
        return $"{X}, {Y}, {Z}";
    }
}