using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._22;

public abstract class Base : Solution
{
    public override string Description => "Grid computing";

    protected List<(int X, int Y, int Used, int Available)> GetViableNodes()
    {
        var nodes = Input
            .Skip(2)
            .Select(ParseLine)
            .ToArray();

        var result = new List<(int X, int Y, int Used, int Available)>();

        for (var a = 0; a < nodes.Length; a++)
        {
            var nodeA = nodes[a];

            if (nodeA.Used == 0)
            {
                continue;
            }

            for (var b = 0; b < nodes.Length; b++)
            {
                if (a == b)
                {
                    continue;
                }

                if (nodeA.Used <= nodes[b].Available)
                {
                    result.Add(nodeA);
                }
            }
        }

        return result;
    }

    protected static (int X, int Y, int Used, int Available) ParseLine(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        var coordsPart = parts[0][15..].Split('-');

        var x = int.FastParse(coordsPart[0][1..]);
        
        var y = int.FastParse(coordsPart[1][1..]);

        return (x, y, int.FastParse(parts[2][..^1]), int.FastParse(parts[3][..^1]));
    }
}