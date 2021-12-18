using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._14;

[UsedImplicitly]
public class Part1 : Base
{
    public override string Description => "Nanofactory replicator";

    private readonly Dictionary<string, int> _stock = new();

    private Matter _fuel;

    private const string BaseMaterial = "ORE";

    private const string EndMaterial = "FUEL";

    public override string GetAnswer()
    {
        _fuel = ProcessInput(EndMaterial);

        var fuel = GetOre(_fuel, _fuel.Amount).ToString();

        return fuel;
    }

    public Matter ProcessInput(string outputMatter)
    {
        var io = Input.Single(i => i.EndsWith($" {outputMatter}")).Split("=>", StringSplitOptions.TrimEntries);

        var output = ParseMatter(io[1]);

        var matter = new Matter
        {
            Amount = output.Amount,
            Name = output.Name
        };

        matter.Components.AddRange(ProcessComponents(io[0]));

        return matter;
    }

    private List<Matter> ProcessComponents(string input)
    {
        var components = input.Split(',', StringSplitOptions.TrimEntries);

        var result = new List<Matter>();

        foreach (var component in components)
        {
            var matterParsed = ParseMatter(component);

            if (matterParsed.Name == BaseMaterial)
            {
                return new List<Matter>
                       {
                           new()
                           {
                               Amount = matterParsed.Amount,
                               Name = BaseMaterial
                           }
                       };
            }

            var matter = new Matter
            {
                Amount = matterParsed.Amount,
                Name = matterParsed.Name
            };

            matter.Components.AddRange(ProcessComponents(Input.Single(i => i.EndsWith($" {matterParsed.Name}"))));

            result.Add(matter);
        }

        return result;
    }

    private int GetOre(Matter node, int requiredAmount)
    {
        if (node.Name == BaseMaterial)
        {
            return node.Amount * requiredAmount;
        }

        if (!_stock.ContainsKey(node.Name))
        {
            _stock.Add(node.Name, 0);
        }

        if (_stock[node.Name] >= node.Amount)
        {
            _stock[node.Name] -= node.Amount;

            return node.Amount;
        }

        foreach (var matter in node.Components)
        {
            // Create the stock and decrement it.
        }

        return 0;
    }

    private static (int Amount, string Name) ParseMatter(string matter)
    {
        var split = matter.Split(' ', StringSplitOptions.TrimEntries);

        return (int.Parse(split[0]), split[1]);
    }
}