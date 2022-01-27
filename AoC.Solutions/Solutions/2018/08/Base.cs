using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._08;

public abstract class Base : Solution
{
    public override string Description => "Navigation licensing";

    protected static (int Sum, List<int> Remaining) SumMetadata(List<int> input, bool metadataIsPointer = false)
    {
        var childCount = input[0];

        var metadataCount = input[1];

        var remaining = input.Skip(2).ToList();

        var total = 0;

        var childSums = new List<int>();

        for (var i = 0; i < childCount; i++)
        {
            var result = SumMetadata(remaining, metadataIsPointer);

            childSums.Add(result.Sum);

            if (! metadataIsPointer)
            {
                total += result.Sum;
            }

            remaining = result.Remaining;
        }

        if (metadataIsPointer)
        {
            total += childCount == 0
                         ? remaining.Take(metadataCount).Sum()
                         : remaining.Take(metadataCount).Select(p => childSums.ElementAtOrDefault(p - 1)).Sum();
        }
        else
        {
            total += remaining.Take(metadataCount).Sum();
        }

        return (total, remaining.Skip(metadataCount).ToList());
    }

    protected List<int> ParseInput()
    {
        return Input[0].Split(' ').Select(int.Parse).ToList();
    }
}