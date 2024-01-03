using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._15;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        LoadMap();

#if DEBUG && DUMP
        Console.ForegroundColor = ConsoleColor.DarkGreen;

        Dump();
#endif

        var result = SpreadGas();

#if DEBUG && DUMP
        Console.ForegroundColor = ConsoleColor.Green;

        Console.CursorLeft = 0;

        Console.CursorTop = Height + 4;
#endif

        return result.ToString();
    }

    private void LoadMap()
    {
        var data = File.ReadAllLines(Part1ResultFile);

        var coords = data[0].Split(',', StringSplitOptions.TrimEntries);

        Origin = new Point(int.Parse(coords[0]), int.Parse(coords[1]));

        coords = data[1].Split(',', StringSplitOptions.TrimEntries);

        Destination = new Point(int.Parse(coords[0]), int.Parse(coords[1]));

        Map = new bool[data[2].Length, data.Length];

        for (var y = 2; y < data.Length; y++)
        {
            var line = data[y];

            for (var x = 0; x < line.Length; x++)
            {
                Map[x, y - 2] = line[x] == '1';
            }
        }

        Width = data[1].Length;

        Height = data.Length - 2;
    }

    private int SpreadGas()
    {
        var bots = new List<Bot>
                   {
                       new(new Point(Destination), null, Map)
                   };

        var max = 0;

        while (true)
        {
            var newBots = new List<Bot>();

            foreach (var bot in bots)
            {
                var moveResult = bot.Move();

                max = Math.Max(max, bot.Steps);

                if (moveResult == null)
                {
                    continue;
                }

                newBots.AddRange(moveResult);
            }

            if (newBots.Count == 0)
            {
                return max;
            }

            bots = newBots;

#if DEBUG && DUMP
            DrawBots(bots);

            Thread.Sleep(25);
#endif
        }
    }

#if DEBUG && DUMP
    private static void DrawBots(List<Bot> bots)
    {
        foreach (var bot in bots)
        {
            Console.CursorLeft = 1 + bot.Position.X;

            Console.CursorTop = 1 + bot.Position.Y;

            Console.ForegroundColor = ConsoleColor.Red;

            Console.Write('█');

            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
    }

    private void Dump()
    {
        Console.CursorTop = 1;

        Console.CursorLeft = 0;

        for (var y = 0; y < Height; y++)
        {
            Console.Write(' ');

            for (var x = 0; x < Width; x++)
            {
                if (x == Origin.X && y == Origin.Y)
                {
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.Write('S');

                    Console.ForegroundColor = ConsoleColor.DarkGreen;

                    continue;
                }

                if (x == Destination.X && y == Destination.Y)
                {
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.Write('O');

                    Console.ForegroundColor = ConsoleColor.DarkGreen;

                    continue;
                }

                Console.Write(Map[x, y] ? ' ' : '█');
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
#endif
}