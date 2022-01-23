using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._24;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly List<Point> _neighbors = new();

    public override string GetAnswer()
    {
        ParseInput();

        FlipTiles();

        InitialiseNeighbors();

        for (var i = 0; i < 10; i++)
        {
            PlayGameOfLife();

            Console.WriteLine(BlackTiles.Count);
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

        Dump();

        foreach (var tile in BlackTiles)
        {
            var neighbors = CountNeighbors(tile);

            if (neighbors == 1)
            {
                var emptyNeighbors = GetEmptyNeighbors(tile);

                foreach (var emptyNeighbor in emptyNeighbors)
                {
                    if (CountNeighbors(emptyNeighbor) == 2)
                    {
                        flips.Add(emptyNeighbor);
                    }
                }
            }
            else if (neighbors is 0 or > 2)
            {
                flips.Add(tile);
            }
        }

        foreach (var point in flips.Distinct())
        {
            if (BlackTiles.Contains(point))
            {
                BlackTiles.Remove(point);
            }
            else
            {
                BlackTiles.Add(point);
            }
        }
    }

    private void Dump()
    {
        Console.Clear();

        Console.CursorVisible = false;

        var points = new List<Point>();

        foreach (var tile in BlackTiles)
        {
            points.Add(new Point((int) (Math.Sqrt(3) * tile.X + Math.Sqrt(3) / 2 * tile.Y), 3 / 2 * tile.Y));
        }

        var xMin = points.Min(p => p.X);

        var yMin = points.Min(p => p.Y);

        foreach (var point in points)
        {
            Console.SetCursorPosition(-xMin + 1 + point.X, -yMin + 1 + point.Y);

            Console.Write('#');
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