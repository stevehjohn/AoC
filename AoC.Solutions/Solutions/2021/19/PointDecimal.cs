using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._19;

public class PointDecimal
{
    public decimal X { get; set; }

    public decimal Y { get; set; }

    public decimal Z { get; set; }

    public PointDecimal(Point point)
    {
        X = point.X;

        Y = point.Y;

        Z = point.Z;
    }

    public PointDecimal(decimal x, decimal y, decimal z = 0)
    {
        X = x;

        Y = y;

        Z = z;
    }

    public PointDecimal()
    {
    }

    public override string ToString()
    {
        return $"{X,5},{Y,5},{Z,5}";
    }

    protected bool Equals(PointDecimal other)
    {
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((PointDecimal) obj);
    }

    public override int GetHashCode()
    {
        // ReSharper disable NonReadonlyMemberInGetHashCode - sue me, I need Point to be mutable sometimes...
        return HashCode.Combine(X, Y, Z);
        // ReSharper restore NonReadonlyMemberInGetHashCode
    }
}