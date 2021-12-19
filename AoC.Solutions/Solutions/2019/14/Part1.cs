using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._14;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly List<Reaction> _reactions = new();

    private readonly Dictionary<string, int> _stock = new();

    private const string EndMaterial = "FUEL";

    private const string BaseMaterial = "ORE";

    public override string GetAnswer()
    {
        ParseInput();

        var endMaterial = _reactions.Single(r => r.Name == EndMaterial);

        var ore = GetRequiredOre(endMaterial, endMaterial.AmountCreated);

        return "TESTING";
    }

    private int GetRequiredOre(Reaction reaction, int requiredAmount)
    {
        if (_stock[reaction.Name] >= requiredAmount)
        {
            _stock[reaction.Name] -= requiredAmount;

            return 0;
        }

        var total = 0;

        foreach (var component in reaction.ComponentsRequired)
        {
            if (component.Key == BaseMaterial)
            {
                total += component.Value;

                _stock[reaction.Name] += reaction.AmountCreated;

                continue;
            }

            var requirement = _reactions.Single(r => r.Name == component.Key);

            total += GetRequiredOre(requirement, requiredAmount * requirement.AmountCreated);
        }

        return total;
    }

    private void ParseInput()
    {
        foreach (var line in Input)
        {
            var reaction = ParseLine(line);

            _reactions.Add(reaction);

            _stock.Add(reaction.Name, 0);
        }
    }

    private Reaction ParseLine(string line)
    {
        var io = line.Split("=>", StringSplitOptions.TrimEntries);

        var output = ParseComponent(io[1]);

        var reaction = new Reaction(output.Name, output.Quantity);

        var inputs = io[0].Split(',', StringSplitOptions.TrimEntries);

        foreach (var input in inputs)
        {
            var component = ParseComponent(input);

            reaction.ComponentsRequired.Add(component.Name, component.Quantity);
        }

        return reaction;
    }

    private (int Quantity, string Name) ParseComponent(string component)
    {
        var split = component.Split(' ', StringSplitOptions.TrimEntries);

        return (int.Parse(split[0]), split[1]);
    }
}