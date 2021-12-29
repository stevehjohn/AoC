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
            costs.Add(TryMove(_initialPositions, i, Enumerable.Repeat(-1, _amphipodCount).ToArray()));
        }

        //costs.Add(TryMove(_initialPositions, 1, Enumerable.Repeat(-1, _amphipodCount).ToArray()));

        return costs.Min();
    }

    private int TryMove((int, int)[,] initialPositions, int index, int[] initialHallwayTargets)
    {
        // Hallway positions exhausted
        if (initialHallwayTargets[index - 1] >= Width - 1)
        {
            return int.MaxValue;
        }

        var positions = new (int Type, int Id)[Width, Height];

        Array.Copy(initialPositions, positions, Width * Height);

        var hallwayTargets = new int[initialHallwayTargets.Length];

        Array.Copy(initialHallwayTargets, hallwayTargets, initialHallwayTargets.Length);

        var home = 0;

        // All home?
        for (var y = 1; y < Height; y++)
        {
            for (var x = 2; x < Width - 2; x += 2)
            {
                if (positions[x, y].Type == x)
                {
                    home++;
                }
                // TODO: Uncomment when not using sparse test data.
                //else
                //{
                //    goto notHome;
                //}
            }
        }

        if (home == _amphipodCount)
        {
            return 0;
        }

        notHome:

        var aX = 0;

        var aY = 0;

        // Find amphipod of index.
        while (true)
        {
            if (positions[aX, aY].Id == index)
            {
                break;
            }

            aX++;

            if (aX == Width)
            {
                aX = 0;

                aY++;
            }
        }

        var type = positions[aX, aY].Type;

        var cost = 0;

        var moved = false;

        // Is home (and not blocking another in)?
        if (aX == type)
        {
            if (aY == 2 || aY == 1 && positions[type, 2].Type == type)
            {
                goto next;
            }
        }

        // In hallway or burrow, can get home?
        if (positions[type, 2].Type == 0)
        {
            cost = CostToGetTo(positions, aX, aY, type, 2);

            if (cost > 0)
            {
                MoveTo(positions, aX, aY, type, 2);

                moved = true;

                goto next;
            }
        }
        else if (positions[type, 1].Type == 0 && positions[type, 2].Type == type)
        {
            cost = CostToGetTo(positions, aX, aY, type, 1);

            if (cost > 0)
            {
                MoveTo(positions, aX, aY, type, 2);

                moved = true;

                goto next;
            }
        }

        // In burrow, can get to hallway?
        var targetX = ++hallwayTargets[index - 1];

        if (targetX is 2 or 4 or 6 or 8)
        {
            targetX++;

            hallwayTargets[index - 1]++;
        }

        cost = CostToGetTo(positions, aX, aY, targetX, 0);

        if (cost > 0)
        {
            MoveTo(positions, aX, aY, targetX, 0);

            moved = true;
        }

        // TODO: What are the pauses? Something to do with 0 cost?

        next:

#if DEBUG && DUMP
        //Console.ReadKey();

        Thread.Sleep(100);

        Console.CursorVisible = false;

        Console.CursorTop = 1;

        Console.WriteLine(cost);

        Dump(positions);
#endif

        // TODO: Add cost multiplier.
        if (moved)
        {
            var costs = new List<int>();

            for (var i = 1; i <= _amphipodCount; i++)
            {
                var subCost = TryMove(positions, i, hallwayTargets);

                if (subCost < int.MaxValue)
                {
                    costs.Add(subCost);
                }
            }

            if (costs.Any())
            {
                cost += costs.Min();
            }
        }
        else
        {
            return int.MaxValue;
        }

        return cost;
    }

    private static void MoveTo((int Type, int Id)[,] positions, int startX, int startY, int endX, int endY)
    {
        positions[endX, endY].Type = positions[startX, startY].Type;

        positions[endX, endY].Id = positions[startX, startY].Id;

        positions[startX, startY].Type = 0;

        positions[startX, startY].Id = 0;
    }

    private static int CostToGetTo((int Type, int)[,] positions, int startX, int startY, int endX, int endY)
    {
        var cost = 0;

        while (startY > 0)
        {
            startY--;

            if (positions[startX, startY].Type != 0)
            {
                return 0;
            }

            cost++;
        }

        while (startX != endX)
        {
            startX += startX < endX ? 1 : -1;

            if (positions[startX, startY].Type != 0)
            {
                return 0;
            }

            cost++;
        }

        while (startY < endY)
        {
            startY++;

            if (positions[startX, startY].Type != 0)
            {
                return 0;
            }

            cost++;
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
            Console.Write('.');

            for (var x = 0; x < Width; x++)
            {
                if (positions[x, y].Type == 0)
                {
                    Console.Write(y > 0 && x != 2 && x != 4 && x != 6 && x != 8 ? '.' : ' ');

                    continue;
                }

                Console.Write((char) (positions[x, y].Type / 2 + '@'));
            }

            Console.WriteLine('.');
        }

        Console.WriteLine();
    }
#endif
}