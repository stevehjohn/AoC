using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._14;

[UsedImplicitly]
public class Part1 : Solution
{
    private readonly List<Reaction> _reactions = new();

    private readonly Dictionary<string, int> _stock = new();

    public override string GetAnswer()
    {
        ParseInput();

        var fuel = _reactions.Single(r => r.Result.Name == "FUEL");

        foreach (var reaction in _reactions)
        {
            _stock.Add(reaction.Result.Name, 0);
        }

        var total = GetOre(fuel, fuel.Result.Amount);

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

    private int GetOre(Reaction node, int requiredAmount)
    {
        var total = 0;

        foreach (var matter in node.Materials)
        {
            if (matter.Name == "ORE")
            {
                return matter.Amount * requiredAmount;
            }

            if (_stock[matter.Name] >= matter.Amount)
            {
                _stock[matter.Name] -= matter.Amount;

                return matter.Amount;
            }

            _stock[matter.Name] += (int) Math.Ceiling((double) requiredAmount / matter.Amount) * matter.Amount;

            total += GetOre(_reactions.Single(r => r.Result.Name == matter.Name), matter.Amount);
        }

        return total;
    }
}