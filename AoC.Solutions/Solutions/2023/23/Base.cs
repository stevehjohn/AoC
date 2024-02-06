using System.Collections.Concurrent;
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._23;

public abstract class Base : Solution
{
    public override string Description => "A long walk";

    protected readonly List<(int X, int Y)> Intersections = [];

    protected readonly List<int> Counts = [];

    private char[,] _map;

    private int _width;

    private int _height;

    private readonly ConcurrentDictionary<(int X, int Y), List<((int X, int Y) Start, (int X, int Y) End, int Steps)>> _edges = new();

    private readonly ConcurrentDictionary<(int, int, int, int), int> _reachCache = new();

    private readonly HashSet<(int, int)> _history = [];

    private int _lastSteps;
    
    protected bool FindLongestPath((int X, int Y) position, int steps, bool isPart2 = false)
    {
        if (position == Intersections[^2])
        {
            Counts.Add(steps + _lastSteps);
            
            return true;
        }

        var reaches = _edges[position];

        foreach (var item in reaches)
        {
            if (_history.Add(item.End))
            {
                var result = FindLongestPath(item.End, steps + item.Steps, isPart2);

                if ((! isPart2 || Counts.Count > 100_000) && result)
                {
                    return true;
                }

                _history.Remove(item.End);
            }
        }

        return false;
    }

    protected void CreateEdges(bool isPart2 = false)
    {
        FindIntersections();

        Parallel.ForEach(Intersections, intersection =>
        {
            foreach (var other in Intersections)
            {
                if (other == intersection)
                {
                    continue;
                }

                var distance = CanReach(intersection, other, isPart2);

                if (other == (_width - 2, _height - 1) && _lastSteps == 0)
                {
                    _lastSteps = distance;
                }

                if (distance > 0)
                {
                    if (! _edges.ContainsKey(intersection))
                    {
                        _edges[intersection] = [(intersection, other, distance)];
                    }
                    else
                    {
                        var insertAt = 0;

                        foreach (var edge in _edges[intersection])
                        {
                            if (edge.Steps < distance)
                            {
                                break;
                            }
                        
                            insertAt++;
                        }
                        
                        _edges[intersection].Insert(insertAt, (intersection, other, distance));
                    }
                }
            }
        });
    }

    private int CanReach((int X, int Y) start, (int X, int Y) end, bool isPart2)
    {
        if (isPart2 && _reachCache.TryGetValue((end.X, end.Y, start.X, start.Y), out var distance))
        {
            return distance;
        }

        distance = CanReachInternal(start, end, isPart2);

        _reachCache.TryAdd((start.X, start.Y, end.X, end.Y), distance);

        return distance;
    }

    private int CanReachInternal((int X, int Y) start, (int X, int Y) end, bool isPart2)
    {
        var queue = new Queue<((int X, int Y) Position, int Steps)>();
        
        queue.Enqueue((start, 0));

        var visited = new HashSet<(int, int)>();

        while (queue.TryDequeue(out var node))
        {
            var (position, steps) = node;
            
            if (! visited.Add(position))
            {
                continue;
            }
            
            if (position == end)
            {
                return steps;
            }

            if (position != start && Intersections.Contains(position))
            {
                continue;
            }

            if (position.X > 0 && _map[position.X - 1, position.Y] != '#' && (_map[position.X - 1, position.Y] != '>' || isPart2))
            {
                queue.Enqueue(((position.X - 1, position.Y), steps + 1));
            }

            if (position.X < _width - 1 && _map[position.X + 1, position.Y] != '#')
            {
                queue.Enqueue(((position.X + 1, position.Y), steps + 1));
            }

            if (position.Y > 0 && _map[position.X, position.Y - 1] != '#' && (_map[position.X, position.Y - 1] != 'v' || isPart2))
            {
                queue.Enqueue(((position.X, position.Y - 1), steps + 1));
            }

            if (position.Y < _height - 1 && _map[position.X, position.Y + 1] != '#')
            {
                queue.Enqueue(((position.X, position.Y + 1), steps + 1));
            }
        }

        return 0;
    }

    private void FindIntersections()
    {
        Intersections.Add((1, 0));
        
        for (var x = 1; x < _width - 1; x++)
        {
            for (var y = 1; y < _height - 1; y++)
            {
                var count = 0;

                count += _map[x - 1, y] == '#' ? 1 : 0;
                count += _map[x + 1, y] == '#' ? 1 : 0;
                count += _map[x, y - 1] == '#' ? 1 : 0;
                count += _map[x, y + 1] == '#' ? 1 : 0;

                if (count < 2 && _map[x, y] != '#')
                {
                    Intersections.Add((x, y));
                }
            }
        }
        
        Intersections.Add((_width - 2, _height - 1));
    }

    protected void ParseInput()
    {
        _map = Input.To2DArray();

        _width = _map.GetLength(0);

        _height = _map.GetLength(1);
    }
}