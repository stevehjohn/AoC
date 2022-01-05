using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._14;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly List<Reaction> _reactions = new();

    private readonly Dictionary<string, int> _stock = new();

    private const string EndMaterial = "FUEL";

    private const string BaseMaterial = "ORE";

    private int _total;

    public override string GetAnswer()
    {
        ParseInput();

        var endMaterial = _reactions.Single(r => r.Name == EndMaterial);

        _total = 0;

        GetRequiredOre(endMaterial, 1, 0);

        return _total.ToString();
    }

    private void GetRequiredOre(Reaction reaction, int amountRequired, int level)
    {
        var padding = new string(' ', level * 2);

        Console.WriteLine($"{padding}{reaction.Name}");

        foreach (var component in reaction.ComponentsRequired)
        {
            Console.Write($"{padding}{component.Key}: {component.Value} ");

            Console.WriteLine($"{(component.Key == BaseMaterial ? string.Empty : $"Stock: {_stock[component.Key]}")}");

            if (component.Key == BaseMaterial)
            {
                while (_stock[reaction.Name] < amountRequired)
                {
                    _stock[reaction.Name] += reaction.AmountCreated;

                    _total += component.Value;

                    Console.WriteLine($"{padding}  {reaction.Name} stock {_stock[reaction.Name]}. Ore: {_total}");
                }

                continue;
            }

            var nextAmountRequired = component.Value * (int) Math.Ceiling((decimal) amountRequired / reaction.AmountCreated);

            GetRequiredOre(_reactions.Single(r => r.Name == component.Key), nextAmountRequired, level + 1);

            _stock[component.Key] -= nextAmountRequired;
        }
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

    private static Reaction ParseLine(string line)
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

    private static (int Quantity, string Name) ParseComponent(string component)
    {
        var split = component.Split(' ', StringSplitOptions.TrimEntries);

        return (int.Parse(split[0]), split[1]);
    }
}