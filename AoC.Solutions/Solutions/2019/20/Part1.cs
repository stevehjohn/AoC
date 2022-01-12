#define DUMP
using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2019._20;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

#if DEBUG && DUMP
        Console.CursorVisible = false;

        DumpMap();
#endif

        var result = FindShortestRoute();

        return result.ToString();
    }

    
    private int FindShortestRoute()
    {
        var bots = new List<Bot>
                   {
                       new(new Point(Start), new Point(End), Maze)
                   };

        while (true)
        {
            var newBots = new List<Bot>();

            foreach (var bot in bots)
            {
                if (bot.IsHome)
                {
                    return bot.Steps;
                }

                newBots.AddRange(bot.Move());
            }

            bots = newBots;

#if DEBUG && DUMP
            Thread.Sleep(100);

            DrawBots(bots);

            Console.CursorLeft = 0;

            Console.CursorTop = Height + 2;

            Console.ForegroundColor = ConsoleColor.Green;
#endif
        }
    }

#if DEBUG && DUMP
    private List<Point> _previousPositions = new();

    private void DrawBots(List<Bot> bots)
    {
        foreach (var bot in bots)
        {
            Console.CursorLeft = 1 + bot.Position.X;

            Console.CursorTop = 1 + bot.Position.Y;

            Console.ForegroundColor = ConsoleColor.Red;

            Console.Write('█');

            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }

        foreach (var position in _previousPositions)
        {
            Console.CursorLeft = 1 + position.X;

            Console.CursorTop = 1 + position.Y;

            Console.Write(' ');
        }

        _previousPositions = bots.Select(b => new Point(b.Position)).ToList();
    }

    private void DumpMap()
    {
        Console.Clear();

        Console.CursorLeft = 0;

        Console.CursorTop = 1;

        for (var y = 0; y < Height; y++)
        {
            Console.Write(' ');

            for (var x = 0; x < Width; x++)
            {
                if (Maze[x, y] is -2 or 0)
                {
                    Console.Write(' ');

                    continue;
                }

                if (Maze[x, y] == -1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.Write('█');

                    continue;
                }

                if (Maze[x, y] > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;

                    Console.Write('█');

                    continue;
                }
            }

            Console.WriteLine();
        }
    }
#endif
}