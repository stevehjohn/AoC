namespace AoC.Solutions.Solutions._2019._18;

public class Graph
{
    public Dictionary<char, Node> Nodes { get; private set; }

    public Dictionary<string, string> Doors { get; private set; }

    private Dictionary<string, int> _distances;

    public void Build(Dictionary<string, int> distances, Dictionary<string, string> doors)
    {
        Nodes = new Dictionary<char, Node>();

        _distances = distances;

        Doors = doors;

        Nodes.Add('@', new Node('@'));

        foreach (var c in _distances.Select(d => d.Key[1]).Where(char.IsLower).Distinct())
        {
            Nodes.Add(c, new Node(c));
        }

        foreach (var (parentKey, node) in Nodes)
        {
            var connections = _distances.Where(d => d.Key.Contains(parentKey));

            foreach (var (childKey, distance) in connections)
            {
                var child = childKey.Replace(parentKey.ToString(), string.Empty)[0];

                if (char.IsUpper(child))
                {
                    continue;
                }

                node.Children.Add(Nodes[child], distance);
            }
        }
    }
}