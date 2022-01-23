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

        foreach (var tile in BlackTiles)
        {
            var neighbors = CountNeighbors(tile);

            if (neighbors == 1)
            {
                // Check neighbors for white tile candidates

            }
            else if (neighbors is 0 or > 2)
            {
                flips.Add(tile);
            }
        }

        //for (var z = BlackTiles.Min(p => p.Z) - 1; z <= BlackTiles.Max(p => p.Z) + 1; z++)
        //{
        //    for (var y = BlackTiles.Min(p => p.Y) - 1; y <= BlackTiles.Max(p => p.Y) + 1; y++)
        //    {
        //        for (var x = BlackTiles.Min(p => p.X) - 1; x <= BlackTiles.Max(p => p.X) + 1; x++)
        //        {
        //            var position = new Point(x, y, z);

        //            var neighbors = CountNeighbors(position);

        //            if (BlackTiles.Contains(position))
        //            {
        //                if (neighbors is 0 or > 2)
        //                {
        //                    flips.Add(position);
        //                }
        //            }
        //            else
        //            {
        //                if (neighbors == 2)
        //                {
        //                    flips.Add(position);
        //                }
        //            }
        //        }
        //    }
        //}

        foreach (var point in flips)
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

    private int CountNeighbors(Point position)
    {
        var neighbors = 0;

        foreach (var neighbor in _neighbors)
        {
            neighbors += BlackTiles.Contains(new Point(position.X + neighbor.X, position.Y + neighbor.Y, position.Z + neighbor.Z)) ? 1 : 0;
        }

        return neighbors;
    }
}