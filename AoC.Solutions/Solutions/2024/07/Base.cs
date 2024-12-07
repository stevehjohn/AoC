using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._07;

public abstract class Base : Solution
{
    public override string Description => "Bridge repair";

    protected string GetAnswer(bool isPart2)
    {
        var result = 0L;

        Parallel.ForEach(Input, line =>
        {
            var parts = line.Split(':', StringSplitOptions.TrimEntries);

            var expected = long.Parse(parts[0]);

            var components = parts[1].Split(' ').Select(long.Parse).ToArray();

            var total = isPart2
                ? ProcessThreeOperators(expected, components, 1, components[0])
                : ProcessTwoOperators(expected, components, 1, components[0]);

            if (total > 0)
            {
                Interlocked.Add(ref result, total);
            }
        });

        return result.ToString();
    }

    private static long ProcessTwoOperators(long expected, long[] components, int index, long currentTotal)
    {
        if (currentTotal > expected)
        {
            return 0;
        }

        if (index == components.Length)
        {
            return currentTotal == expected ? currentTotal : 0;
        }

        var result = ProcessTwoOperators(expected, components, index + 1, currentTotal + components[index]);

        if (result > 0)
        {
            return result;
        }

        result = ProcessTwoOperators(expected, components, index + 1, currentTotal * components[index]);

        return result;
    }

    private static long ProcessThreeOperators(long expected, long[] components, int index, long currentTotal)
    {
        if (currentTotal > expected)
        {
            return 0;
        }

        if (index == components.Length)
        {
            return currentTotal == expected ? currentTotal : 0;
        }

        var result = ProcessThreeOperators(expected, components, index + 1, currentTotal + components[index]);

        if (result > 0)
        {
            return result;
        }

        result = ProcessThreeOperators(expected, components, index + 1, currentTotal * components[index]);

        if (result > 0)
        {
            return result;
        }

        var digits = (long) Math.Log10(components[index]) + 1;

        var pow = 1;
        
        for (var i = 0; i < digits; i++)
        {
            pow *= 10;
        }

        result = ProcessThreeOperators(expected, components, index + 1, currentTotal * pow + components[index]);

        return result;
    }
}