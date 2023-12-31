using AoC.Solutions.Common;
using JetBrains.Annotations;
using System.Text;

namespace AoC.Solutions.Solutions._2019._15;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        GetMap();

#if DEBUG && DUMP
        Dump();
#endif

        var shortestPath = FindShortestRoute();

#if DEBUG && DUMP
        Console.ForegroundColor = ConsoleColor.Green;

        Console.CursorLeft = 0;

        Console.CursorTop = Height + 3;
#endif

        SaveResult();


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
            DrawBots(bots);

            Thread.Sleep(50);
#endif
        }
    }

    private void SaveResult()
    {
        var data = new StringBuilder();
        
        data.AppendLine($"{Origin.X},{Origin.Y}");

        data.AppendLine($"{Destination.X},{Destination.Y}");

        for (var y = 0; y < Width; y++)
        {
            for (var x = 0; x < Height; x++)
            {
                data.Append(Map[x, y] ? '1' : '0');
            }

            data.AppendLine();
        }

        File.WriteAllText(Part1ResultFile, data.ToString());
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

    private void Dump()
    {
        Console.CursorTop = 1;

        Console.CursorLeft = 0;

        Console.ForegroundColor = ConsoleColor.DarkGreen;

        for (var y = 0; y < Width; y++)
        {
            Console.Write(' ');

            for (var x = 0; x < Height; x++)
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