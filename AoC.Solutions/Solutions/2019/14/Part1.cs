using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._14;

[UsedImplicitly]
public class Part1 : Solution
{
    private readonly List<Reaction> _reactions = new();

    public override string GetAnswer()
    {
        ParseInput();

        var fuel = _reactions.Single(r => r.Result.Name == "FUEL");

        var total = GetOre(fuel);

        return "TEST";
    }

    private void ParseInput()
    {
        foreach (var line in Input)
        {
            var io = line.Split("=>", StringSplitOptions.TrimEntries);

            var reaction = new Reaction
                           {
                               Result = ParseMatter(io[1])
                           };

            var input = io[0].Split(',', StringSplitOptions.TrimEntries);

            foreach (var matter in input)
            {
                reaction.Materials.Add(ParseMatter(matter));
            }

            _reactions.Add(reaction);
        }
    }

    private Matter ParseMatter(string input)
    {
        var parts = input.Split(' ', StringSplitOptions.TrimEntries);

        return new Matter
               {
                   Mass = int.Parse(parts[0]),
                   Name = parts[1]
               };
    }

    private int GetOre(Reaction node, int sum = 0)
    {
        foreach (var material in node.Materials)
        {
            if (material.Name == "ORE")
            {
                continue;
            }

            sum += GetOre(_reactions.Single(r => r.Result.Name == material.Name), sum);
        }

        return sum;
    }
}