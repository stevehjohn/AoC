using AoC.Solutions.Extensions;

namespace AoC.Solutions.Solutions._2024._16;

public class Graph
{
    private Edge _start;

    private readonly Dictionary<Point, Edge> _edges = [];

    public Graph(char[,] map)
    {
        ParseMap(map);
    }

    public int WalkToEnd(Func<Point, Vertex, int> heuristic)
    {
        var queue = new PriorityQueue<Node, int>();
        
        queue.Enqueue(new Node(_start, Point.East, 0, null), 0);

        var scores = new int[_edges.Count << 4];
        
        Array.Fill(scores, int.MaxValue);

        var unique = new HashSet<Point>();

        var bestScore = int.MaxValue;
        
        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            var key = (node.Edge.Id << 4) | ((node.Direction.Y + 1) << 2) | (node.Direction.X + 1);
            
            if (scores[key] < node.Score)
            {
                continue;
            }

            scores[key] = node.Score;

            if (node.Edge.MetaData == "End")
            {
                bestScore = Math.Min(bestScore, node.Score);

                if (node.Score > bestScore)
                {
                    return unique.Count;
                }
                
                // Note: this is only the corners, hence low score.
                
                var walker = node;

                var position = node.Edge.Position;
                
                unique.Add(position);

                while (walker != null)
                {
                    while (position != walker.Edge.Position)
                    {
                        position.StepTowards(walker.Edge.Position);

                        unique.Add(position);
                    }
                    
                    walker = walker.Previous;
                }
            }

            for (var i = 0; i < node.Edge.Vertices.Count; i++)
            {
                var vertex = node.Edge.Vertices[i];

                var newScore = node.Score + heuristic(node.Direction, vertex);

                key = (vertex.Edge.Id << 4) | ((vertex.Direction.Y + 1) << 2) | (vertex.Direction.X + 1);

                var penalty = scores[key];

                if (penalty != int.MaxValue)
                {
                    newScore += penalty;
                }

                if (scores[key] > newScore)
                {
                    queue.Enqueue(new Node(vertex.Edge, vertex.Direction, newScore, node), newScore);
                }
            }
        }

        return 0;
    }

    private void ParseMap(char[,] map)
    {
        var start = Point.Null;

        var end = Point.Null;
        
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

        _start = new Edge(0, start, "Start");
        
        _edges.Add(start, _start);

        FindEdges(map, _start, start, end);
    }

    private void FindEdges(char[,] map, Edge startEdge, Point start, Point end)
    {
        var queue = new Queue<(Edge Edge, Point Position, Point Direction)>();
        
        queue.Enqueue((startEdge, start, Point.North));
        queue.Enqueue((startEdge, start, Point.East));
        queue.Enqueue((startEdge, start, Point.South));
        queue.Enqueue((startEdge, start, Point.West));

        var visited = new HashSet<(Point, Point)>
        {
            (start, Point.North),
            (start, Point.South),
            (start, Point.East),
            (start, Point.West)
        };

        var id = 1;

        while (queue.Count > 0)
        {
            var (edge, position, direction) = queue.Dequeue();

            position += direction;

            if (map[position.X, position.Y] != '.')
            {
                continue;
            }

            if (! visited.Add((position, direction)))
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

                visited.Add((position, direction));

                steps++;
            }

            if (! _edges.TryGetValue(position, out var next))
            {
                next = new Edge(id++, position, position.Equals(end) ? "End" : string.Empty);
                
                _edges.Add(position, next);
            }
            
            edge.AddHeading(new Vertex(direction, steps, next));
            
            queue.Enqueue((next, position, Point.North));
            queue.Enqueue((next, position, Point.East));
            queue.Enqueue((next, position, Point.South));
            queue.Enqueue((next, position, Point.West));
        }
    }
}