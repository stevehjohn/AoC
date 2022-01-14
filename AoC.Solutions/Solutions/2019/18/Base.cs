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

    private char _target = '\0';

    private readonly List<Node> _nodes = new();

    private readonly Dictionary<string, int> _distances = new();

    protected void InterrogateMap()
    {
#if DUMP && DEBUG
        Visualiser.DumpMap(_map);
#endif
        var bots = new List<Bot>();

        foreach (var node in _nodes)
        {
            bots.Add(new Bot(node.Position, _map, _distances));
        }

        while (bots.Count > 0)
        {
#if DUMP && DEBUG
            Visualiser.DumpBots(bots.Select(b => b.Position).ToList(), _map);
#endif
            var newBots = new List<Bot>();

            foreach (var bot in bots)
            {
                newBots.AddRange(bot.Move());
            }

            bots = newBots;
        }

#if DUMP && DEBUG
        Visualiser.DumpBots(bots.Select(b => b.Position).ToList(), _map);
#endif
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

                    _nodes.Add(new Node(c, new Point(x, y)));
                }

                if (c > _target)
                {
                    _target = c;
                }

                if (char.IsLetter(c))
                {
                    _nodes.Add(new Node(c, new Point(x, y)));
                }
            }
        }
    }
}