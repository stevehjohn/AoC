#define DUMP
using System.Collections;
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

    private char _target = '\0';

    private readonly Dictionary<string, int> _distances = new();

    private readonly Dictionary<string, List<Point>> _paths = new();

    private readonly Dictionary<char, Point> _itemLocations = new();

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
        var destination = new Destination(_target);

        FindPaths('@', destination);
    }

    private void FindPaths(char position, Destination destination)
    {
        var requiredKeys = FindRequiredKeys(position, destination.Name);

        var availableKeys = FindAvailableKeys(position, requiredKeys.Select(k => (char) (k + 32)).ToList());

        foreach (var availableKey in availableKeys)
        {
            var keyDestination = new Destination(availableKey);

            destination.Requires.Add(keyDestination);
        }

        //foreach (var key in keys)
        //{
        //    var blocker = new Destination(key);

        //}

        // Find order keys are required in.
    }

    private List<char> FindAvailableKeys(char position, List<char> required)
    {
        var keys = new List<char>();

        foreach (var key in required)
        {
            if (FindRequiredKeys(position, key).Count == 0)
            {
                keys.Add(key);
            }
        }

        return keys;
    }

    private List<char> FindRequiredKeys(char position, char target)
    {
        var pathKey = new string(new[] { position, target }.OrderBy(x => x).ToArray());

        var path = _paths[pathKey];

        var keys = new List<char>();

        foreach (var itemLocation in _itemLocations)
        {
            if (itemLocation.Key < 'a')
            {
                if (path.Contains(itemLocation.Value))
                {
                    keys.Add(itemLocation.Key);
                }
            }
        }

#if DUMP && DEBUG
        Visualiser.VisualiseBlockers(path, keys, _itemLocations);
#endif

        return keys;
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

                if (c > _target)
                {
                    _target = c;
                }

                if (char.IsLetter(c))
                {
                    _itemLocations.Add(c, new Point(x, y));
                }
            }
        }
    }
}