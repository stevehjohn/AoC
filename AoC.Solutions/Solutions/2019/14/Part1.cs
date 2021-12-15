using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._14;

[UsedImplicitly]
public class Part1 : Solution
{
    public override string Description => "Nanofactory replicator";

    private readonly List<Reaction> _reactions = new();

    private readonly Dictionary<string, int> _stock = new();

    private const string BaseMaterial = "ORE";

    public override string GetAnswer()
    {
        ParseInput();

        var fuel = _reactions.Single(r => r.Result.Name == "FUEL");

        foreach (var reaction in _reactions)
        {
            _stock.Add(reaction.Result.Name, 0);
        }

        _stock.Add(BaseMaterial, int.MaxValue);

        var total = GetOre(fuel);

        return total.ToString();
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

    private static Matter ParseMatter(string input)
    {
        var parts = input.Split(' ', StringSplitOptions.TrimEntries);

        return new Matter
               {
                   Amount = int.Parse(parts[0]),
                   Name = parts[1]
               };
    }

    private int GetOre(Reaction reaction)
    {
        var total = 0;

        foreach (var material in reaction.Materials)
        {
            if (_stock[material.Name] >= material.Amount)
            {
                _stock[material.Name] -= material.Amount;

                continue;
            }

            if (material.Name == BaseMaterial)
            {
                _stock[reaction.Result.Name] += reaction.Result.Amount;

                return reaction.Result.Amount * material.Amount;
            }

            total += GetOre(_reactions.Single(r => r.Result.Name == material.Name));
        }

        return total;
    }
}