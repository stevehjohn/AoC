using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._15;

public abstract class Base : Solution
{
    private int _width;

    private int _height;

    private Node _rootNode;

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        var nodes = new Node[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                nodes[x, y] = new Node(new Point(x, y), (byte)(Input[y][x] - '0'));
            }
        }

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

    protected int Solve()
    {
        var queue = new PriorityQueue<Node>();

        queue.Push(int.MaxValue, _rootNode);

        var costs = new Dictionary<int, int>
                    {
                        { 0, 0 }
                    };

        while (!queue.IsEmpty)
        {
            var node = queue.Pop();

            if (node.Position.X == _width - 1 && node.Position.Y == _height - 1)
            {
                break;
            }

            foreach (var neighbor in node.Neighbors)
            {
                var cost = costs[node.Position.X + node.Position.Y * _width] + neighbor.Value;

                if (!costs.TryGetValue(neighbor.Position.X + neighbor.Position.Y * _width, out var nextCost) || cost < nextCost)
                {
                    costs[neighbor.Position.X + neighbor.Position.Y * _width] = cost;

                    queue.Push(cost, neighbor);
                }
            }
        }

        return costs[_width - 1 + (_height - 1) * _width];
    }
}