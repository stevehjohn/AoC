using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._23;

[UsedImplicitly]
public class Part2 : Base
{
    private Edge _start;

    private int _id = 1;
    
    public override string GetAnswer()
    {
        ParseInput();
        
        CreateEdges();

        var result = 0;

        return result.ToString();
    }

    private void CreateEdges()
    {
        var queue = new Queue<(int X, int Y, Edge edge)>();

        var visited = new HashSet<(int X, int Y)> { (1, 0) };

        _start = new Edge { Id = _id, Length = 1 };
        
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
            
            Console.WriteLine($"{x}, {y}: {edge.Length}");
            
            if (Map[x + 1, y] != '#' && ! visited.Contains((x + 1, y)))
            {
                var newEdge = new Edge { Id = _id };
                
                edge.Connections.Add(edge);
                
                queue.Enqueue((x + 1, y, newEdge));
            }
                
            if (Map[x - 1, y] != '#' && ! visited.Contains((x - 1, y)))
            {
                var newEdge = new Edge { Id = _id };
                
                edge.Connections.Add(edge);
                
                queue.Enqueue((x - 1, y, newEdge));
            }
                
            if (Map[x, y + 1] != '#' && ! visited.Contains((x, y + 1)))
            {
                var newEdge = new Edge { Id = _id };
                
                edge.Connections.Add(edge);
                
                queue.Enqueue((x, y + 1, newEdge));
            }
                
            if (Map[x, y - 1] != '#' && ! visited.Contains((x, y - 1)))
            {
                var newEdge = new Edge { Id = _id };
                
                edge.Connections.Add(edge);
                
                queue.Enqueue((x, y - 1, newEdge));
            }
        }
    }
}