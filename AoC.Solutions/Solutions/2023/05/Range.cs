namespace AoC.Solutions.Solutions._2023._05;

public class Range
{
    public long Start { get; set; }
    
    public long End { get; set; }

    public Range(long start, long end)
    {
        Start = start;
        
        End = end;
    }

    public bool Contains(Range other)
    {
        return other.Start >= Start && other.End <= End;
    }
}