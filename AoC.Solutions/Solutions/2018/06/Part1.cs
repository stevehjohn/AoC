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

                // This could probably be sped up by: https://www.youtube.com/watch?v=BK5x7IUTIyU
                var lowestDistance = int.MaxValue;

                var secondLowestDistance = int.MaxValue;

                var closestPoint = new Point();

                for (var i = 0; i < Points.Length; i++)
                {
                    var distance = ManhattanDistance(Points[i], position);

                    if (distance < lowestDistance)
                    {
                        secondLowestDistance = lowestDistance;

                        lowestDistance = distance;

                        closestPoint = Points[i];
                    }
                    else if (distance < secondLowestDistance)
                    {
                        secondLowestDistance = distance;
                    }
                }

                if (lowestDistance != secondLowestDistance)
                {
                    if (! _counts.TryAdd(closestPoint, 1))
                    {
                        _counts[closestPoint]++;
                    }

                    if (position.X == 0 || position.X == Width - 1 || position.Y == 0 || position.Y == Height - 1)
                    {
                        _infinitePoints.Add(closestPoint);
                    }
                }
            }
        }
    }
}