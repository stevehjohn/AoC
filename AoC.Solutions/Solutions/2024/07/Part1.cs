using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._07;

[UsedImplicitly]
public class Part1 : Base
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
            }

            if (test == total)
            {
                return total;
            }
        }

        return 0;
    }
}