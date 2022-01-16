#if DUMP && DEBUG
using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2019._18;

public static class Visualiser
{
    private static char[,] _map;

    private static int _width;

    private static int _height;

    private static List<Point> _previousBots = new();

    public static void ShowSolution(string solution, Dictionary<string, List<Point>> paths, Dictionary<char, Point> itemLocations, List<Point> starts)
    {
        foreach (var start in starts)
        {
            itemLocations.Add('@', new Point(start));
        }

        for (var i = 0; i < solution.Length - 1; i++)
        {
            var pathKey = new string(new[] { solution[i], solution[i + 1] }.OrderBy(x => x).ToArray());

            var path = paths[pathKey];

            if (! path[0].Equals(itemLocations[solution[i]]))
            {
                path.Reverse();
            }

            foreach (var point in path.Take(path.Count - 1))
            {
                Console.SetCursorPosition(point.X + 1, point.Y + 1);

                Console.ForegroundColor = ConsoleColor.Red;

                Console.Write('█');

                Thread.Sleep(1);
            }

            var on = true;

            for (var f = 0; f < 8; f++)
            {
                if (on)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;

                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Blue;

                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.SetCursorPosition(itemLocations[solution[i + 1]].X + 1, itemLocations[solution[i + 1]].Y + 1);

                Console.Write(solution[i + 1]);

                Console.SetCursorPosition(itemLocations[char.ToUpper(solution[i + 1])].X + 1, itemLocations[char.ToUpper(solution[i + 1])].Y + 1);

                Console.Write(char.ToUpper(solution[i + 1]));

                Thread.Sleep(200);

                on = ! on;
            }

            Console.BackgroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(itemLocations[solution[i + 1]].X + 1, itemLocations[solution[i + 1]].Y + 1);

            Console.Write(' ');

            Console.SetCursorPosition(itemLocations[char.ToUpper(solution[i + 1])].X + 1, itemLocations[char.ToUpper(solution[i + 1])].Y + 1);

            Console.Write(' ');

            foreach (var point in path)
            {
                Console.SetCursorPosition(point.X + 1, point.Y + 1);

                Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.Write('█');
            }

            Console.ForegroundColor = ConsoleColor.Green;
        }

        Console.SetCursorPosition(0, _height + 3);
    }

    public static void DumpBots(List<Bot> bots)
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

            var c = _map[position.X, position.Y];

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
    }

    public static void DumpMap(char[,] map)
    {
        _map = map;

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
                var c = _map[x, y];

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
#endif