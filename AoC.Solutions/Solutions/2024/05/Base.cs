using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._05;

public abstract class Base : Solution
{
    public override string Description => "Print queue";

    protected readonly Dictionary<int, List<int>> Rules = [];

    protected readonly List<int[]> Updates = [];
    
    protected void ParseInput()
    {
        var i = 0;

        while (! string.IsNullOrEmpty(Input[i]))
        {
            var parts = Input[i].Split('|');

            var left = int.Parse(parts[0]);

            var right = int.Parse(parts[1]);
            
            if (Rules.TryGetValue(left, out var rule))
            {
                rule.Add(right);
            }
            else
            {
                Rules.Add(left, [right]);
            }   

            i++;
        }

        i++;

        while (i < Input.Length)
        {
            Updates.Add(Input[i].Split(',').Select(int.Parse).ToArray());

            i++;
        }
    }

    protected bool IsCorrect(int[] update)
    {
        for (var i = 0; i < update.Length; i++)
        {
            if (i > 0)
            {
                var left = update[i - 1];

                var right = update[i];

                if (Rules.TryGetValue(right, out var rules))
                {
                    if (rules.Contains(left))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
}