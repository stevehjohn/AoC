using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._15;

[UsedImplicitly]
public class Part1 : Solution
{
    public override string Description => "Risky business";

    private int _width;
    
    private int _height;

    private Node _rootNode;

    private int _minimumCost = int.MaxValue;

    public override string GetAnswer()
    {
        _rootNode = ParseInput();

        Solve();

        return "TEST";
    }

    private Node ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        var nodes = new Node[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                nodes[x, y] = new Node(new Point(x, y), (byte) (Input[y][x] - '0'));
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

        return nodes[0, 0];
    }

    private void Solve()
    {
        var queue = new PriorityQueue<Node>();

        queue.Push(int.MaxValue, _rootNode);

        var cost = 0;

        while (! queue.IsEmpty)
        {
            var node = queue.Pop();

            if (node.Position.X == _width - 1 && node.Position.Y == _height - 1)
            {
                break;
            }

            foreach (var neighbor in node.Neighbors)
            {
                if (cost + neighbor.Value < _minimumCost)
                {
                    queue.Push(int.MaxValue, neighbor);
                }
            }
        }
    }
}