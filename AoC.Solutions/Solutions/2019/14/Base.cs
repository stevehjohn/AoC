using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._14;

public abstract class Base : Solution
{
    public override string Description => "Replicator";

    protected Material RootMaterial { get; set; }

    protected const string BaseMaterialName = "ORE";

    private const string RootMaterialName = "FUEL";

    private readonly Dictionary<string, Material> _materials = new();

    protected void ParseInput()
    {
        RootMaterial = GetMaterial(RootMaterialName);

        _materials.Clear();
    }

    private Material GetMaterial(string name)
    {
        var line = Input.Single(i => i.EndsWith($" {name}"));

        var parts = line.Split("=>", StringSplitOptions.TrimEntries);

        var material = new Material(name, ParseMaterial(parts[1]).Quantity);

        if (! _materials.ContainsKey(material.Name))
        {
            _materials.Add(material.Name, material);
        }

        var components = parts[0].Split(',', StringSplitOptions.TrimEntries);

        foreach (var component in components)
        {
            var componentData = ParseMaterial(component);

            if (componentData.Name == BaseMaterialName)
            {
                material.Components.Add(new Component(componentData.Quantity, new Material(BaseMaterialName, 0)));

                continue;
            }

            if (_materials.ContainsKey(componentData.Name))
            {
                material.Components.Add(new Component(componentData.Quantity, _materials[componentData.Name]));

                continue;
            }

            material.Components.Add(new Component(componentData.Quantity, GetMaterial(componentData.Name)));
        }

        return material;
    }

    private static (string Name, int Quantity) ParseMaterial(string input)
    {
        var split = input.Split(' ', StringSplitOptions.TrimEntries);

        return (split[1], int.Parse(split[0]));
    }
}