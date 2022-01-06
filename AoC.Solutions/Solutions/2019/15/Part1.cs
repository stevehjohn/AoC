using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._15;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        Console.Clear();

        Console.CursorVisible = false;

        GetMap();

        Dump();

        return "TESTING";
    }

    private void Dump()
    {
        Console.CursorTop = 1;
        
        Console.CursorLeft = 0;

        for (var y = 0; y < Width; y++)
        {
            Console.Write(' ');

            for (var x = 0; x < Height; x++)
            {
                if (x == Origin.X && y == Origin.Y)
                {
                    Console.Write('S');

                    continue;
                }

                if (x == Destination.X && y == Destination.Y)
                {
                    Console.Write('O');

                    continue;
                }

                Console.Write(Map[x, y] ? ' ' : '█');
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}