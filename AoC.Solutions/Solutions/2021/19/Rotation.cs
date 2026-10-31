namespace AoC.Solutions.Solutions._2021._19;

public abstract partial class Base
{
    private readonly record struct Rotation(int A, int B, int C, int Sx, int Sy, int Sz)
    {
        public Vector Apply(Vector p)
        {
            Span<int> v = [p.X, p.Y, p.Z];

            return new Vector(Sx * v[A], Sy * v[B], Sz * v[C]);
        }
    }
}