using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._11;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var floors = ParseInput();

        var result = Solve(floors);

        return result.ToString();
    }

    private static int Solve(long[] initialState)
    {
        var visited = new HashSet<int>();

        var queue = new PriorityQueue<long[], int>();

        queue.Enqueue(initialState, 0);

        visited.Add(HashState(initialState));

        var steps = 0;

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();

            if (state[0] < 0x100000000 && state[1] < 0x100000000 && state[2] < 0x100000000)
            {
                return steps;
            }

            var nextStates = GetPossibleNextStates(state);

            foreach (var next in nextStates)
            {
                var nextHash = HashState(next);

                if (! visited.Contains(nextHash))
                {
                    queue.Enqueue(next, steps);

                    visited.Add(nextHash);
                }
            }

            steps++;
        }

        throw new PuzzleException("Solution not found.");
    }

    private static List<long[]> GetPossibleNextStates(long[] state)
    {
        int floor;

        for (floor = 0; floor < 4; floor++)
        {
            if ((state[floor] & 0x1000000000000) > 0)
            {
                break;
            }
        }

        Console.WriteLine(floor);

        var states = new List<long[]>();

        // 0xFFFF0000 to mask off chips
        // 0xFFFF to mask off generators

        if (floor < 3)
        {
            // Try move 2 things up

            // Try move 1 thing up
        }

        if (floor > 0)
        {
            // Try move 1 thing down

            // Try move 2 things down (necessary?)
        }

        return states;
    }

    private long[] CopyState(long[] state, int newFloor)
    {
        var newState = new long[4];

        newState[0] = state[0] & 0xffffffffffff;
        newState[1] = state[1] & 0xffffffffffff;
        newState[2] = state[2] & 0xffffffffffff;
        newState[3] = state[3] & 0xffffffffffff;

        newState[newFloor] |= 0x1000000000000;

        return newState;
    }

    private static int HashState(long[] state)
    {
        return HashCode.Combine(state[0], state[1], state[2], state[3]);
    }

    private long[] ParseInput()
    {
        var bit = 1;

        var itemCodes = new Dictionary<string, long>();

        var floors = new long[4];

        for (var f = 0; f < 3; f++)
        {
            var floor = 0x100000000 << f;

            Console.WriteLine(floor);

            if (f == 0)
            {
                floor |= 0x1000000000000;
            }

            var line = Input[f][(f == 1 ? 26 : 25)..];

            var items = line.Split(',', StringSplitOptions.TrimEntries);

            if (items.Length > 1)
            {
                items[^1] = items[^1][4..];
            }

            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i].Split(' ', StringSplitOptions.TrimEntries)[1].Split('-');

                var key = item[0];

                long code;

                if (! itemCodes.ContainsKey(key))
                {
                    code = bit;

                    itemCodes.Add(key, code);

                    bit <<= 1;
                }
                else
                {
                    code = itemCodes[key];
                }

                if (item.Length > 1)
                {
                    code <<= 16;
                }

                floor |= code;

                Console.WriteLine($"{items[i]}    {Convert.ToString(code, 2)}");
            }

            floors[f] = floor;

            Console.WriteLine($"F{f}: {Convert.ToString(floors[f], 2)}");
        }

        return floors;
    }
}