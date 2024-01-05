using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._24;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly List<Point> _neighbors = [];

    public override string GetAnswer()
    {
        ParseInput();

        FlipTiles();

        InitialiseNeighbors();

        for (var i = 0; i < 100; i++)
        {
            PlayGameOfLife();
        }

        return BlackTiles.Count.ToString();
    }

    private void InitialiseNeighbors()
    {
        _neighbors.Add(new Point(1, 0, -1));

        _neighbors.Add(new Point(1, -1));

        _neighbors.Add(new Point(0, -1, 1));

        _neighbors.Add(new Point(-1, 0, 1));

        _neighbors.Add(new Point(-1, 1));

        _neighbors.Add(new Point(0, 1, -1));
    }

    private void PlayGameOfLife()
    {
        var flips = new List<Point>();

        foreach (var tile in BlackTiles)
        {
            var neighbors = CountNeighbors(tile);

            var emptyNeighbors = GetEmptyNeighbors(tile);

            foreach (var emptyNeighbor in emptyNeighbors)
            {
                if (CountNeighbors(emptyNeighbor) == 2)
                {
                    flips.Add(emptyNeighbor);
                }
            }

            if (neighbors is 0 or > 2)
            {
                flips.Add(tile);
            }
        }

        foreach (var point in flips.Distinct())
        {
            if (! BlackTiles.Add(point))
            {
                BlackTiles.Remove(point);
            }
        }
    }

    private int CountNeighbors(Point position)
    {
        var neighbors = 0;

        foreach (var neighbor in _neighbors)
        {
            neighbors += BlackTiles.Contains(new Point(position.X + neighbor.X, position.Y + neighbor.Y, position.Z + neighbor.Z)) ? 1 : 0;
        }

        return neighbors;
    }

    private List<Point> GetEmptyNeighbors(Point position)
    {
        var emptyNeighbors = new List<Point>();

        foreach (var neighborOffset in _neighbors)
        {
            var neighbor = new Point(position.X + neighborOffset.X, position.Y + neighborOffset.Y, position.Z + neighborOffset.Z);

            if (! BlackTiles.Contains(neighbor))
            {
                emptyNeighbors.Add(neighbor);
            }
        }

        return emptyNeighbors;
    }
}