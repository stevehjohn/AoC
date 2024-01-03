using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._11;

public abstract class Base : Solution
{
    public override string Description => "Radioisotope thermoelectric elevators";

    protected static int Solve(long[] initialState)
    {
        var visited = new HashSet<int>();

        var queue = new PriorityQueue<(long[] State, int Steps), int>();

        queue.Enqueue((initialState, 0), 0);

        visited.Add(HashState(initialState));

        while (queue.Count > 0)
        {
            var item = queue.Dequeue();

            var state = item.State;

            if ((state[0] & 0xFFFFFFFF) == 0 && (state[1] & 0xFFFFFFFF) == 0 && (state[2] & 0xFFFFFFFF) == 0)
            {
                return item.Steps;
            }

            var nextStates = GetPossibleNextStates(state);

            foreach (var next in nextStates)
            {
                var nextHash = HashState(next);

                if (! visited.Contains(nextHash))
                {
                    queue.Enqueue((next, item.Steps + 1), -(item.Steps + 1));

                    visited.Add(nextHash);
                }
            }
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

        var states = new List<long[]>();

        // 0xFFFF0000 to mask off chips
        // 0xFFFF to mask off generators
        var items = GetBits(state[floor] & 0xFFFFFFFF);

        if (floor < 3)
        {
            // Try move 2 things up
            for (var i = 0; i < items.Length - 1; i++)
            {
                for (var j = i + 1; j < items.Length; j++)
                {
                    var newState = CopyState(state, floor + 1);

                    newState[floor] ^= items[i];
                    newState[floor] ^= items[j];

                    newState[floor + 1] |= items[i];
                    newState[floor + 1] |= items[j];

                    states.Add(newState);
                }
            }

            // Try move 1 thing up
            for (var i = 0; i < items.Length; i++)
            {
                var newState = CopyState(state, floor + 1);

                newState[floor] ^= items[i];

                newState[floor + 1] |= items[i];

                states.Add(newState);
            }
        }

        if (floor > 0)
        {
            // Try move 1 thing down
            for (var i = 0; i < items.Length; i++)
            {
                var newState = CopyState(state, floor - 1);

                newState[floor] ^= items[i];

                newState[floor - 1] |= items[i];

                states.Add(newState);
            }

            // Try move 2 things down
            for (var i = 0; i < items.Length - 1; i++)
            {
                for (var j = i + 1; j < items.Length; j++)
                {
                    var newState = CopyState(state, floor - 1);

                    newState[floor] ^= items[i];
                    newState[floor] ^= items[j];

                    newState[floor - 1] |= items[i];
                    newState[floor - 1] |= items[j];

                    states.Add(newState);
                }
            }
        }

        return states;
    }

    private static long[] GetBits(long value)
    {
        var bit = 1;

        var bits = new List<long>();

        for (var i = 0; i < 32; i++)
        {
            if ((value & bit) > 0)
            {
                bits.Add(bit);
            }

            bit <<= 1;
        }

        return bits.ToArray();
    }

    private static long[] CopyState(long[] state, int newFloor)
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

    protected long[] ParseInput()
    {
        var bit = 1;

        var itemCodes = new Dictionary<string, long>();

        var floors = new long[4];

        for (var f = 0; f < 3; f++)
        {
            var floor = 0x100000000 << f;

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
            }

            floors[f] = floor;
        }

        return floors;
    }
}