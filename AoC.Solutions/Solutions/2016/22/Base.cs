using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._22;

public abstract class Base : Solution
{
    public override string Description => "Grid computing";

    protected List<(int X, int Y, int Used, int Available)> GetViableNodes()
    {
        var data = Input.Skip(2).ToList();

        var result = new List<(int X, int Y, int Used, int Available)>();

        for (var o = 0; o < data.Count; o++)
        {
            for (var i = 0; i < data.Count; i++)
            {
                if (o == i)
                {
                    continue;
                }

                var nodeA = ParseLine(data[o]);

                var nodeB = ParseLine(data[i]);

                if (nodeA.Used > 0 && nodeA.Used <= nodeB.Available)
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

        var x = int.Parse(coordsPart[0][1..]);

        var y = int.Parse(coordsPart[1][1..]);

        return (x, y, int.Parse(parts[2][..^1]), int.Parse(parts[3][..^1]));
    }
}