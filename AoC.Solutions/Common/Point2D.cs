namespace AoC.Solutions.Common;

public struct Point2D : IEquatable<Point2D>
{
    public int X { get; private set; }
    
    public int Y { get; private set; }

    public Point2D(int x, int y)
    {
        X = x;
        
        Y = y;
    }

    public Point2D(string text)
    {
        var parts = text.Split(',');

        X = int.Parse(parts[0]);

        Y = int.Parse(parts[1]);
    }

    public int ManhattanDistance(Point2D other)
    {
        return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
    }

    public void StepTowards(Point2D other)
    {
        X += Math.Sign(other.X - X);

        Y += Math.Sign(other.Y - Y);
    }

    public void StepTowardsSingleAxis(Point2D other)
    {
        if (X == other.X && Y == other.Y)
        {
            return;
        }

        var manhattanDistance = Math.Abs(other.X - X) + Math.Abs(other.Y - Y);

        if (manhattanDistance % 2 == 0)
        {
            if (X != other.X)
            {
                X += Math.Sign(other.X - X);
            }
            else
            {
                Y += Math.Sign(other.Y - Y);
            }
        }
        else
        {
            if (Y != other.Y)
            {
                Y += Math.Sign(other.Y - Y);
            }
            else
            {
                X += Math.Sign(other.X - X);
            }
        }
    }

    public static readonly Point2D North = new(0, -1);

    public static readonly Point2D East = new(1, 0);

    public static readonly Point2D South = new(0, 1);

    public static readonly Point2D West = new(-1, 0);

    public static readonly Point2D Null = new(int.MinValue, int.MaxValue);

    public static Point2D operator +(Point2D left, Point2D right)
    {
        return new Point2D(left.X + right.X, left.Y + right.Y);
    }

    public static Point2D operator -(Point2D left)
    {
        return new Point2D(-left.X, -left.Y);
    }
    
    public static bool operator ==(Point2D left, Point2D right)
    {
        return left.X == right.X && left.Y == right.Y;
    }

    public static bool operator !=(Point2D left, Point2D right)
    {
        return !(left == right);
    }
    
    public bool Equals(Point2D other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object obj)
    {
        return obj is Point2D other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        return $"{X}, {Y}";
    }
}