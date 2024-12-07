using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._07;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var result = 0L;

        foreach (var line in Input)
        {
            var total = ProcessLine(line);

            if (total > 0)
            {
                result += total;
            }
        }
        
        return result.ToString();
    }

    private static long ProcessLine(string line)
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

            var test = Evaluate(components, operators);
            
            if (test == total)
            {
                return total;
            }
        }

        return 0;
    }

    private static long Evaluate(List<long> values, List<int> operators)
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
        }
        
        return left;
    }
}