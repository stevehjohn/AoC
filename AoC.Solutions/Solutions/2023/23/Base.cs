using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._23;

public abstract class Base : Solution
{
    public override string Description => "A long walk";

    protected readonly List<(int X, int Y)> Intersections = new();

    protected readonly List<int> Counts = new();

    private char[,] _map;

    private int _width;

    private int _height;

    private readonly Dictionary<(int X, int Y), List<((int X, int Y) Start, (int X, int Y) End, int Steps)>> _startIndexes = new();
    
    private readonly HashSet<(int, int)> _history = new();

    protected void FindLongestPath((int X, int Y) position, int steps)
    {
        if (position == Intersections[^1])
        {
            //Console.WriteLine(steps);
            
            Counts.Add(steps);
            
            return;
        }

        var reaches = _startIndexes[position];

        foreach (var item in reaches)
        {
            if (_history.Add(item.End))
            {
                FindLongestPath(item.End, steps + item.Steps);

                _history.Remove(item.End);
            }
        }
    }

    protected void CreateEdges(bool isPart2 = false)
    {
        FindIntersections();

        foreach (var intersection in Intersections)
        {
            foreach (var other in Intersections)
            {
                if (other == intersection)
                {
                    continue;
                }
        
                var distance = CanReach(intersection, other, isPart2);
        
                if (distance > 0)
                {
                    if (! _startIndexes.ContainsKey(intersection))
                    {
                        _startIndexes[intersection] = new List<((int X, int Y) Start, (int X, int Y) End, int Steps)> { (intersection, other, distance) };
                    }
                    else
                    {
                        _startIndexes[intersection].Add((intersection, other, distance));
                    }
                }
            }
        }
    }

    private int CanReach((int X, int Y) start, (int X, int Y) end, bool isPart2)
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