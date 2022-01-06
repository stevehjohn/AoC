﻿#define DUMP
using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._15;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        GetMap();

#if DEBUG && DUMP
        Console.Clear();

        Console.CursorVisible = false;

        Console.ForegroundColor = ConsoleColor.DarkGreen;

        Dump();
#endif

        var shortestPath = FindShortestRoute();

#if DEBUG && DUMP
        Console.ForegroundColor = ConsoleColor.Green;
#endif

        return shortestPath.ToString();
    }

    private int FindShortestRoute()
    {
        var bots = new List<Bot>
                   {
                       new(new Point(Origin), new Point(Destination), Map)
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
            Dump(bots);
#endif
        }
    }

#if DEBUG && DUMP
    private void Dump(List<Bot> bots = null)
    {
        Console.CursorTop = 1;

        Console.CursorLeft = 0;

        for (var y = 0; y < Width; y++)
        {
            Console.Write(' ');

            for (var x = 0; x < Height; x++)
            {
                if (bots != null)
                {
                    var found = false;

                    foreach (var bot in bots)
                    {
                        if (x == bot.Position.X && y == bot.Position.Y)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.Write('█');

                            Console.ForegroundColor = ConsoleColor.DarkGreen;

                            found = true;

                            break;
                        }
                    }

                    if (found)
                    {
                        continue;
                    }
                }

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