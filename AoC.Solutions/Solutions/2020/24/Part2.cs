using System.Runtime.InteropServices;
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

    private readonly Dictionary<Point, int> _counts = new();

    private readonly List<Point> _next = [];

    private void PlayGameOfLife()
    {
        _counts.Clear();

        foreach (var tile in BlackTiles)
        {
            foreach (var offset in _neighbors)
            {
                var point = new Point(tile.X + offset.X, tile.Y + offset.Y, tile.Z + offset.Z);

                ref var count = ref CollectionsMarshal.GetValueRefOrAddDefault(_counts, point, out _);

                count++;
            }
        }

        _next.Clear();

        foreach (var (point, count) in _counts)
        {
            if (count == 2 || count == 1 && BlackTiles.Contains(point))
            {
                _next.Add(point);
            }
        }

        BlackTiles.Clear();

        foreach (var point in _next)
        {
            BlackTiles.Add(point);
        }
    }
}