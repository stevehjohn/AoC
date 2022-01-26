using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._08;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var input = ParseInput();

        var result = SumMetadata(input);

        return result.Sum.ToString();
    }

    private static (int Sum, List<int> Remaining) SumMetadata(List<int> input)
    {
        var childCount = input[0];

        var metadataCount = input[1];

        var remaining = input.Skip(2).ToList();

        var total = 0;

        for (var i = 0; i < childCount; i++)
        {
            var result = SumMetadata(remaining);

            total += result.Sum;

            remaining = result.Remaining;
        }

        total += remaining.Take(metadataCount).Sum();

        return (total, remaining.Skip(metadataCount).ToList());
    }

    private List<int> ParseInput()
    {
        return Input[0].Split(' ').Select(int.Parse).ToList();
    }
}