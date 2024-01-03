namespace AoC.Solutions.Solutions._2019._18;

public class Graph
{
    public Dictionary<char, Node> Nodes { get; private set; }

    public Dictionary<string, HashSet<char>> Doors { get; private set; }

    private Dictionary<string, int> _distances;

    public void Build(Dictionary<string, int> distances, Dictionary<string, HashSet<char>> doors)
    {
        Nodes = new Dictionary<char, Node>();

        _distances = distances;

        Doors = doors;

        foreach (var c in _distances.SelectMany(d => new [] { d.Key[0], d.Key[1] }).Where(c => char.IsLower(c) || char.IsNumber(c) || c == '@').Distinct())
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