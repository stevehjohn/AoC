using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._15;

public abstract class Base : Solution
{
    public override string Description => "Risky business";

    private int _width;

    private int _height;

    private Node _rootNode;

    protected void ParseInput(int scalingFactor = 1)
    {
        _width = Input[0].Length * scalingFactor;

        _height = Input.Length * scalingFactor;

        var nodes = GetNodes(scalingFactor);

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if (x > 0)
                {
                    nodes[x, y].Neighbors.Add(nodes[x - 1, y]);
                }

                if (y > 0)
                {
                    nodes[x, y].Neighbors.Add(nodes[x, y - 1]);
                }

                if (x < _width - 1)
                {
                    nodes[x, y].Neighbors.Add(nodes[x + 1, y]);
                }

                if (y < _height - 1)
                {
                    nodes[x, y].Neighbors.Add(nodes[x, y + 1]);
                }
            }
        }

        _rootNode = nodes[0, 0];
    }

    private Node[,] GetNodes(int scalingFactor)
    {
        var nodes = new Node[_width, _height];

        var originalWidth = _width / scalingFactor;

        var originalHeight = _height / scalingFactor;

        for (var xScale = 0; xScale < scalingFactor; xScale++)
        {
            for (var yScale = 0; yScale < scalingFactor; yScale++)
            {
                for (var y = 0; y < originalHeight; y++)
                {
                    for (var x = 0; x < originalWidth; x++)
                    {
                        var cost = (byte) (Input[x][y] - '0' + xScale + yScale);

                        if (cost > 9)
                        {
                            cost -= 9;
                        }

                        nodes[x + xScale * originalWidth, y + yScale * originalHeight] = new Node(new Point(x + xScale * originalWidth, y + yScale * originalHeight), cost);
                    }
                }
            }
        }

        return nodes;
    }

    protected int Solve()
    {
        var queue = new PriorityQueue<Node, int>();

        queue.Enqueue(_rootNode, int.MaxValue);

        var costs = new Dictionary<int, int>
                    {
                        { 0, 0 }
                    };

#if DUMP && DEBUG
        Console.Clear();

        Console.CursorVisible = false;

        Console.ForegroundColor = ConsoleColor.DarkGray;
#endif

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

#if DUMP && DEBUG
            Console.CursorLeft = node.Position.X;

            Console.CursorTop = node.Position.Y;

            Console.Write('█');
#endif

            if (node.Position.X == _width - 1 && node.Position.Y == _height - 1)
            {
                break;
            }

            foreach (var neighbor in node.Neighbors)
            {
                var cost = costs[node.Position.X + node.Position.Y * _width] + neighbor.Value;

                if (! costs.TryGetValue(neighbor.Position.X + neighbor.Position.Y * _width, out var nextCost) || cost < nextCost)
                {
                    costs[neighbor.Position.X + neighbor.Position.Y * _width] = cost;

                    queue.Enqueue(neighbor, cost);
                }
            }

#if DUMP && DEBUG
            DrawPath(costs);
#endif
        }

#if DUMP && DEBUG
        DrawPath(costs, true);

        Console.CursorLeft = 0;

        Console.CursorTop = _height + 2;

        Console.ForegroundColor = ConsoleColor.Green;
#endif

        return costs[_width - 1 + (_height - 1) * _width];
    }

#if DUMP && DEBUG
    private void DrawPath(Dictionary<int, int> costs, bool final = false)
    {
        var target = costs.Where(c => c.Key % _width == c.Key / _width).OrderByDescending(c => c.Key).First().Key;

        var position = new Point(target / _width, target / _width);

        var temp = Console.ForegroundColor;

        Console.ForegroundColor = final ? ConsoleColor.Green : ConsoleColor.DarkCyan;

        while (position.X != 0 || position.Y != 0)
        {
            Console.CursorLeft = position.Y;

            Console.CursorTop = position.X;

            Console.Write('█');

            var lowest = int.MaxValue;

            var lowestPosition = new Point();

            for (var x = position.X - 1; x < position.X + 2; x += 2)
            {
                if (x < 0 || x >= _width)
                {
                    continue;
                }

                if (! costs.ContainsKey(x + position.Y * _width))
                {
                    continue;
                }

                if (costs[x + position.Y * _width] < lowest)
                {
                    lowestPosition.X = x;

                    lowestPosition.Y = position.Y;

                    lowest = costs[x + position.Y * _width];
                }
            }

            for (var y = position.Y - 1; y < position.Y + 2; y += 2)
            {
                if (y < 0 || y >= _width)
                {
                    continue;
                }

                if (!costs.ContainsKey(position.X + y * _width))
                {
                    continue;
                }

                if (costs[position.X + y * _width] < lowest)
                {
                    lowestPosition.X = position.X;

                    lowestPosition.Y = y;

                    lowest = costs[position.X + y * _width];
                }
            }

            position = lowestPosition;
        }

        Console.CursorLeft = 0;

        Console.CursorTop = 0;

        Console.Write('█');

        Console.ForegroundColor = temp;
    }
#endif
}