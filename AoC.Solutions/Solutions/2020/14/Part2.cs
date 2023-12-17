using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._14;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly Dictionary<long, long> _memory = new();

    public override string GetAnswer()
    {
        (long Or, long Floating) mask = (0, 0);

        foreach (var line in Input)
        {
            if (line.StartsWith("ma"))
            {
                mask = ParseMask(line);

                continue;
            }

            var instruction = ParseInstruction(line);

            ApplyInstruction(instruction.Location, instruction.Value, mask);
        }

        var result = _memory.Sum(kvp => kvp.Value);

        return result.ToString();
    }

    private void ApplyInstruction(long location, long value, (long Or, long Floating) mask)
    {
        location |= mask.Or;

        location &= long.MaxValue - mask.Floating;

        var locations = new List<long>();

        for (var b = 0; b < 36; b++)
        {
            var bit = 1L << b;

            if ((mask.Floating & bit) > 0)
            {
                if (locations.Count == 0)
                {
                    locations.Add(location);

                    locations.Add(location ^ bit);

                    continue;
                }

                var count = locations.Count;

                for (var i = 0; i < count; i++)
                {
                    locations.Add(locations[i] ^ bit);
                }
            }
        }

        foreach (var item in locations)
        {
            _memory[item] = value;
        }
    }

    private static (long Or, long Floating) ParseMask(string mask)
    {
        var or = 0L;

        var floating = 0L;

        mask = mask.Split('=', StringSplitOptions.TrimEntries)[1];

        var bit = 1L;

        for (var i = mask.Length - 1; i >= 0; i--)
        {
            switch (mask[i])
            {
                case 'X':
                    floating |= bit;

                    break;

                case '1':
                    or |= bit;

                    break;
            }

            bit <<= 1;
        }

        return (or, floating);
    }
}