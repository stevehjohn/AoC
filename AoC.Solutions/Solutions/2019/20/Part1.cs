#define DUMP
namespace AoC.Solutions.Solutions._2019._20;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

#if DEBUG && DUMP
        DumpMap();

        Console.CursorLeft = 0;

        Console.CursorTop = Height + 2;

        Console.ForegroundColor = ConsoleColor.Green;
#endif

        return "TESTING";
    }

#if DEBUG && DUMP
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