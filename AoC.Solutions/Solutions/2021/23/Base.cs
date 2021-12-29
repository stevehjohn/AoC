#define DUMP
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._23;

public abstract class Base : Solution
{
    public override string Description => "Hotel amphipod";

    private int[,] _initialPositions;

    private int _amphipodCount;

    protected void ParseInput()
    {
        _initialPositions = new int[11, 3];

        for (var y = 1; y < 4; y++)
        {
            for (var x = 1; x < Input[y].Length; x++)
            {
                if (Input[y][x] == '#' || Input[y][x] == '.' || Input[y][x] == ' ')
                {
                    continue;
                }

                _initialPositions[x - 1, y - 1] = (Input[y][x] - '@') * 2;

                _amphipodCount++;
            }
        }

#if DUMP && DEBUG
        Dump(_initialPositions);
#endif
    }

    protected int Solve()
    {
        var costs = new List<int>();

        for (var i = 0; i < 1; i++)
        {
            costs.Add(TryMove(_initialPositions, 1));
        }

        return costs.Min();
    }

    private static int TryMove(int[,] positions, int index)
    {
        // Gonna need to deep copy positions.

        var x = 0;

        var y = 0;

        var i = -1;

        // Find amphipod of index.
        while (true)
        {
            if (positions[x, y] != 0)
            {
                i++;

                if (i == index)
                {
                    break;
                }
            }

            x++;

            if (x == positions.GetLength(0))
            {
                x = 0;

                y++;
            }
        }

        var type = positions[x, y];

        var cost = 0;

        // In hallway, can it go home?
        if (y == 0)
        {
            if (positions[type, 1] != 0 || positions[type, 2] != type && positions[type, 2] != 0)
            {
                return 0;
            }

            while (x != type)
            {
                x += x < type ? 1 : -1;

                if (positions[x, y] != 0)
                {
                    return 0;
                }

                cost++;
            }

            positions[x, y] = 0;

            cost++;

            if (positions[2, type] == 0)
            {
                cost++;

                y++;
            }

            positions[type, y + 1] = type;

            return cost * GetCost(type);
        }

        // In burrow, can it get out?
        if (y == 2 && positions[x, 1] != 0)
        {
            return 0;
        }

        // In burrow, can get out, pick a hallway position.

        return 0;
    }

    private static int GetCost(int type)
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
    private static void Dump(int[,] positions)
    {
        for (var y = 0; y < positions.GetLength(1); y++)
        {
            Console.Write('#');

            for (var x = 0; x < positions.GetLength(0); x++)
            {
                if (positions[x, y] == 0)
                {
                    Console.Write(' ');

                    continue;
                }

                Console.Write((char) (positions[x, y] / 2 + '@'));
            }

            Console.WriteLine('#');
        }

        Console.WriteLine();
    }
#endif
}