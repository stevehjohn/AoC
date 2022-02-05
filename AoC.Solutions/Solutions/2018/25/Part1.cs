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

    private static int CountConstellations(List<(int X, int Y, int Z, int T)> points)
    {
        var constellations = new List<HashSet<(int X, int Y, int Z, int T)>>();

        foreach (var outer in points)
        {
            var pairs = new HashSet<((int X, int Y, int Z, int T) Left, (int X, int Y, int Z, int T) Right)>();

            foreach (var inner in points)
            {
                if (outer == inner)
                {
                    continue;
                }

                if (GetManhattanDistance(outer, inner) < 4)
                {
                    pairs.Add((outer, inner));
                }
            }

            foreach (var pair in pairs)
            {
                if (! constellations.Any(c => c.Contains(pair.Left) || c.Contains(pair.Right)))
                {
                    constellations.Add(new HashSet<(int X, int Y, int Z, int T)> { pair.Left, pair.Right });

                    continue;
                }

                var constellation = constellations.SingleOrDefault(c => c.Contains(pair.Left));

                if (constellation != null)
                {
                    constellation.Add(pair.Right);

                    continue;
                }

                constellation = constellations.Single(c => c.Contains(pair.Right));

                constellation.Add(pair.Left);
            }
        }

        return constellations.Count;
    }

    private static int GetManhattanDistance((int X, int Y, int Z, int T) left, (int X, int Y, int Z, int T) right)
    {
        return Math.Abs(left.X - right.X) + Math.Abs(left.Y - right.Y) + Math.Abs(left.Z - right.Z) + Math.Abs(left.T - right.T);
    }

    private List<(int X, int Y, int Z, int T)> ParseInput()
    {
        var result = new List<(int X, int Y, int Z, int T)>();

        foreach (var line in Input)
        {
            var parts = line.Split(',', StringSplitOptions.TrimEntries);

            result.Add((int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3])));
        }

        return result;
    }
}