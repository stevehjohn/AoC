﻿using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._18;

public abstract class Base : Solution
{
    public override string Description => "Maze and keys";

    protected char[,] Map;

    private int _width;

    private int _height;

    protected readonly List<Point> Starts = new();

    protected readonly Dictionary<string, int> Distances = new();

    protected readonly Dictionary<string, List<Point>> Paths = new();

    protected readonly Dictionary<string, string> Doors = new();

    protected readonly Dictionary<char, Point> ItemLocations = new();

    protected void InterrogateMap()
    {
#if DUMP && DEBUG
        Visualiser.DumpMap(Map);
#endif
        FindStarts();

        var bots = new List<Bot>();

        foreach (var start in Starts)
        {
            bots.Add(new Bot('@', start, Map, Distances, Paths, Doors));
        }

        foreach (var node in ItemLocations)
        {
            bots.Add(new Bot(node.Key, node.Value, Map, Distances, Paths, Doors));
        }

        while (bots.Count > 0)
        {
#if DUMP && DEBUG
            Visualiser.DumpBots(bots);
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

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        Map = new char[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                var c = Input[y][x];

                Map[x, y] = c;

                if (char.IsLetter(c))
                {
                    ItemLocations.Add(c, new Point(x, y));
                }
            }
        }
    }

    private void FindStarts()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if (Map[x, y] == '@' || char.IsNumber(Map[x, y]))
                {
                    Starts.Add(new Point(x, y));
                }
            }
        }
    }
}