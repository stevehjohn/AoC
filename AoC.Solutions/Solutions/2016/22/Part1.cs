using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._22;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var viable = 0;

        var data = Input.Skip(2).ToList();

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
                    viable++;
                }
            }
        }

        return viable.ToString();
    }

    private (int Used, int Available) ParseLine(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        return (int.Parse(parts[2][..^1]), int.Parse(parts[3][..^1]));
    }
}