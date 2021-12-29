#define DUMP
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._23;

public abstract class Base : Solution
{
    public override string Description => "Hotel amphipod";

    private (int, int)[,] _initialPositions;

    private const int Width = 11;

    private const int Height = 3;

    private int _amphipodCount;

    protected void ParseInput()
    {
        _initialPositions = new (int, int)[Width, Height];

        for (var y = 1; y < 4; y++)
        {
            for (var x = 1; x < Input[y].Length; x++)
            {
                if (Input[y][x] == '#' || Input[y][x] == '.' || Input[y][x] == ' ')
                {
                    continue;
                }

                _amphipodCount++;

                _initialPositions[x - 1, y - 1] = ((Input[y][x] - '@') * 2, _amphipodCount);
            }
        }

#if DUMP && DEBUG
        Dump(_initialPositions);
#endif
    }

    protected int Solve()
    {
        var costs = new List<int>();

        for (var i = 1; i <= _amphipodCount; i++)
        {
            costs.Add(TryMove(_initialPositions, i, new int[_amphipodCount]));
        }

        return costs.Min();
    }

    private int TryMove((int, int)[,] initialPositions, int index, int[] initialPreviousPositions)
    {
        if (initialPreviousPositions[index - 1] > Width)
        {
            return 0;
        }

        var positions = new (int Type, int Id)[Width, Height];

        Array.Copy(initialPositions, positions, Width * Height);

        var previousPositions = new int[initialPreviousPositions.Length];

        Array.Copy(initialPreviousPositions, previousPositions, initialPreviousPositions.Length);

        var x = 0;

        var y = 0;

        Console.CursorVisible = false;

        Console.CursorTop = 10;

        Dump(positions);

        // Find amphipod of index.
        while (true)
        {
            if (positions[x, y].Id == index)
            {
                break;
            }

            x++;

            if (x == Width)
            {
                x = 0;

                y++;
            }
        }

        var type = positions[x, y].Type;

        var id = positions[x, y].Id;

        var cost = 0;

        // Is home?
        if (x == type && y == 2 || x == type && y == 1 && positions[type, 2].Type == type)
        {
            return 0;
        }

        // In hallway, can it go home?
        if (y == 0)
        {
            if (positions[type, 1].Type != 0 || positions[type, 2].Type != type && positions[type, 2].Type != 0)
            {
                return 0;
            }

            positions[x, y].Type = 0;

            positions[x, y].Id = 0;

            while (x != type)
            {
                x += x < type ? 1 : -1;

                if (positions[x, y].Type != 0)
                {
                    return 0;
                }

                cost++;
            }

            cost++;
            
            y++;
            
            if (positions[type, 2].Type == 0)
            {
                cost++;

                y++;
            }

            positions[type, y].Type = type;

            positions[type, y].Id = id;

            Console.ForegroundColor = ConsoleColor.Blue;

            Dump(positions);

            Console.ForegroundColor = ConsoleColor.Green;

            if (y == 1)
            {
                Console.ReadKey();
            }

            return cost * GetCostMultiplier(type);
        }

        // In burrow, can it get out?
        if (y == 2 && positions[x, 1].Type != 0)
        {
            return 0;
        }

        // In burrow, can get out, pick a hallway position.
        var newX = previousPositions[index - 1];

        while (newX < Width && (positions[newX, 0].Type != 0 || newX == 2 || newX == 4 || newX == 6 || newX == 8))
        {
            newX++;

            if (newX >= Width)
            {
                return 0;
            }
        }

        positions[x, y].Type = 0;

        positions[x, y].Id = 0;

        cost = y;

        cost += Math.Abs(newX - x);

        positions[newX, 0].Type = type;

        positions[newX, 0].Id = id;

        //Console.CursorVisible = false;

        //Console.CursorTop = 10;

        //Console.WriteLine(cost * GetCostMultiplier(type));

        //Dump(positions);

        //Thread.Sleep(10);

        cost *= GetCostMultiplier(type);

        previousPositions[index - 1] = newX;

        for (var i = 1; i <= _amphipodCount; i++)
        {
            cost += TryMove(positions, i, previousPositions);
        }

        return cost;
    }

    private static int GetCostMultiplier(int type)
    {
        return type switch
        {
            4 => 10,
            6 => 100,
            8 => 1000,
            _ => 1
        };
    }

#if DUMP && DEBUG
    private static void Dump((int Type, int)[,] positions)
    {
        for (var y = 0; y < Height; y++)
        {
            Console.Write('#');

            for (var x = 0; x < Width; x++)
            {
                if (positions[x, y].Type == 0)
                {
                    Console.Write(' ');

                    continue;
                }

                Console.Write((char) (positions[x, y].Type / 2 + '@'));
            }

            Console.WriteLine('#');
        }

        Console.WriteLine();
    }
#endif
}