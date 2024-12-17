using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Extensions;

namespace AoC.Solutions.Solutions._2024._16;

public class Graph
{
    private Edge _start;

    public Graph(char[,] map)
    {
        ParseMap(map);
    }

    private void ParseMap(char[,] map)
    {
        Point start = null;

        Point end = null;
        
        map.ForAll((x, y, c) =>
        {
            switch (c)
            {
                case 'S':
                    map[x, y] = '.';
                    start = new Point(x, y);
                    break;
                case 'E':
                    map[x, y] = '.';
                    end = new Point(x, y);
                    break;
            }
        });

        _start = new Edge(0, "Start");

        FindEdges(map, _start, start, end);
    }

    private static void FindEdges(char[,] map, Edge startEdge, Point start, Point end)
    {
        var queue = new Queue<(Edge Edge, Point Position, Point Direction)>();
        
        queue.Enqueue((startEdge, start, new Point(0, -1)));
        queue.Enqueue((startEdge, start, new Point(1, 0)));
        queue.Enqueue((startEdge, start, new Point(0, 1)));
        queue.Enqueue((startEdge, start, new Point(-1, 0)));

        var visited = new HashSet<Point> { start };

        map[start.X, start.Y] = 'O';

        var id = 1;

        while (queue.Count > 0)
        {
            var (edge, position, direction) = queue.Dequeue();

            position += direction;

            if (map[position.X, position.Y] != '.')
            {
                continue;
            }

            map[position.X, position.Y] = 'O';

            if (! visited.Add(position))
            {
                continue;
            }

            var steps = 1;

            var left = new Point(direction.Y, -direction.X);

            var right = new Point(-direction.Y, direction.X);

            while (map[position.X + left.X, position.Y + left.Y] == '#' 
                   && map[position.X + right.X, position.Y + right.Y] == '#'
                   && map[position.X + direction.X, position.Y + direction.Y] != '#')
            {
                position += direction;

                visited.Add(position);

                map[position.X, position.Y] = 'O';

                steps++;
            }
            
            var newEdge = new Edge(id++, position.Equals(end) ? "End" : string.Empty);
            
            edge.AddHeading(new Vertex(GetHeading(direction), steps, newEdge));
            
            queue.Enqueue((newEdge, position, new Point(0, -1)));
            queue.Enqueue((newEdge, position, new Point(1, 0)));
            queue.Enqueue((newEdge, position, new Point(0, 1)));
            queue.Enqueue((newEdge, position, new Point(-1, 0)));
        }

        for (var y = 0; y < map.GetLength(1); y++)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                Console.Write(map[x, y]);
            }
                
            Console.WriteLine();
        }
    }

    private static Heading GetHeading(Point direction)
    {
        return (direction.X, direction.Y) switch
        {
            (0, -1) => Heading.North,
            (1, 0) => Heading.East,
            (0, 1) => Heading.South,
            (-1, 0) => Heading.West,
            _ => throw new PuzzleException($"Invalid direction ({direction}).")
        };
    }
}