using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._23;

public abstract class Base : Solution
{
    public override string Description => "Hotel amphipod";

    private static readonly int[] Doors = [2, 4, 6, 8];

    private static readonly int[] HallwayStops =
        [0, 1, 3, 5, 7, 9, 10];

    private static readonly int[] Energy =
        [0, 1, 10, 100, 1000];

    private int _depth;

    private UInt128 _initialState;

    private UInt128 _goalState;

    private PriorityQueue<(UInt128 State, int Cost), int> _queue;

    private Dictionary<UInt128, int> _bestCosts;

    protected void ParseInput(bool insertExtra = false)
    {
        _depth = insertExtra ? 4 : 2;

        _initialState = UInt128.Zero;

        _goalState = UInt128.Zero;

        var rows = new List<string> { Input[2] };

        if (insertExtra)
        {
            rows.Add("  #D#C#B#A#");

            rows.Add("  #D#B#A#C#");
        }

        rows.Add(Input[3]);

        for (var room = 0; room < 4; room++)
        {
            for (var depth = 0; depth < _depth; depth++)
            {
                var type = rows[depth][3 + room * 2] - 'A' + 1;

                _initialState = Set(_initialState, RoomIndex(room, depth), type);

                _goalState = Set(_goalState, RoomIndex(room, depth), room + 1);
            }
        }
    }

    protected int Solve()
    {
        _queue = new PriorityQueue<(UInt128 State, int Cost), int>();

        _bestCosts = new Dictionary<UInt128, int>();

        Enqueue(_initialState, 0);

        while (_queue.TryDequeue(out var item, out _))
        {
            var (state, cost) = item;

            if (_bestCosts[state] != cost)
            {
                continue;
            }

            if (state == _goalState)
            {
                return cost;
            }

            MoveFromHallway(state, cost);

            MoveFromRooms(state, cost);
        }

        throw new InvalidOperationException("No solution found.");
    }

    private void MoveFromHallway(UInt128 state, int cost)
    {
        foreach (var hall in HallwayStops)
        {
            var type = Get(state, hall);

            if (type == 0)
            {
                continue;
            }

            var room = type - 1;

            var door = Doors[room];

            if (! HallwayClear(state, hall, door))
            {
                continue;
            }

            if (! RoomCanAccept(state, room, type, out var depth))
            {
                continue;
            }

            var steps = Math.Abs(hall - door) + depth + 1;

            var next = Set(state, hall, 0);

            next = Set(next, RoomIndex(room, depth), type);

            Enqueue(next, cost + steps * Energy[type]);
        }
    }

    private void MoveFromRooms(UInt128 state, int cost)
    {
        for (var room = 0; room < 4; room++)
        {
            var depth = 0;

            while (depth < _depth && Get(state, RoomIndex(room, depth)) == 0)
            {
                depth++;
            }

            if (depth == _depth)
            {
                continue;
            }

            var index = RoomIndex(room, depth);

            var type = Get(state, index);

            if (IsSettled(state, room, depth, type))
            {
                continue;
            }

            var door = Doors[room];

            var targetRoom = type - 1;

            var targetDoor = Doors[targetRoom];

            if (targetRoom != room && HallwayClear(state, door, targetDoor) && RoomCanAccept(state, targetRoom, type, out var targetDepth))
            {
                var steps = depth + 1 + Math.Abs(door - targetDoor) + targetDepth + 1;

                var next = Set(state, index, 0);

                next = Set(next, RoomIndex(targetRoom, targetDepth), type);

                Enqueue(next, cost + steps * Energy[type]);
            }

            foreach (var hall in HallwayStops)
            {
                if (! HallwayClear(state, door, hall))
                {
                    continue;
                }

                var steps = depth + 1 + Math.Abs(door - hall);

                var next = Set(state, index, 0);

                next = Set(next, hall, type);

                Enqueue(next, cost + steps * Energy[type]);
            }
        }
    }

    private bool RoomCanAccept(UInt128 state, int room, int type, out int targetDepth)
    {
        targetDepth = -1;

        for (var depth = _depth - 1; depth >= 0; depth--)
        {
            var occupant = Get(state, RoomIndex(room, depth));

            if (occupant == 0)
            {
                if (targetDepth == -1)
                {
                    targetDepth = depth;
                }
            }
            else if (occupant != type)
            {
                return false;
            }
        }

        return targetDepth >= 0;
    }

    private bool IsSettled(UInt128 state, int room, int depth, int type)
    {
        if (type != room + 1)
        {
            return false;
        }

        for (var d = depth + 1; d < _depth; d++)
        {
            if (Get(state, RoomIndex(room, d)) != type)
            {
                return false;
            }
        }

        return true;
    }

    private static bool HallwayClear(UInt128 state, int from, int to)
    {
        var step = from < to ? 1 : -1;

        for (var x = from + step; x != to + step; x += step)
        {
            if (Get(state, x) != 0)
            {
                return false;
            }
        }

        return true;
    }

    private void Enqueue(UInt128 state, int cost)
    {
        if (_bestCosts.TryGetValue(state, out var best) && best <= cost)
        {
            return;
        }

        _bestCosts[state] = cost;

        var priority = cost + Heuristic(state);

        _queue.Enqueue((state, cost), priority);
    }

    private int Heuristic(UInt128 state)
    {
        var estimate = 0;

        foreach (var hall in HallwayStops)
        {
            var type = Get(state, hall);

            if (type == 0)
            {
                continue;
            }

            var targetDoor = Doors[type - 1];

            var steps = Math.Abs(hall - targetDoor) + 1;

            estimate += steps * Energy[type];
        }

        for (var room = 0; room < 4; room++)
        {
            for (var depth = 0; depth < _depth; depth++)
            {
                var type = Get(state, RoomIndex(room, depth));

                if (type == 0)
                {
                    continue;
                }

                if (IsSettled(state, room, depth, type))
                {
                    continue;
                }

                var steps = depth + 1 + Math.Abs(Doors[room] - Doors[type - 1]) + 1;

                estimate += steps * Energy[type];
            }
        }

        return estimate;
    }

    private int RoomIndex(int room, int depth)
    {
        return 11 + room * _depth + depth;
    }

    private static int Get(UInt128 state, int index)
    {
        return (int) ((state >> (index * 3)) & 7);
    }

    private static UInt128 Set(UInt128 state, int index, int value)
    {
        var shift = index * 3;

        var mask = (UInt128) 7 << shift;

        return (state & ~mask) | ((UInt128) value << shift);
    }
}