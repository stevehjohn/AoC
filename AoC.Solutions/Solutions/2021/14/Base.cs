using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._14;

public abstract class Base : Solution
{
    public string GetAnswer(int steps)
    {
        var pairs = new Dictionary<string, int>();

        for (var i = 0; i < Input[0].Length - 1; i++)
        {
            var pair = Input[0].Substring(i, 2);

            if (! pairs.ContainsKey(pair))
            {
                pairs.Add(pair, 1);
            }
        }

        for (var i = 0; i < steps; i++)
        {
            for (var r = 2; r < Input.Length; r++)
            {
                var rule = Input[r].Split("->", StringSplitOptions.TrimEntries);

                if (! pairs.ContainsKey(rule[0]))
                {
                    continue;
                }

                var newPair = $"{rule[0][0]}{rule[1][0]}";

                if (! pairs.ContainsKey(newPair))
                {
                    pairs.Add(newPair, 1);
                }
                else
                {
                    pairs[newPair]++;
                }
            }
        }

        return "TEST";
    }
}