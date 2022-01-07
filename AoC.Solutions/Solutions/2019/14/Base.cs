using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._14;

public abstract class Base : Solution
{
    public override string Description => "Replicator";

    protected Material RootMaterial { get; set; }

    private const string RootMaterialName = "FUEL";

    private const string BaseMaterialName = "ORE";

    protected void ParseInput()
    {
        RootMaterial = GetMaterial(RootMaterialName);
    }

    private Material GetMaterial(string name)
    {
        var line = Input.Single(i => i.EndsWith($" {name}"));

        var parts = line.Split("=>", StringSplitOptions.TrimEntries);

        var material = new Material(name, ParseMaterial(parts[1]).Quantity);

        var components = parts[0].Split(',', StringSplitOptions.TrimEntries);

        foreach (var component in components)
        {
            var componentData = ParseMaterial(component);

            if (componentData.Name == BaseMaterialName)
            {
                continue;
            }

            material.Components.Add(new Component(componentData.Quantity, GetMaterial(componentData.Name)));
        }

        return material;
    }

    private (string Name, int Quantity) ParseMaterial(string input)
    {
        var split = input.Split(' ', StringSplitOptions.TrimEntries);

        return (split[1], int.Parse(split[0]));
    }
}