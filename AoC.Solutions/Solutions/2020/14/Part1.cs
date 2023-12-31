using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._14;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        (long Or, long And) mask = (0, int.MaxValue);

        var memory = new Dictionary<int, long>();

        foreach (var line in Input)
        {
            if (line.StartsWith("ma"))
            {
                mask = ParseMask(line);

                continue;
            }

            var instruction = ParseInstruction(line);

            var value = (instruction.Value & mask.And) | mask.Or;

            memory[instruction.Location] = value;
        }

        var result = memory.Sum(kvp => kvp.Value);

        return result.ToString();
    }

    private static (long Or, long And) ParseMask(string mask)
    {
        var or = 0L;

        var and = long.MaxValue;

        mask = mask.Split('=', StringSplitOptions.TrimEntries)[1];

        var bit = 1L;

        for (var i = mask.Length - 1; i >= 0; i--)
        {
            switch (mask[i])
            {
                case '0':
                    and ^= bit;

                    break;

                case '1':
                    or |= bit;

                    break;
            }

            bit <<= 1;
        }

        return (or, and);
    }
}