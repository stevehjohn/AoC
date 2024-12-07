using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._07;

public abstract class Base : Solution
{
    public override string Description => "Bridge repair";

    protected string GetAnswer(bool isPart2)
    {
        var result = 0L;

        foreach (var line in Input)
        {
            var parts = line.Split(':', StringSplitOptions.TrimEntries);

            var expected = long.Parse(parts[0]);

            var components = parts[1].Split(' ').Select(long.Parse).ToArray();

            var total = isPart2 ? ProcessLineComplex(expected, components, 1, components[0]) : ProcessLineSimple(expected, components);

            if (total > 0)
            {
                result += total;
            }
        }
        
        return result.ToString();
    }
    
    private static long ProcessLineSimple(long expected, long[] components)
    {
        for (var i = 0; i < Math.Pow(2, components.Length); i++)
        {
            var total = 0L;
            
            for (var j = 0; j < components.Length; j++)
            {
                if (((1 << j) & i) == 0)
                {
                    total += components[j];
                }
                else
                {
                    total *= components[j];
                }

                if (total > expected)
                {
                    break;
                }
            }

            if (total == expected)
            {
                return total;
            }
        }

        return 0;
    }
    
    private static long ProcessLineComplex(long expected, long[] components, int index, long currentTotal)
    {
        if (currentTotal > expected)
        {
            return 0;
        }

        if (index == components.Length)
        {
            return currentTotal == expected ? currentTotal : 0;
        }

        var result = ProcessLineComplex(expected, components, index + 1, currentTotal + components[index]);
            
        if (result > 0)
        {
            return result;
        }

        result = ProcessLineComplex(expected, components, index + 1, currentTotal * components[index]);

        if (result > 0)
        {
            return result;
        }

        result = ProcessLineComplex(expected, components, index + 1, long.Parse($"{currentTotal}{components[index]}"));

        return result;
    }
}