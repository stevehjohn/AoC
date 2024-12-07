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
        var operators = new List<int>();
        
        for (var i = 0; i < Math.Pow(3, components.Length); i++)
        {
            operators.Clear();
            
            var current = i;
            
            for (var j = 0; j < components.Length - 1; j++)
            {
                operators.Add(current % 3);
                
                current /= 3;
            }

            var total = Evaluate(expected, components, operators);
            
            if (total == expected)
            {
                return total;
            }
        }

        return 0;
    }

    private static long Evaluate(long expected, long[] values, List<int> operators)
    {
        var left = values[0];
        
        for (var i = 0; i < operators.Count; i++)
        {
            switch (operators[i])
            {
                case 0:
                    left += values[i + 1];
                    break;
                
                case 1:
                    left *= values[i + 1];
                    break;
                
                default:
                    left = long.Parse($"{left}{values[i + 1]}");
                    break;
            }

            if (left > expected)
            {
                return 0;
            }
        }
        
        return left;
    }
}