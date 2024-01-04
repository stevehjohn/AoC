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
            HashSet<(int X, int Y, int Z, int T)> constellation;

            if (! constellations.Any(c => c.Contains(outer)))
            {
                constellation = [outer];
                
                constellations.Add(constellation);
            }
            else
            {
                var matches = constellations.Where(c => c.Contains(outer)).ToList();

                constellation = matches[0];

                if (matches.Count > 1)
                {
                    foreach (var match in matches.Skip(1))
                    {
                        match.ToList().ForEach(m => matches[0].Add(m));

                        constellations.Remove(match);
                    }
                }
            }

            foreach (var inner in points)
            {
                if (outer == inner)
                {
                    continue;
                }

                if (GetManhattanDistance(outer, inner) < 4)
                {
                    constellation.Add(inner);
                }
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