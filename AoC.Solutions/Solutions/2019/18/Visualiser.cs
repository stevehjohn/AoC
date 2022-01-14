using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2019._18;

public static class Visualiser
{
    private static int _width;

    private static int _height;

    private static List<Point> _previousBots = new();

    public static void DumpBots(List<Bot> bots, char[,] map)
    {
        foreach (var bot in bots)
        {
            Console.SetCursorPosition(bot.Position.X + 1, bot.Position.Y + 1);

            Console.BackgroundColor = ConsoleColor.Red;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write(bot.Name);

            Console.BackgroundColor = ConsoleColor.Black;
        }

        foreach (var position in _previousBots)
        {
            if (bots.Any(b => b.Position.Equals(position)))
            {
                continue;
            }

            Console.SetCursorPosition(position.X + 1, position.Y + 1);

            var c = map[position.X, position.Y];

            if (char.IsLetter(c) || c == '@')
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;

                Console.ForegroundColor = ConsoleColor.White;

                Console.Write(c);

                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Write(' ');
            }
        }

        _previousBots = bots.Select(b => new Point(b.Position)).ToList();

        Console.SetCursorPosition(0, _height + 3);

        Console.ForegroundColor = ConsoleColor.Green;

        //Thread.Sleep(10);
    }

    public static void DumpMap(char[,] map)
    {
        Console.Clear();

        Console.CursorVisible = false;

        Console.SetCursorPosition(0, 1);

        _width = map.GetLength(0);

        _height = map.GetLength(1);

        for (var y = 0; y < _height; y++)
        {
            Console.Write(' ');

            for (var x = 0; x < _width; x++)
            {
                var c = map[x, y];

                if (c == '#')
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;

                    Console.Write('█');

                    continue;
                }

                if (char.IsLetter(c) || c == '@')
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;

                    Console.ForegroundColor = ConsoleColor.White;

                    Console.Write(c);

                    Console.BackgroundColor = ConsoleColor.Black;

                    continue;
                }

                Console.Write(' ');
            }

            Console.WriteLine();
        }
    }
}