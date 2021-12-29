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

        for (var i = 0; i < _amphipodCount; i++)
        {
            costs.Add(TryMove(_initialPositions, i));
        }

        return costs.Min();
    }

    private static int TryMove((int, int)[,] initialPositions, int index)
    {
        // Gonna need to deep copy positions.
        var positions = new (int Type, int Id)[Width, Height];

        Array.Copy(initialPositions, positions, Width * Height);

        var x = 0;

        var y = 0;

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

        // In hallway, can it go home?
        if (y == 0)
        {
            if (positions[type, 1].Type != 0 || positions[type, 2].Type != type && positions[type, 2].Type != 0)
            {
                return 0;
            }

            while (x != type)
            {
                x += x < type ? 1 : -1;

                if (positions[x, y].Type != 0)
                {
                    return 0;
                }

                cost++;
            }

            positions[x, y].Type = 0;

            positions[x, y].Id = 0;

            cost++;

            if (positions[2, type].Type == 0)
            {
                cost++;

                y++;
            }

            positions[type, y + 1].Type = type;

            positions[type, y + 1].Id = id;

            return cost * GetCostMultiplier(type);
        }

        // In burrow, can it get out?
        if (y == 2 && positions[x, 1].Type != 0)
        {
            return 0;
        }

        positions[x, y].Type = 0;

        positions[x, y].Id = 0;

        // In burrow, can get out, pick a hallway position.
        cost = y;

        y = 0;

        // How to pick a different position every time?
        var newX = 10;

        cost += Math.Abs(newX - x);

        positions[newX, y].Type = type;

        positions[newX, y].Id = id;

        Console.WriteLine(cost * GetCostMultiplier(type));

        Dump(positions);

        cost *= GetCostMultiplier(type);

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