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
        var permutations = People.GetPermutations();

        var happiest = int.MinValue;

        foreach (var permutation in permutations)
        {
            var happiness = 0;

            for (var i = 0; i < permutation.Length; i++)
            {
                if (i == 0)
                {
                    happiness += Happiness[$"{permutation[i]}{permutation[^1]}"];

                    happiness += Happiness[$"{permutation[i]}{permutation[i + 1]}"];

                    continue;
                }

                if (i == permutation.Length - 1)
                {
                    happiness += Happiness[$"{permutation[i]}{permutation[i - 1]}"];

                    happiness += Happiness[$"{permutation[i]}{permutation[0]}"];

                    continue;
                }

                happiness += Happiness[$"{permutation[i]}{permutation[i - 1]}"];

                happiness += Happiness[$"{permutation[i]}{permutation[i + 1]}"];
            }

            if (happiness > happiest)
            {
                happiest = happiness;
            }
        }

        return happiest;
    }

    protected void ParseInput()
    {
        People = Input.Select(l => l.Split(' ', StringSplitOptions.TrimEntries)[0]).Distinct().ToArray();

        foreach (var line in Input)
        {
            var parts = line[..^1].Split(' ', StringSplitOptions.TrimEntries);

            Happiness.Add($"{parts[0]}{parts[10]}", int.Parse(parts[3]) * (parts[2] == "gain" ? 1 : -1));
        }
    }
}