using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._25;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var points = ParseInput();

        var result = CountConstellations(points);

        return result.ToString();
    }

    private int CountConstellations(List<(int X, int Y, int z, int t)> points)
    {
        return 0;
    }

    private List<(int X, int Y, int z, int t)> ParseInput()
    {
        var result = new List<(int X, int Y, int Z, int t)>();

        foreach (var line in Input)
        {
            var parts = line.Split(',', StringSplitOptions.TrimEntries);

            result.Add((int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3])));
        }

        return result;
    }
}