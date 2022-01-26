using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._06;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly Dictionary<Point, int> _counts = new();

    private readonly HashSet<Point> _infinitePoints = new();

    public override string GetAnswer()
    {
        ParseInput();

        MapClosestPoints();

        var nonInfinite = _counts.Where(p => ! _infinitePoints.Contains(p.Key));

        return nonInfinite.Max(c => c.Value).ToString();
    }

    private void MapClosestPoints()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var position = new Point(x, y);

                // This could probably be sped up by: https://www.youtube.com/watch?v=BK5x7IUTIyU&t=666s
                var distances = Points.Select(p => (Distance: ManhattanDistance(p, position), Point: p)).OrderBy(d => d.Distance).ToList();

                if (distances[0].Distance != distances[1].Distance)
                {
                    if (_counts.ContainsKey(distances[0].Point))
                    {
                        _counts[distances[0].Point]++;
                    }
                    else
                    {
                        _counts.Add(distances[0].Point, 1);
                    }

                    if (position.X == 0 || position.X == Width - 1 || position.Y == 0 || position.Y == Height - 1)
                    {
                        _infinitePoints.Add(distances[0].Point);
                    }
                }
            }
        }
    }
}