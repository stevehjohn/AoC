using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2017._07;

public abstract class Base : Solution
{
    public override string Description => "Balancing act";

    private readonly Dictionary<string, Node> _nodes = new();

    protected Node RootNode;

    protected void ParseInput()
    {
        GetNodes();

        BuildTree();
    }

    private void BuildTree()
    {
        var linesWithChildren = Input.Where(l => l.Contains("->"));

        foreach (var line in linesWithChildren)
        {
            var parts = line.Split(" -> ", StringSplitOptions.TrimEntries);

            var name = parts[0][..parts[0].IndexOf(' ')];

            var node = _nodes[name];

            var children = parts[1].Split(',', StringSplitOptions.TrimEntries);

            foreach (var child in children)
            {
                var childNode = _nodes[child];

                childNode.Parent = node;

                node.Children.Add(childNode);
            }
        }

        RootNode = _nodes.Single(n => n.Value.Parent == null).Value;
    }

    private void GetNodes()
    {
        foreach (var line in Input)
        {
            var left = line.Split(" -> ", StringSplitOptions.TrimEntries)[0];

            var parts = left.Split(' ', StringSplitOptions.TrimEntries);

            var node = new Node(parts[0], int.Parse(parts[1][1..^1]));

            _nodes.Add(node.Name, node);
        }
    }
}