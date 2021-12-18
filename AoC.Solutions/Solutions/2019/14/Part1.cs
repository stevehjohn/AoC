using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._14;

[UsedImplicitly]
public class Part1 : Base
{
    private List<Reaction> _reactions = new();

    private Dictionary<string, int> _stock = new();

    public override string GetAnswer()
    {
        ParseInput();

        throw new NotImplementedException();
    }

    private void ParseInput()
    {
        foreach (var line in Input)
        {
            _reactions.Add(ParseLine(line));
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