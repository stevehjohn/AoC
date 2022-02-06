using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._12;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly Dictionary<int, Node> _nodes = new();

    public override string GetAnswer()
    {
        ParseInput();

        return "TESTING";
    }

    private void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split(" <-> ", StringSplitOptions.TrimEntries);

            var node = new Node(int.Parse(parts[0]));

            _nodes.Add(node.Id, node);

            var connectionsString = parts[1].Split(',', StringSplitOptions.TrimEntries).Select(int.Parse);

            foreach (var connection in connectionsString)
            {
                if (_nodes.ContainsKey(connection))
                {
                    node.Connections.Add(_nodes[connection]);
                }
                else
                {
                    var connectionNode = new Node(connection);

                    node.Connections.Add(connectionNode);

                    _nodes.Add(connectionNode.Id, connectionNode);
                }
            }
        }
    }
}