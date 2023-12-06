using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._05;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly List<List<(Range Range, long Adjustment)>> _mappings = new();

    private readonly List<Range> _seeds = new();
    
    public override string GetAnswer()
    {
        ParseInput();

        return "";
    }

    private void ParseInput()
    {
        var seeds = Input[0][6..].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

        for (var i = 0; i < seeds.Length; i++)
        {
            _seeds.Add(new Range(seeds[i], seeds[i + 1]));
        }

        var mapping = new List<(Range Range, long Adjustment)>();

        foreach (var line in Input[3..])
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (!char.IsNumber(line[0]))
            {
                _mappings.Add(mapping);

                mapping = new List<(Range Range, long Adjustment)>();

                continue;
            }

            var parts = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

            mapping.Add((new Range(parts[1], parts[1] + parts[2] - 1), parts[0] - parts[1]));
        }

        _mappings.Add(mapping);
    }
}