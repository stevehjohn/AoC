#define DUMP
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._23;

public abstract class Base : Solution
{
    public override string Description => "Hotel amphipod";

    protected int[,] InitialPositions;

    protected void ParseInput()
    {
        InitialPositions = new int[11, 3];

        for (var y = 2; y < 4; y++)
        {
            for (var x = 3; x < 10; x++)
            {
                if (Input[y][x] == '#' || Input[y][x] == '.')
                {
                    continue;
                }

                InitialPositions[x - 1, y - 1] = (Input[y][x] - '@') * 2 + 1;
            }
        }

#if DUMP && DEBUG
        Dump(InitialPositions);
#endif
    }

    protected static int Solve(int[,] positions)
    {
        var costs = new List<int>();

        for (var i = 0; i < positions.Length; i++)
        {
            costs.Add(TryMove(positions, i));
        }

        return costs.Min();
    }

    private static int TryMove(int[,] positions, int index)
    {
        // Gonna need to deep copy positions.

        //var amphipod = positions[index];

        //if (amphipod.Position.Y == 0)
        //{
        //    if (positions.Any(a => a.Position.X != amphipod.Type || a.Position.Y == 1))
        //    {
        //        return 0;
        //    }
        //}

        return 0;
    }

#if DUMP && DEBUG
    private void Dump(int[,] positions)
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

                Console.Write((char) ((positions[x, y] - 1) / 2 + '@'));
            }

            Console.WriteLine('#');
        }

        Console.WriteLine();
    }
#endif
}