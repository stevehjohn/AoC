namespace AoC.Solutions.Solutions._2023._05;

public class Range
{
    public long Start { get; }
    
    public long End { get; }

    public Range(long start, long end)
    {
        Start = start;
        
        End = end;
    }

    public bool Contains(Range other)
    {
        return other.Start >= Start && other.End <= End;
    }

    public Range Intersects(Range other)
    {
        if (other.Contains(this))
        {
            return new Range(Start, End);
        }

        if (Contains(other))
        {
            return new Range(other.Start, other.End);
        }

        if (other.Start <= Start && other.End >= Start && other.End <= End)
        {
            return new Range(Start, other.End);
        }

        if (other.End >= End && other.Start >= Start && other.Start <= End)
        {
            return new Range(other.Start, End);
        }

        return null;
    }

    public override string ToString()
    {
        return $"{Start} - {End}";
    }
}