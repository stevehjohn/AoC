using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._23;

[UsedImplicitly]
public class Part2 : Base
{
    private Edge _start;

    private List<Edge> _edges = new();

    private int _id = 0;
    
    public override string GetAnswer()
    {
        ParseInput();
        
        CreateEdges();

        Dump();
        
        // foreach (var edge in _edges)
        // {
        //     Console.WriteLine($"{edge.Id}: {edge.Connections.Count}");
        // }
        
        var result = FindLongestPath();

        return result.ToString();
    }

    private void Dump()
    {
        for (var y = 0; y < Width; y++)
        {
            for (var x = 0; x < Height; x++)
            {
                if (_edges.Any(e => e.X == x && e.Y == y))
                {
                    Console.Write('*');
                }
                else
                {
                    Console.Write(Map[x, y] == '#' ? '#' : ' ');
                }
            }
            
            Console.WriteLine();
        }
    }

    private int FindLongestPath()
    {
        var queue = new Queue<(Edge Edge, int Steps, HashSet<int> History)>();

        queue.Enqueue((_start, 0, new HashSet<int> { _start.Id }));

        var lengths = new List<int>();
        
        while (queue.TryDequeue(out var node))
        {
            if (node.Edge.Connections.Count == 0)
            {
                lengths.Add(node.Steps);
                
                continue;
            }

            foreach (var connection in node.Edge.Connections)
            {
                if (! node.History.Contains(connection.Id))
                {
                    queue.Enqueue((connection, node.Steps + connection.Length, new HashSet<int>(node.History) { connection.Id} ));
                }
            }
        }

        return lengths.Max();
    }

    private void CreateEdges()
    {
        var queue = new Queue<(int X, int Y, Edge edge)>();

        var visited = new HashSet<(int X, int Y)> { (1, 0) };

        _id++;

        _start = new Edge { Id = _id, Length = 1, X = 1, Y = 1 };
        
        _edges.Add(_start);
        
        queue.Enqueue((1, 1, _start));

        while (queue.TryDequeue(out var position))
        {
            var (x, y, edge) = position;

            if (! visited.Add((x, y)))
            {
                continue;
            }

            var count = 0;

            count += Map[x - 1, y] == '#' ? 1 : 0;
            count += Map[x + 1, y] == '#' ? 1 : 0;
            count += Map[x, y - 1] == '#' ? 1 : 0;
            count += Map[x, y + 1] == '#' ? 1 : 0;

            while (count > 1)
            {
                var dX = 0;
                var dY = 0;
                
                if (Map[x + 1, y] != '#' && ! visited.Contains((x + 1, y)))
                {
                    dX = 1;
                    dY = 0;
                }
                
                if (Map[x - 1, y] != '#' && ! visited.Contains((x - 1, y)))
                {
                    dX = -1;
                    dY = 0;
                }
                
                if (Map[x, y + 1] != '#' && ! visited.Contains((x, y + 1)))
                {
                    dX = 0;
                    dY = 1;
                }
                
                if (Map[x, y - 1] != '#' && ! visited.Contains((x, y - 1)))
                {
                    dX = 0;
                    dY = -1;
                }

                if (dX == 0 && dY == 0)
                {
                    break;
                }
                
                x += dX;
                y += dY;

                if (x == Width - 2 && y == Height - 1)
                {
                    break;
                }

                visited.Add((x, y));

                count = 0;

                count += Map[x - 1, y] == '#' ? 1 : 0;
                count += Map[x + 1, y] == '#' ? 1 : 0;
                count += Map[x, y - 1] == '#' ? 1 : 0;
                count += Map[x, y + 1] == '#' ? 1 : 0;

                edge.Length++;
            }

            if (x == Width - 2 && y == Height - 1)
            {
                continue;
            }
            
            if (Map[x + 1, y] != '#' && ! visited.Contains((x + 1, y)))
            {
                queue.Enqueue((x + 1, y, NewEdge(edge, x, y)));
            }
                
            if (Map[x - 1, y] != '#' && ! visited.Contains((x - 1, y)))
            {
                queue.Enqueue((x - 1, y, NewEdge(edge, x, y)));
            }
                
            if (Map[x, y + 1] != '#' && ! visited.Contains((x, y + 1)))
            {
                queue.Enqueue((x, y + 1, NewEdge(edge, x, y)));
            }
                
            if (Map[x, y - 1] != '#' && ! visited.Contains((x, y - 1)))
            {
                queue.Enqueue((x, y - 1, NewEdge(edge, x, y)));
            }
        }
    }

    private Edge NewEdge(Edge edge, int x, int y)
    {
        _id++;
                
        var newEdge = new Edge { Id = _id, X = x, Y = y };

        _edges.Add(newEdge);
        
        edge.Connections.Add(newEdge);

        return newEdge;
    }
}