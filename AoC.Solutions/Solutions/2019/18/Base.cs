#define DUMP
using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._18;

public abstract class Base : Solution
{
    public override string Description => "Maze and keys";

    private char[,] _map;

    private int _width;

    private int _height;

    private Point _start;

    private readonly Dictionary<string, int> _distances = new();

    private readonly Dictionary<string, List<Point>> _paths = new();

    private readonly Dictionary<char, Point> _itemLocations = new();

    private readonly List<string> _routes = new();

    protected void InterrogateMap()
    {
#if DUMP && DEBUG
        Visualiser.DumpMap(_map);
#endif
        var bots = new List<Bot>
                   {
                       new('@', _start, _map, _distances, _paths)
                   };

        foreach (var node in _itemLocations)
        {
            bots.Add(new Bot(node.Key, node.Value, _map, _distances, _paths));
        }

        while (bots.Count > 0)
        {
#if DUMP && DEBUG
            //Visualiser.DumpBots(bots);
#endif
            var newBots = new List<Bot>();

            foreach (var bot in bots)
            {
                newBots.AddRange(bot.Move());
            }

            bots = newBots;
        }

#if DUMP && DEBUG
        Visualiser.DumpBots(bots);
#endif

        GenerateRoutes(new List<char>());
    }

    private void GenerateRoutes(List<char> collected)
    {
        FindAvailableKeys('@', collected);
    }

    private List<char> FindAvailableKeys(char position, List<char> collected)
    {
        var keys = new List<char>();

        // TODO: This could really be optimised with a bit of pre-calculation.
        foreach (var key in _itemLocations.Select(i => i.Key).Where(i => i >= 'a').Except(collected)) // Hopefully IEnumerable of all uncollected keys.
        {
            if (IsAccessible(position, key, collected))
            {
                keys.Add(key);
            }
        }

        return keys;
    }

    private bool IsAccessible(char position, char target, List<char> collected)
    {
        var pathKey = new string(new[] { position, target }.OrderBy(x => x).ToArray());

        var path = _paths[pathKey];

        foreach (var itemLocation in _itemLocations)
        {
            if (itemLocation.Key < 'a' && ! collected.Contains(char.ToLower(itemLocation.Key)))
            {
                if (path.Contains(itemLocation.Value))
                {
                    return false;
                }
            }
        }

        return true;
    }

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        _map = new char[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                var c = Input[y][x];

                _map[x, y] = c;

                if (c == '@')
                {
                    _start = new Point(x, y);
                }

                if (char.IsLetter(c))
                {
                    _itemLocations.Add(c, new Point(x, y));
                }
            }
        }
    }
}