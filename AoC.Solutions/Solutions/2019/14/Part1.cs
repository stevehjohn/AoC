using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._14;

[UsedImplicitly]
public class Part1 : Solution
{
    public override string Description => "Nanofactory replicator";

    private readonly Dictionary<string, int> _stock = new();

    private Matter _fuel;

    private const string BaseMaterial = "ORE";

    private const string EndMaterial = "FUEL";

    public override string GetAnswer()
    {
        _fuel = ProcessInput(EndMaterial);

        return "FUDGE";
    }

    public Matter ProcessInput(string outputMatter)
    {
        var io = Input.Single(i => i.EndsWith($" {outputMatter}")).Split("=>", StringSplitOptions.TrimEntries);

        var output = ParseMatter(io[1]);

        return new Matter
               {
                   Amount = output.Amount,
                   Name = output.Name
               };
    }

    private (int Amount, string Name) ParseMatter(string matter)
    {
        var split = matter.Split(' ', StringSplitOptions.TrimEntries);

        return (int.Parse(split[0]), split[1]);
    }
}