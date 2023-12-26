using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._23;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly List<(int X, int Y)> _intersections = new();

    private readonly List<((int X, int Y) Start, (int X, int Y) End, int Steps)> _edges = new();
    
    public override string GetAnswer()
    {
        ParseInput();
        
        CreateEdges();

        var longest = FindLongestPath();
        
        return longest.ToString();
    }

    private int FindLongestPath()
    {
        var queue = new Queue<((int X, int Y) Position, int Steps, HashSet<(int, int)> History)>();
        
        queue.Enqueue((_intersections[0], 0, new HashSet<(int, int)>()));

        var counts = new List<int>();

        var max = 0;
        
        while (queue.TryDequeue(out var node))
        {
            if (! node.History.Add(node.Position))
            {
                continue;
            }

            if (node.Position == _intersections[^1])
            {
                counts.Add(node.Steps);

                if (node.Steps > max)
                {
                    Console.WriteLine(node.Steps);

                    max = node.Steps;
                }

                continue;
            }

            var reaches = _edges.Where(e => e.Start == node.Position).ToList();

            foreach (var item in reaches)
            {
                queue.Enqueue(((item.End), node.Steps + item.Steps, new HashSet<(int, int)>(node.History)));
            }
        }

        return counts.Max();
    }

    private void CreateEdges()
    {
        FindIntersections();

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
                    _edges.Add((intersection, other, distance));
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