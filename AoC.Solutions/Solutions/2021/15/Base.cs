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

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

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
        }

        return costs[_width - 1 + (_height - 1) * _width];
    }
}