namespace AoC.Solutions.Solutions._2020._17;

public class Point4D
{
    public int X { get; set; }

    public int Y { get; set; }

    public int Z { get; set; }

    public int W { get; set; }

    public Point4D(int x, int y, int z = 0, int w = 0)
    {
        X = x;

        Y = y;

        Z = z;

        W = w;
    }

    public override string ToString()
    {
        return $"{X,5},{Y,5},{Z,5},{W,5}";
    }

    protected bool Equals(Point4D other)
    {
        return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
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
        
        return Equals((Point4D)obj);
    }

    public override int GetHashCode()
    {
        // ReSharper disable NonReadonlyMemberInGetHashCode - sue me, I need Point to be mutable sometimes...
        return HashCode.Combine(X, Y, Z, W);
        // ReSharper restore NonReadonlyMemberInGetHashCode
    }
}