#define DUMP
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._23;

public abstract class Base : Solution
{
    public override string Description => "Hotel amphipod";

    private int[] _initialAmphipodState;

    // Stack for each room?
    protected void ParseInput()
    {
        _initialAmphipodState = new int[8];

        var count = 0;

        for (var y = 1; y < 4; y++)
        {
            for (var x = 1; x < 12; x += 2)
            {
                if (x >= Input[y].Length)
                {
                    continue;
                }

                var c = Input[y][x];

                if (c == '#' || c == '.' || c == ' ')
                {
                    continue;
                }

                _initialAmphipodState[count] = Encode(x - 1, y - 1, (c - '@') * 2);

                count++;
            }
        }

#if DEBUG && DUMP
        Console.Clear();
        
        Console.CursorVisible = false;

        Dump(_initialAmphipodState);
#endif
    }

    protected int Solve()
    {
        return 0;
    }

    private int Encode(int x, int y, int home)
    {
        var result = ((x & 255) << 16)
                     | ((y & 255) << 8)
                     | (home & 255);

        return result;
    }

    private (int X, int Y, int Home, int Id) Decode(int state)
    {
        var x = DecodeX(state);

        var y = DecodeY(state);

        var home = DecodeHome(state);
        
        var id = state & 255;

        return (x, y, home, id);
    }

    private int DecodeX(int state)
    {
        return (state >> 16) & 255;
    }

    private int DecodeY(int state)
    {
        return (state >> 8) & 255;
    }

    private int DecodeHome(int state)
    {
        return state & 255;
    }

#if DEBUG && DUMP
    private void Dump(int[] state)
    {
        Console.CursorTop = 1;

        Console.CursorLeft = 0;

        Console.WriteLine(" #############");

        Console.Write(" #");

        var previousColour = Console.ForegroundColor;

        for (var x = 0; x < 11; x++)
        {
            var pod = state.SingleOrDefault(s => DecodeX(s) == x && DecodeY(s) == 0);

            if (pod == 0)
            {
                Console.Write('.');

                continue;
            }

            Console.ForegroundColor = ConsoleColor.Blue;

            Console.Write(GetType(pod));

            Console.ForegroundColor = previousColour;
        }

        Console.WriteLine('#');

        for (var y = 1; y < 3; y++)
        {
            Console.Write(y == 1 ? " ###" : "   #");

            for (var x = 2; x < 10; x += 2)
            {
                var pod = state.SingleOrDefault(s => DecodeX(s) == x && DecodeY(s) == y);

                if (pod == 0)
                {

                    Console.Write(".#");

                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Blue;

                Console.Write(GetType(pod));

                Console.ForegroundColor = previousColour;

                Console.Write('#');
            }

            Console.WriteLine(y == 1 ? "##" : string.Empty);
        }

        Console.WriteLine("   #########");

        Console.WriteLine();
    }

    private char GetType(int home)
    {
        return (char) ('@' + (char) (DecodeHome(home) / 2));
    }
#endif
}