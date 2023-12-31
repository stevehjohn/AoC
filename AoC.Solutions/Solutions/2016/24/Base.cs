using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._24;

public abstract class Base : Solution
{
    public override string Description => "Air duct spelunking";

    private bool[,] _map;

    private int _width;

    private int _height;

    private readonly List<(char Id, Point Position)> _pointsOfInterest = new();

    private readonly Dictionary<string, int> _distancePairs = new();

    protected int GetShortestPath(bool isPart2 = false)
    {
        var points = _pointsOfInterest.Select(p => p.Id).ToArray();

        var permutations = points.GetPermutations().Where(p => p[0] == '0');

        var min = int.MaxValue;

        foreach (var permutation in permutations)
        {
            var totalDistance = 0;

            for (var i = 0; i < permutation.Length - 1; i++)
            {
                if (! _distancePairs.TryGetValue($"{permutation[i]}{permutation[i + 1]}", out var distance))
                {
                    distance = _distancePairs[$"{permutation[i + 1]}{permutation[i]}"];
                }

                totalDistance += distance;
            }

            if (isPart2)
            {
                if (! _distancePairs.TryGetValue($"{permutation[^1]}0", out var d))
                {
                    d = _distancePairs[$"0{permutation[^1]}"];
                }

                totalDistance += d;
            }

            if (totalDistance < min)
            {
                min = totalDistance;
            }
        }

        return min;
    }

    protected void GetDistancePairs()
    {
        for (var o = 0; o < _pointsOfInterest.Count - 1; o++)
        {
            for (var i = o + 1; i < _pointsOfInterest.Count; i++)
            {
                _distancePairs.Add($"{_pointsOfInterest[o].Id}{_pointsOfInterest[i].Id}", GetShortestDistance(_pointsOfInterest[o].Position, _pointsOfInterest[i].Position));
            }
        }
    }

    private int GetShortestDistance(Point a, Point b)
    {
        var queue = new PriorityQueue<(Point Position, int Steps), int>();

        queue.Enqueue((a, 0), 0);

        var visited = new HashSet<Point> { a };

        while (queue.Count > 0)
        {
            var item = queue.Dequeue();

            var position = item.Position;

            if (position.Equals(b))
            {
                return item.Steps;
            }

            if (! _map[position.X, position.Y - 1])
            {
                var newPosition = new Point(position.X, position.Y - 1);

                if (! visited.Contains(newPosition))
                {
                    queue.Enqueue((newPosition, item.Steps + 1), item.Steps + 1);

                    visited.Add(newPosition);
                }
            }

            if (! _map[position.X + 1, position.Y])
            {
                var newPosition = new Point(position.X + 1, position.Y);

                if (! visited.Contains(newPosition))
                {
                    queue.Enqueue((newPosition, item.Steps + 1), item.Steps + 1);

                    visited.Add(newPosition);
                }
            }

            if (! _map[position.X, position.Y + 1])
            {
                var newPosition = new Point(position.X, position.Y + 1);

                if (! visited.Contains(newPosition))
                {
                    queue.Enqueue((newPosition, item.Steps + 1), item.Steps + 1);

                    visited.Add(newPosition);
                }
            }

            if (! _map[position.X - 1, position.Y])
            {
                var newPosition = new Point(position.X - 1, position.Y);

                if (! visited.Contains(newPosition))
                {
                    queue.Enqueue((newPosition, item.Steps + 1), item.Steps + 1);

                    visited.Add(newPosition);
                }
            }
        }

        throw new PuzzleException("Solution not found.");
    }

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        _map = new bool[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                var c = Input[y][x];

                if (char.IsNumber(c))
                {
                    _pointsOfInterest.Add((c, new Point(x, y)));
                }

                _map[x, y] = c == '#';
            }
        }
    }
}