
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._13;

public abstract class Base : Solution
{
    public override string Description => "Knights of the dinner table";

    protected string[] People;

    protected readonly Dictionary<string, int> Happiness = new();

    protected int Solve()
    {
        var count = People.Length;

        if (count < 2)
        {
            return 0;
        }

        var scores = new int[count, count];

        for (var i = 0; i < count; i++)
        {
            for (var j = i + 1; j < count; j++)
            {
                Happiness.TryGetValue(People[i] + People[j], out var forward);

                Happiness.TryGetValue(People[j] + People[i], out var backward);

                scores[i, j] = forward + backward;
                
                scores[j, i] = forward + backward;
            }
        }

        var remaining = count - 1;
        
        var combinations = 1 << remaining;

        var dp = new int[combinations * remaining];

        Array.Fill(dp, int.MinValue);

        for (var person = 0; person < remaining; person++)
        {
            var mask = 1 << person;

            dp[mask * remaining + person] = scores[0, person + 1];
        }

        for (var mask = 1; mask < combinations; mask++)
        {
            for (var last = 0; last < remaining; last++)
            {
                var current = dp[mask * remaining + last];

                if (current == int.MinValue)
                {
                    continue;
                }

                for (var next = 0; next < remaining; next++)
                {
                    var bit = 1 << next;

                    if ((mask & bit) != 0)
                    {
                        continue;
                    }

                    var nextMask = mask | bit;
                    
                    var index = nextMask * remaining + next;

                    var happiness = current + scores[last + 1, next + 1];

                    if (happiness > dp[index])
                    {
                        dp[index] = happiness;
                    }
                }
            }
        }

        var fullMask = combinations - 1;
        
        var best = int.MinValue;

        for (var last = 0; last < remaining; last++)
        {
            var current = dp[fullMask * remaining + last];

            if (current == int.MinValue)
            {
                continue;
            }

            var total = current + scores[last + 1, 0];

            if (total > best)
            {
                best = total;
            }
        }

        return best;
    }

    protected void ParseInput()
    {
        People = [..Input.Select(l => l.Split(' ', StringSplitOptions.TrimEntries)[0]).Distinct()];

        Happiness.Clear();

        foreach (var line in Input)
        {
            var parts = line[..^1].Split(' ', StringSplitOptions.TrimEntries);

            Happiness.Add($"{parts[0]}{parts[10]}", int.FastParse(parts[3]) * (parts[2] == "gain" ? 1 : -1));
        }
    }
}
