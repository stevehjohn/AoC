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
    }

    public int FindShortestPath()
    {
        var graph = new Graph();

        graph.Build(_distances);

        var result = graph.Solve();

        return result;
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