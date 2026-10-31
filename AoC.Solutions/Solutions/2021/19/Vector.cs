namespace AoC.Solutions.Solutions._2021._19;

public abstract partial class Base
{
    private readonly record struct Vector(int X, int Y, int Z)
    {
        public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public static Vector operator -(Vector a, Vector b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        public int Manhattan(Vector other) => Math.Abs(X - other.X) + Math.Abs(Y - other.Y) + Math.Abs(Z - other.Z);

        public int SquaredDistance(Vector other)
        {
            var d = this - other;
            return d.X * d.X + d.Y * d.Y + d.Z * d.Z;
        }
    }
}