using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._13;

public abstract class Base : Solution
{
    public override string Description => "Distress signal";

    protected int Solve()
    {
        var packetPair = 0;

        var result = 0;

        for (var i = 0; i < Input.Length; i += 3)
        {
            packetPair++;

            if (Compare(Input[i].Replace("10", ":"), Input[i + 1].Replace("10", ":")))
            {
                result += packetPair;
            }
        }

        return result;
    }

    private bool Compare(string left, string right)
    {
        if (! string.IsNullOrWhiteSpace(left) && ! string.IsNullOrWhiteSpace(right))
        {
            if (left[0] != right[0])
            {
                return (L: left[0], R: right[0]) switch
                {
                    { L: ']' } => true,
                    { R: ']' } => false,
                    { L: '[' } => Compare(left, $"[{right[0]}]{right[1..]}"),
                    { R: '[' } => Compare($"[{left[0]}]{left[1..]}", right),
                    _ => left[0] < right[0]
                };
            }

            return Compare(left[1..], right[1..]);
        }

        return ! string.IsNullOrWhiteSpace(left);
    }
}