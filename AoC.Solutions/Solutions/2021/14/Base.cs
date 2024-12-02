using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._14;

public abstract class Base : Solution
{
    public override string Description => "Ablative armour";

    protected string GetAnswer(int steps)
    {
        var pairs = new Dictionary<string, long>();

        for (var i = 0; i < Input[0].Length - 1; i++)
        {
            var pair = Input[0].Substring(i, 2);

            pairs.TryAdd(pair, 1);
        }

        for (var i = 0; i < steps; i++)
        {
            var pairsToAdd = new Dictionary<string, long>();

            for (var r = 2; r < Input.Length; r++)
            {
                var rule = Input[r].Split("->", StringSplitOptions.TrimEntries);

                if (! pairs.TryGetValue(rule[0], out var value) || value == 0)
                {
                    continue;
                }

                var newPair = $"{rule[0][0]}{rule[1][0]}";

                var incrementBy = pairs[rule[0]];

                if (! pairsToAdd.TryAdd(newPair, incrementBy))
                {
                    pairsToAdd[newPair] += incrementBy;
                }

                newPair = $"{rule[1][0]}{rule[0][1]}";

                if (! pairsToAdd.TryAdd(newPair, incrementBy))
                {
                    pairsToAdd[newPair] += incrementBy;
                }

                pairs[rule[0]] = 0;
            }

            foreach (var pair in pairsToAdd)
            {
                if (pairs.ContainsKey(pair.Key))
                {
                    pairs[pair.Key] += pair.Value;
                }
                else
                {
                    pairs.Add(pair.Key, pair.Value);
                }
            }
        }

        var counts = new long[26];

        foreach (var pair in pairs.Where(p => p.Value > 0))
        {
            counts[pair.Key[0] - 'A'] += pair.Value;
            counts[pair.Key[1] - 'A'] += pair.Value;
        }

        counts[Input[0][0] - 'A']++;
        counts[Input[0][Input[0].Length - 1] - 'A']++;

        return (counts.Max() / 2L - counts.Where(c => c > 0).Min() / 2L).ToString();
    }
}