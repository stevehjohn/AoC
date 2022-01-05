using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._14;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly List<Material> _materials = new();

    private readonly Dictionary<string, int> _stock = new();

    private const string EndMaterial = "FUEL";

    private const string BaseMaterial = "ORE";

    private int _total;

    public override string GetAnswer()
    {
        ParseInput();

        var endMaterial = _materials.Single(r => r.Name == EndMaterial);

        _total = 0;

        return _total.ToString();
    }

    private void ParseInput()
    {
        foreach (var line in Input)
        {
            var reaction = ParseLine(line);

            _materials.Add(reaction);

            _stock.Add(reaction.Name, 0);
        }
    }

    private static Material ParseLine(string line)
    {
        var io = line.Split("=>", StringSplitOptions.TrimEntries);

        var material = ParseComponent(io[1]);

        var inputs = io[0].Split(',', StringSplitOptions.TrimEntries);

        foreach (var input in inputs)
        {
            var component = ParseComponent(input);

            material.Components.Add(component);
        }

        return material;
    }

    private static Material ParseComponent(string component)
    {
        var split = component.Split(' ', StringSplitOptions.TrimEntries);

        return new Material(split[1], int.Parse(split[0]));
    }
}