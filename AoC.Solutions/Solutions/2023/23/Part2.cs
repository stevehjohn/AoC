using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._23;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly List<(int X, int Y)> _intersections = new();

    private readonly Dictionary<int, ((int X, int Y) Start, (int X, int Y) End, int Steps)> _edges = new();

    private readonly Dictionary<(int X, int Y), List<int>> _startIndexes = new();
    
    private readonly HashSet<(int, int)> _history = new();

    private readonly List<int> _counts = new();
    
    public override string GetAnswer()
    {
        ParseInput();
        
        CreateEdges();

        FindLongestPath(_intersections[0], 0);
        
        return _counts.Max().ToString();
    }

    private void FindLongestPath((int X, int Y) position, int steps)
    {
        if (position == _intersections[^1])
        {
            _counts.Add(steps);
            
            return;
        }

        var reaches = _startIndexes[position];

        foreach (var id in reaches)
        {
            var item = _edges[id];
            
            if (_history.Add(item.End))
            {
                FindLongestPath(item.End, steps + item.Steps);

                _history.Remove(item.End);
            }
        }
    }

    private void CreateEdges()
    {
        FindIntersections();

        var id = 0;
        
        foreach (var intersection in _intersections)
        {
            foreach (var other in _intersections)
            {
                if (other == intersection)
                {
                    continue;
                }
        
                var distance = CanReach(intersection, other);
        
                if (distance > 0)
                {
                    _edges.Add(id, (intersection, other, distance));

                    if (! _startIndexes.ContainsKey(intersection))
                    {
                        _startIndexes[intersection] = new List<int> { id };
                    }
                    else
                    {
                        _startIndexes[intersection].Add(id);
                    }

                    id++;
                }
            }
        }
    }

    private int CanReach((int X, int Y) start, (int X, int Y) end)
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

            if (position != start && _intersections.Contains(position))
            {
                continue;
            }

            if (position.X > 0 && Map[position.X - 1, position.Y] != '#')
            {
                queue.Enqueue(((position.X - 1, position.Y), steps + 1));
            }

            if (position.X < Width - 1 && Map[position.X + 1, position.Y] != '#')
            {
                queue.Enqueue(((position.X + 1, position.Y), steps + 1));
            }

            if (position.Y > 0 && Map[position.X, position.Y - 1] != '#')
            {
                queue.Enqueue(((position.X, position.Y - 1), steps + 1));
            }

            if (position.Y < Height - 1 && Map[position.X, position.Y + 1] != '#')
            {
                queue.Enqueue(((position.X, position.Y + 1), steps + 1));
            }
        }

        return 0;
    }

    private void FindIntersections()
    {
        _intersections.Add((1, 0));
        
        for (var x = 1; x < Width - 1; x++)
        {
            for (var y = 1; y < Height - 1; y++)
            {
                var count = 0;

                count += Map[x - 1, y] == '#' ? 1 : 0;
                count += Map[x + 1, y] == '#' ? 1 : 0;
                count += Map[x, y - 1] == '#' ? 1 : 0;
                count += Map[x, y + 1] == '#' ? 1 : 0;

                if (count < 2 && Map[x, y] != '#')
                {
                    _intersections.Add((x, y));
                }
            }
        }
        
        _intersections.Add((Width - 2, Height - 1));
    }
}