using System.Numerics;

namespace AoC.Solutions.Infrastructure;

public readonly record struct Range<T>(T Start, T End) : IComparable<Range<T>> where T : INumber<T>
{
    public int CompareTo(Range<T> other)
    {
        if (Start < other.Start)
        {
            return -1;
        }

        if (Start > other.Start)
        {
            return 1;
        }
        
        return 0;
    }
}