using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._18;

public abstract class Base : Solution
{
    public override string Description => "Maze and keys";

    protected char[,] Map;

    private int _width;

    private int _height;

    protected readonly Dictionary<string, int> Distances = new();

    protected readonly Dictionary<string, List<Point>> Paths = new();

    protected readonly Dictionary<string, HashSet<char>> Doors = new();

    protected readonly Dictionary<char, Point> ItemLocations = new();

    protected void InterrogateMap()
    {
        var bots = new List<Bot>();

        // TODO: Don't really like this. Should probably pass around a pointer to AllHistory.
        Bot.ResetStaticHistory();

        foreach (var start in ItemLocations.Where(l => char.IsNumber(l.Key) || l.Key == '@'))
        {
            bots.Add(new Bot(start.Key, start.Value, Map, Distances, Paths, Doors));
        }

        foreach (var node in ItemLocations)
        {
            bots.Add(new Bot(node.Key, node.Value, Map, Distances, Paths, Doors));
        }

        while (bots.Count > 0)
        {
            var newBots = new List<Bot>();

            foreach (var bot in bots)
            {
                newBots.AddRange(bot.Move());
            }

            bots = newBots;
        }
    }

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        Map = new char[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                Map[x, y] = Input[y][x];
            }
        }
    }

    protected void FindItemLocations()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                var c = Map[x, y];

                if (char.IsLetter(c) || char.IsNumber(c) || c == '@')
                {
                    ItemLocations.Add(c, new Point(x, y));
                }
            }
        }
    }

    private readonly record struct Route(
        int Target,
        uint Key,
        uint Required,
        int Distance);

    protected int SolveFast()
    {
        var width = Map.GetLength(0);

        var height = Map.GetLength(1);

        var size = width * height;

        var cells = new char[size];

        var nodes = new List<int>();

        var starts = new List<int>();

        var nodeAt = new int[size];

        Array.Fill(nodeAt, -1);

        var allKeys = 0U;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var position = y * width + x;

                var c = Map[x, y];

                cells[position] = c;

                switch (c)
                {
                    case '@' or >= '1' and <= '4':
                        starts.Add(nodes.Count);
                        break;
                    case >= 'a' and <= 'z':
                        allKeys |= 1U << (c - 'a');
                        break;
                    default:
                        continue;
                }

                nodeAt[position] = nodes.Count;

                nodes.Add(position);
            }
        }

        var routes = new List<Route>[nodes.Count];

        var labels = new List<uint>[size];

        var queue =
            new Queue<(int Position, uint Required, int Distance)>();

        for (var source = 0; source < nodes.Count; source++)
        {
            routes[source] = [];

            foreach (var label in labels)
            {
                label?.Clear();
            }

            var origin = nodes[source];

            labels[origin] ??= [];

            labels[origin].Add(0);

            queue.Enqueue((origin, 0, 0));

            while (queue.TryDequeue(out var item))
            {
                var position = item.Position;

                var required = item.Required;

                var c = cells[position];

                if (position != origin && c is >= 'a' and <= 'z')
                {
                    var key = 1U << (c - 'a');

                    if ((required & key) == 0)
                    {
                        routes[source].Add(new Route(
                            nodeAt[position], key,
                            required, item.Distance));
                    }

                    required |= key;
                }

                var x = position % width;

                var y = position / width;

                if (x > 0)
                {
                    Visit(position - 1, required, item.Distance + 1);
                }

                if (x < width - 1)
                {
                    Visit(position + 1, required, item.Distance + 1);
                }

                if (y > 0)
                {
                    Visit(position - width, required, item.Distance + 1);
                }

                if (y < height - 1)
                {
                    Visit(position + width, required, item.Distance + 1);
                }
            }
        }

        var initial = 0UL;

        for (var i = 0; i < starts.Count; i++)
        {
            initial |= (ulong) starts[i] << (26 + i * 5);
        }

        var cache = new Dictionary<ulong, int>();

        var answer = Search(initial);

        if (answer == int.MaxValue)
        {
            throw new InvalidOperationException("No route found.");
        }

        return answer;

        void Visit(int position, uint required, int distance)
        {
            var c = cells[position];

            switch (c)
            {
                case '#':
                    return;
                
                case >= 'A' and <= 'Z':
                    required |= 1U << (c - 'A');
                    
                    break;
            }

            var seen = labels[position] ??= [];

            foreach (var previous in seen)
            {
                if ((previous & required) == previous)
                {
                    return;
                }
            }

            seen.Add(required);

            queue.Enqueue((position, required, distance));
        }

        int Search(ulong state)
        {
            var keys = (uint) (state & 0x3FFFFFF);

            if (keys == allKeys)
            {
                return 0;
            }

            if (cache.TryGetValue(state, out var cached))
            {
                return cached;
            }

            var best = int.MaxValue;

            for (var robot = 0; robot < starts.Count; robot++)
            {
                var shift = 26 + robot * 5;

                var position = (int) ((state >> shift) & 31);

                foreach (var route in routes[position])
                {
                    if ((keys & route.Key) != 0 ||
                        (keys & route.Required) != route.Required)
                    {
                        continue;
                    }

                    var next = state & ~(31UL << shift);

                    next |= (ulong) route.Target << shift;

                    next |= route.Key;

                    var remaining = Search(next);

                    if (remaining != int.MaxValue)
                    {
                        best = Math.Min(best, route.Distance + remaining);
                    }
                }
            }

            cache[state] = best;

            return best;
        }
    }
}