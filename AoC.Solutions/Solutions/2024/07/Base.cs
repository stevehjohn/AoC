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

            var total = ProcessLineSimple(expected, components);
            
            if (isPart2 && total == 0)
            {
                total = ProcessLineComplex(expected, components);
            }

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

    private static long ProcessLineComplex(long expected, long[] components)
    {
        for (var i = 0; i < Math.Pow(3, components.Length); i++)
        {
            var current = i;

            var left = components[0];
            
            for (var j = 0; j < components.Length - 1; j++)
            {
                switch (current % 3)
                {
                    case 0:
                        left += components[j + 1];
                        break;
                
                    case 1:
                        left *= components[j + 1];
                        break;
                
                    default:
                        left = long.Parse($"{left}{components[j + 1]}");
                        break;
                }

                current /= 3;
            }

            if (left == expected)
            {
                return left;
            }
        }

        return 0;
    }
}