using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._07;

public abstract class Base : Solution
{
    public override string Description => "Bridge repair";

    protected static long ProcessLineSimple(string line)
    {
        var parts = line.Split(':', StringSplitOptions.TrimEntries);

        var total = long.Parse(parts[0]);

        var components = parts[1].Split(' ').Select(long.Parse).ToList();

        for (var i = 0; i < Math.Pow(2, components.Count); i++)
        {
            var test = 0L;
            
            for (var j = 0; j < components.Count; j++)
            {
                if (((1 << j) & i) == 0)
                {
                    test += components[j];
                }
                else
                {
                    test *= components[j];
                }

                if (test > total)
                {
                    break;
                }
            }

            if (test == total)
            {
                return total;
            }
        }

        return 0;
    }

    protected static long ProcessLineComplex(string line)
    {
        var parts = line.Split(':', StringSplitOptions.TrimEntries);

        var total = long.Parse(parts[0]);

        var components = parts[1].Split(' ').Select(long.Parse).ToList();

        var operators = new List<int>();
        
        for (var i = 0; i < Math.Pow(3, components.Count); i++)
        {
            operators.Clear();
            
            var current = i;
            
            for (var j = 0; j < components.Count - 1; j++)
            {
                operators.Add(current % 3);
                
                current /= 3;
            }

            var test = Evaluate(total, components, operators);
            
            if (test == total)
            {
                return total;
            }
        }

        return 0;
    }

    private static long Evaluate(long expected, List<long> values, List<int> operators)
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