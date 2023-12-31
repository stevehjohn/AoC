using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._13;

public abstract class Base : Solution
{
    public override string Description => "Distress signal";

    protected int Compare(string left, string right)
    {
        if (! string.IsNullOrWhiteSpace(left) && ! string.IsNullOrWhiteSpace(right))
        {
            if (left[0] != right[0])
            {
                return (L: left[0], R: right[0]) switch
                {
                    { L: ']' } => -1,
                    { R: ']' } => 1,
                    { L: '[' } => Compare(left, $"[{right[0]}]{right[1..]}"),
                    { R: '[' } => Compare($"[{left[0]}]{left[1..]}", right),
                    _ => left[0] < right[0] ? -1 : 1
                };
            }

            return Compare(left[1..], right[1..]);
        }

        return string.IsNullOrWhiteSpace(left) ? 1 : -1;
    }
}