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
        var line = Input.Single(i => i.EndsWith($" {outputMatter}"));

        var io = line.Split("=>", StringSplitOptions.TrimEntries);

        var output = ParseMatter(io[1]);

        var matter = new Matter
                     {
                         Amount = output.Amount,
                         Name = output.Name
                     };

        var input = io[0].Split(',', StringSplitOptions.TrimEntries);

        foreach (var item in input)
        {
            var parsed = ParseMatter(item);

            matter.Components.Add(new Matter
                                  {
                                      Amount = parsed.Amount,
                                      Name = parsed.Name
                                  });
        }

        return matter;
    }

    private (int Amount, string Name) ParseMatter(string matter)
    {
        var split = matter.Split(' ', StringSplitOptions.TrimEntries);

        return (int.Parse(split[0]), split[1]);
    }
}